using System;
using System.Collections.Generic;
using System.Linq;
using AKH.Network;
using DewmoLib.Network.Core;
using DewmoLib.Utiles;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace _00.Work.CDH.Code.Chat
{
    public class ChatManager : MonoBehaviour
    {
        [SerializeField] private EventChannelSO chatEventChannel;
        [SerializeField] private Transform chatsTrm;
        [SerializeField] private GameObject chatUI;
        [SerializeField] private TMP_InputField chatInput;
        [SerializeField] private Chat chatPrefab;
        
        private List<Chat> _chats;
        private bool _isChatting = false;
        private bool _isChatActive = false;

        private void Awake()
        {
            _chats = new List<Chat>();
            chatEventChannel.AddListener<ChatEventHandler>(RecvChat);
        }

        private void Update()
        {
            if (Keyboard.current.enterKey.wasPressedThisFrame)
            {
                _isChatActive = true;
                _isChatting = _isChatActive && !_isChatting;
            }
        }

        private void RecvChat(ChatEventHandler evt)
        {
            Chat newChat = Object.Instantiate(chatPrefab, chatsTrm);
            newChat.SetText(evt.pName, evt.message);
        }

        [ContextMenu("Send Chat")]
        private void SendChat()
        {
            C_Chat newChat = new C_Chat();
            newChat.text = chatInput.text;
            NetworkManager.Instance.SendPacket(newChat);
            
            Chat chat = Object.Instantiate(chatPrefab, chatsTrm);
            chat.SetText("Me : ", chatInput.text);
        }
    }
}