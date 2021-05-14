﻿using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OliveToast.Managements
{
    public static class CommandExtension
    {
        public static async Task<IUserMessage> MsgReplyAsync(this SocketCommandContext context, object content, bool disalbeMention = true)
        {
            return await context.Message.ReplyAsync(text: content.ToString(), allowedMentions: disalbeMention ? AllowedMentions.None : null);
        }

        public static async Task<IUserMessage> MsgReplyEmbedAsync(this SocketCommandContext context, object content, bool disalbeMention = true)
        {
            return await context.Message.ReplyAsync(embed: context.CreateEmbed(content.ToString()).Build(), allowedMentions: disalbeMention ? AllowedMentions.None : null);
        }

        public static async Task<IUserMessage> MsgReplyEmbedAsync(this SocketCommandContext context, Embed emb, bool disalbeMention = true)
        {
            return await context.Message.ReplyAsync(embed: emb, allowedMentions: disalbeMention ? AllowedMentions.None : null);
        }

        public static EmbedBuilder CreateEmbed(this SocketCommandContext context, object description = null, string title = null, string imgUrl = null, string url = null, string thumbnailUrl = null, Color? color = null)
        {
            EmbedBuilder emb = new EmbedBuilder()
            {
                Title = title,
                Color = color ?? new Color(255, 200, 0),
                Footer = new()
                {
                    Text = context.User.GetName(context.IsPrivate),
                    IconUrl = context.User.GetAvatar()
                },
                Description = description?.ToString(),
                ImageUrl = imgUrl,
                Url = url,
                ThumbnailUrl = thumbnailUrl
            };

            return emb;
        }
        
        public static EmbedBuilder AddEmptyField(this EmbedBuilder emb)
        {
            return emb.AddField("**  **", "** **", true);
        }

        public static MemoryStream GetFileStream(this SocketCommandContext context, ref string url)
        {
            if (url == null)
            {
                if (context.Message.Attachments.Any())
                {
                    url = context.Message.Attachments.First().Url;
                }
                else
                {
                    return null;
                }
            }

            Utility.TimeOutWebClient wc = new Utility.TimeOutWebClient();
            byte[] bytes = wc.DownloadData(url);

            MemoryStream stream = new MemoryStream(bytes);

            return stream;
        }
    }
}
