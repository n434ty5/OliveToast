﻿using Discord;
using Discord.Commands;
using Discord.WebSocket;
using OliveToast.Managements;
using System;
using System.Threading.Tasks;

namespace OliveToast
{
    class EventHandler
    {
        public static readonly string prefix = ConfigManager.Get("PREFIX");

        public static async Task OnLog(LogMessage msg)
        {
            Console.WriteLine(msg);

            await Task.CompletedTask;
        }

        public static async Task OnCommandLog(LogMessage msg)
        {
            Console.WriteLine(msg);

            await Task.CompletedTask;
        }

        public static async Task OnMessageReceived(SocketMessage msg)
        {
            SocketUserMessage userMsg = msg as SocketUserMessage;

            if (userMsg == null || userMsg.Content == null ||
                userMsg.Author.Id == Program.Client.CurrentUser.Id || userMsg.Author.IsBot) return;

            int argPos = 0;
            if (userMsg.HasStringPrefix(prefix, ref argPos) || userMsg.HasMentionPrefix(Program.Client.CurrentUser, ref argPos))
            {
                SocketCommandContext context = new SocketCommandContext(Program.Client, userMsg);

                await Program.Command.ExecuteAsync(context, argPos, Program.Service);
            }
        }

        public static async Task OnMessageUpdated(Cacheable<IMessage, ulong> cache, SocketMessage msg, ISocketMessageChannel channel)
        {
            await OnMessageReceived(msg);
        }

        public static async Task OnCommandExecuted(Optional<CommandInfo> command, ICommandContext context, IResult result)
        {
            if (!result.IsSuccess)
            {
                var ctx = context as SocketCommandContext;

                EmbedBuilder emb = ctx.CreateEmbed(title: "오류 발생!", description: $"{result.Error}: {result.ErrorReason}");

                await ctx.MsgReplyEmbedAsync(emb.Build());
            }
        }
    }
}
