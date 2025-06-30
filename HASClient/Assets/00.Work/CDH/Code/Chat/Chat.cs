using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace _00.Work.CDH.Code.Chat
{
    public class Chat : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _from;
        [SerializeField] private TextMeshProUGUI _chatText;

        public void Initialize()
        {
            
        }
        
        public void SetText(string from, string text)
        {
            _from.SetText(from);
            _chatText.SetText(text);
        }
    }
}