using System;
using System.Collections.Generic;
using System.Linq;
using AKH.Network;
using ServerCore;
using DewmoLib.Utiles;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Object = UnityEngine.Object;

namespace _00.Work.CDH.Code.Chat
{
    public class ChatManager : MonoBehaviour
    {
        [SerializeField] private EventChannelSO chatEventChannel;
        [SerializeField] private Transform chatsTrm;
        [SerializeField] private Chat chatPrefab;
        
        private List<Chat> _chats;

        private void Awake()
        {
            _chats = new List<Chat>();
            chatEventChannel.AddListener<ChatEventHandler>(RecvChat);
        }

        private void RecvChat(ChatEventHandler evt)
        {
            Chat newChat = Object.Instantiate(chatPrefab, chatsTrm);
            newChat.SetText(evt.pName, evt.message);
        }

        [ContextMenu("Send Chat")]
        public void SendChat(string message)
        {
            C_Chat newChat = new C_Chat();
            newChat.text = message;
            NetworkManager.Instance.SendPacket(newChat);
            
            Chat chat = Object.Instantiate(chatPrefab, chatsTrm);
            chat.SetText("Me : ", message);
        }
    }
}