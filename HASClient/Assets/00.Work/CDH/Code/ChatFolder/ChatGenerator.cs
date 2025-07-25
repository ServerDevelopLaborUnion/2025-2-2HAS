﻿using _00.Work.CDH.Code.ChatFolder;
using UnityEngine;

namespace Assets._00.Work.CDH.Code.ChatFolder
{
    public class ChatGenerator
    {
        public Chat chatPrefab { get; set; }

        public Chat Generate(string name, string message)
        {
            Chat newChat = Object.Instantiate(chatPrefab);
            newChat.SetText(name, message);
            newChat.TextSizeSetting();
            return newChat;
        }
    }
}
