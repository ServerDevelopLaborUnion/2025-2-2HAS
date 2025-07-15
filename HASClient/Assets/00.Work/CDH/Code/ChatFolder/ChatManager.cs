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

        // [SerializeField] private ChatGenerator chatGenerator;
        [SerializeField] private RectTransform chatsTrm;
        [SerializeField] private ScrollRect scrollRect;
        [SerializeField] private Chat chatPrefab;

        private List<Chat> _chats;
        private float maxSize = 0f;
        private ChatGenerator _chatGenerator;

        private void Awake()
        {
            _chats = new List<Chat>();
            chatEventChannel.AddListener<ChatRecvEventHandler>(RecvChat);
            _chatGenerator = new ChatGenerator();
            _chatGenerator.chatPrefab = chatPrefab;
        }

        private void Start()
        {
            Debug.Log("Test Setting name");
            C_SetName c_SetName = new C_SetName();
            c_SetName.name = "rhgksruf roajdcjddl";
            NetworkManager.Instance.SendPacket(c_SetName);
        }

        private void RecvChat(ChatRecvEventHandler evt)
        {
            print("¹ÞÀº Ãª ÆÐÅ¶ : " + evt.pName + " : " + evt.message);

            Chat newChat = _chatGenerator.Generate(evt.pName, evt.message);
            newChat.transform.SetParent(chatsTrm);
            newChat.transform.localScale = Vector3.one;

            if(maxSize < newChat.transform.position.x)
                maxSize = newChat.transform.position.x;

            _chats.Add(newChat);

            Canvas.ForceUpdateCanvases();
            chatsTrm.anchoredPosition = new Vector3(maxSize, 0, 0);
            scrollRect.verticalNormalizedPosition = 0f;
        }

        public void SendChat(string message)
        {
            C_Chat newChat = new C_Chat();
            newChat.text = message;
            NetworkManager.Instance.SendPacket(newChat);

            print("º¸³½ Ãª ÆÐÅ¶ : " + newChat.text);
        }

        
    }
}