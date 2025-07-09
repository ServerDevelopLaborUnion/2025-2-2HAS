using System;
using System.Collections.Generic;
using System.Linq;
using AKH.Network;
using Assets._00.Work.CDH.Code.ChatFolder;
using DewmoLib.Network.Core;
using DewmoLib.Utiles;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace _00.Work.CDH.Code.ChatFolder
{
    public class ChatManager : MonoBehaviour
    {
        [SerializeField] private EventChannelSO chatEventChannel;

        private ChatGenerator chatGenerator;
        private List<Chat> _chats;
        private bool _isChatting = false;
        private bool _isChatActive = false;

        private void Awake()
        {
            chatGenerator = new();
            _chats = new List<Chat>();
            chatEventChannel.AddListener<ChatRecvEventHandler>(RecvChat);
        }

        private void RecvChat(ChatRecvEventHandler evt)
        {
            Chat newChat = chatGenerator.Generate(evt.pName, evt.message);
            _chats.Add(newChat);
        }

        public void SendChat(string message)
        {
            C_Chat newChat = new C_Chat();
            newChat.text = message;
            NetworkManager.Instance.SendPacket(newChat);
        }
    }
}