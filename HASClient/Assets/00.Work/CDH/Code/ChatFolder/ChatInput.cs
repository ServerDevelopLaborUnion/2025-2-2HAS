using _00.Work.CDH.Code.ChatFolder;
using TMPro;
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

        private void Start()
        {
            ChattingClose();
        }

        private void Update()
        {
            if(Keyboard.current.enterKey.wasPressedThisFrame)
            {
                if(!isChatVisible && !isChat)
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
        }

        private void ChattingOpen()
        {
            chatUIObj.SetActive(true);
            isChatVisible = true;
            Chatting();
        }

        private void Chatting()
        {
            chatInputField.ActivateInputField();
            isChat = true;
        }

        private void ChattingClose()
        {
            isChatVisible = false;
            isChat = false;
            chatUIObj.SetActive(false);
        }

        private void SendChat()
        {
            isChat = false;
            chatInputField.DeactivateInputField();
            chatSendEvent?.Invoke(chatInputField.text);
            chatInputField.text = "";
            ChattingClose();
        }
    }
}
