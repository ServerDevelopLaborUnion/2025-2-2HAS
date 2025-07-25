using DewmoLib.Utiles;
using KHG.Events;
using System.Diagnostics.Tracing;
using TMPro;
using UnityEngine;

namespace KHG.UIs
{
    public class NameApplyUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI inputText;
        [SerializeField] private EventChannelSO playerInfoChannel;

        private string nickname;
        public void ApplyName()
        {
            if (inputText.text.Length < 2)
                return;
            nickname = inputText.text;
            SendName();
        }

        private void SendName()
        {
            PlayerNameEvent evt = PlayerInfoEvents.PlayerNameEvent;
            evt.Name = nickname;
            playerInfoChannel.InvokeEvent(evt);

            print("name apply:" + evt.Name);
        }
    }

}