using _00.Work.CDH.Code.ChatFolder;
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

        [SerializeField] private TMP_InputField chatInputField;
        [SerializeField] private GameObject chatUIObj;

        private bool isChatVisible = false;
        private bool isChat = false;

        private Coroutine chatCloseCoroutine;

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
                    ChattingOpen();
                }
                else if(isChatVisible && !isChat)
                {
                    Chatting();
                }
            }
        }

        private void ChattingOpen()
        {
            chatUIObj.SetActive(true);
            isChatVisible = true;
            Chatting();
        }

        private void Chatting()
        {
            if (chatCloseCoroutine != null)
                StopCoroutine(chatCloseCoroutine);

            chatInputField.ActivateInputField();
            isChat = true;
        }

        private void ChattingImmediatelyClose()
        {
            isChatVisible = false;
            isChat = false;
            chatUIObj.SetActive(false);
        }

        private void SendChat()
        {
            string message = chatInputField.text;

            if (!CheckChatText(message))
            {
                ChattingImmediatelyClose();
                Debug.Log("메시지가 없어 채팅을 닫습니다.");
                return;
            }

            isChat = false;
            chatInputField.DeactivateInputField();
            chatSendEvent?.Invoke(message);
            chatInputField.text = "";

            chatCloseCoroutine = StartCoroutine(ChatCloseCoroutine(3f));
        }

        private IEnumerator ChatCloseCoroutine(float time)
        {
            yield return new WaitForSecondsRealtime(time);
            isChatVisible = false;
            isChat = false;
            chatUIObj.SetActive(false);
        }

        private bool CheckChatText(string message)
        {
            if (message == "")
                return false;
            return true;
        }
    }
}
