using _00.Work.CDH.Code.ChatFolder;
using DewmoLib.Utiles;
using System.Collections;
using TMPro;
using Unity.Hierarchy;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Assets._00.Work.CDH.Code.ChatFolder
{
    public class ChatInput : MonoBehaviour
    {
        public UnityEvent<string> chatSendEvent;

        [SerializeField] private EventChannelSO chatEventChannel;

        private bool isChatVisible = false;
        private bool isChat = false;

        private void Start()
        {
            ChattingImmediatelyClose();
        }

        private void Update()
        {
            if(Keyboard.current.enterKey.wasPressedThisFrame)
            {
                if (!isChatVisible && !isChat)
                {
                    ChattingOpen();
                }
                else if (isChatVisible && isChat)
                {
                    SendChat();
                }
                else if(isChatVisible && !isChat)
                {
                    Chatting();
                }
            }
            if(Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                ChattingImmediatelyClose();
            }
        }

        private void ChattingOpen()
        {
            chatEventChannel.InvokeEvent(ChatGameEvents.openEvt);
            isChatVisible = true;
            Chatting();
        }

        private void Chatting()
        {
            chatEventChannel.InvokeEvent(ChatGameEvents.chattingEvt);
            isChat = true;
        }

        private void ChattingImmediatelyClose()
        {
            isChatVisible = false;
            isChat = false;
            chatEventChannel.InvokeEvent(ChatGameEvents.closeEvt);
        }

        private void SendChat()
        {
            chatEventChannel.InvokeEvent(ChatGameEvents.chatSendEvt);
        }

    }
}
