using AKH.Network;
using Assets._00.Work.CDH.Code.ChatFolder;
using DewmoLib.Utiles;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace _00.Work.CDH.Code.ChatFolder
{
    public class ChatManager : MonoBehaviour
    {
        [SerializeField] private EventChannelSO chatEventChannel;

        [SerializeField] private ChatGenerator chatGenerator;
        [SerializeField] private Transform chatsTrm;
        [SerializeField] private ScrollRect scrollRect;

        private List<Chat> _chats;

        private void Awake()
        {
            _chats = new List<Chat>();
            chatEventChannel.AddListener<ChatRecvEventHandler>(RecvChat);
        }

        private void Start()
        {
            Debug.Log("Test Setting name");
            C_SetName c_SetName = new C_SetName();
            c_SetName.name = "It's me! Mario!";
            NetworkManager.Instance.SendPacket(c_SetName);
        }

        private void RecvChat(ChatRecvEventHandler evt)
        {
            Chat newChat = chatGenerator.Generate(evt.pName, evt.message);
            newChat.transform.SetParent(chatsTrm);
            newChat.transform.localScale = Vector3.one;
            _chats.Add(newChat);

            Canvas.ForceUpdateCanvases();
            scrollRect.verticalNormalizedPosition = 0f;
        }

        public void SendChat(string message)
        {
            if (!CheckChatText(message)) return;

            C_Chat newChat = new C_Chat();
            newChat.text = message;
            NetworkManager.Instance.SendPacket(newChat);
        }

        private bool CheckChatText(string message)
        {
            if(message == string.Empty)
                return false;   
            return true;
        }
    }
}