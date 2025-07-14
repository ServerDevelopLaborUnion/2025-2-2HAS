using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace _00.Work.CDH.Code.ChatFolder
{
    public class Chat : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _chatText;

        public void Initialize()
        {
            
        }
        
        public void SetText(string from, string text)
        {
            _chatText.SetText(from + " : " + text);
        }
    }
}