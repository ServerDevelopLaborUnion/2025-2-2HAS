using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using static Unity.Burst.Intrinsics.X86.Avx;

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

        
        public void TextSizeSetting()
        {
            float preferredHeight = _chatText.preferredHeight;
            Vector2 sizeDelta = _chatText.rectTransform.sizeDelta;
            sizeDelta.y = preferredHeight;
            _chatText.rectTransform.sizeDelta = sizeDelta;
        }
    }
}