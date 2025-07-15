using AKH.Network;
using Assets._00.Work.CDH.Code.ChatFolder;
using DewmoLib.Utiles;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _00.Work.CDH.Code.ChatFolder
{
    public class ChatManager : MonoBehaviour
    {
        [Header("Chat Manager")]
        [SerializeField] private EventChannelSO packetEventChannel;

        // [SerializeField] private ChatGenerator chatGenerator;
        [SerializeField] private RectTransform chatsTrm;
        [SerializeField] private ScrollRect scrollRect;
        [SerializeField] private Chat chatPrefab;

        private List<Chat> _chats;
        private float maxSize = 0f;
        private ChatGenerator _chatGenerator;

        [Header("Chat Input")]
        [SerializeField] private EventChannelSO chatEventChannel;
        [SerializeField] private TMP_InputField chatInputField;
        [SerializeField] private GameObject chatUIObj;

        private Coroutine chatCloseCoroutine;

        private void Awake()
        {
            _chats = new List<Chat>();
            packetEventChannel.AddListener<ChatRecvEventHandler>(RecvChat);
            _chatGenerator = new ChatGenerator();
            _chatGenerator.chatPrefab = chatPrefab;
        }

        private void Start()
        {
            Debug.Log("Test Setting name");
            C_SetName c_SetName = new C_SetName();
            c_SetName.name = "rhgksruf roajdcjddl";
            NetworkManager.Instance.SendPacket(c_SetName);
            // 

            chatEventChannel.AddListener<ChatOpenEvent>(ChatOpen);
            chatEventChannel.AddListener<ChatCloseEvent>(ChatClose);
            chatEventChannel.AddListener<ChattingEvent>(Chatting);
            chatEventChannel.AddListener<ChattingSendEvent>(SendChat);
        }

        private void SendChat(ChattingSendEvent evt)
        {
            string message = chatInputField.text;

            if (!CheckChatText(message))
            {
                ChatCloseEvent closeEvt = ChatGameEvents.closeEvt;
                closeEvt.timer = 0f;
                chatEventChannel.InvokeEvent(closeEvt);
                Debug.Log("메시지가 없어 채팅을 닫습니다.");
                return;
            }

            chatInputField.DeactivateInputField();
            SendChat(message);
            chatInputField.text = "";

        }

        private bool CheckChatText(string message)
        {
            if (message == "")
                return false;
            return true;
        }

        private void Chatting(ChattingEvent evt)
        {
            if (chatCloseCoroutine != null)
                StopCoroutine(chatCloseCoroutine);

            chatInputField.ActivateInputField();
        }

        private void ChatClose(ChatCloseEvent evt)
        {
            chatCloseCoroutine = StartCoroutine(ChatCloseCoroutine(3f));
        }

        private IEnumerator ChatCloseCoroutine(float time)
        {
            yield return new WaitForSecondsRealtime(time);
            chatUIObj.SetActive(false);
        }

        private void ChatOpen(ChatOpenEvent evt)
        { 
            chatUIObj.SetActive(true);

        }

        private void RecvChat(ChatRecvEventHandler evt)
        {
            print("받은 챗 패킷 : " + evt.pName + " : " + evt.message);

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

            print("보낸 챗 패킷 : " + newChat.text);
        }

        
    }
}