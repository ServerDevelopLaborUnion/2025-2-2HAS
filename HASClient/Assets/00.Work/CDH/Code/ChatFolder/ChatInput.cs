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

        private bool isChatVisible = false;
        private bool isChat = false;

        private void Update()
        {
            if(Keyboard.current.enterKey.wasPressedThisFrame)
            {
                if(!isChatVisible && !isChat)
                {
                    isChatVisible = true;
                    isChat = true;
                    chatInputField.ActivateInputField();
                }
                else if (isChatVisible && isChat)
                {
                    chatSendEvent?.Invoke("Test Chatting");
                }
            }
        }
    }
}
