﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OliveToast.Managements
{
    class WordsManager
    {
        public static readonly List<string> Words;

        static WordsManager()
        {
            string content = File.ReadAllText("words.json");

            Words = JsonConvert.DeserializeObject<List<string>>(content);
        }
    }

    class WordSession
    {
        public static Dictionary<ulong, List<string>> Sessions = new Dictionary<ulong, List<string>>();
    }
}
