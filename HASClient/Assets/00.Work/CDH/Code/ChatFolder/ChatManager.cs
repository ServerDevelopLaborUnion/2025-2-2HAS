using AKH.Network;
using Assets._00.Work.CDH.Code.ChatFolder;
using DewmoLib.Utiles;
using System.Collections.Generic;
using UnityEngine;

namespace _00.Work.CDH.Code.ChatFolder
{
    public class ChatManager : MonoBehaviour
    {
        [SerializeField] private EventChannelSO chatEventChannel;

        [SerializeField] private ChatGenerator chatGenerator;
        private List<Chat> _chats;
        private bool _isChatting = false;
        private bool _isChatActive = false;

        private void Awake()
        {
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