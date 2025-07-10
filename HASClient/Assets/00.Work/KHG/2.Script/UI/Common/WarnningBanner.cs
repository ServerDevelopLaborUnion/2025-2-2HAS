using DewmoLib.Utiles;
using KHG.Events;
using System;
using TMPro;
using UnityEngine;

namespace KHG.UIs
{
    public class WarnningBanner : PanelController
    {
        [SerializeField] private EventChannelSO uiChannel;
        [SerializeField] private Panel targetPanel;

        [SerializeField] private TextMeshProUGUI TitleTmp;
        [SerializeField] private TextMeshProUGUI MessageTmp;

        private void Awake()
        {
            uiChannel.AddListener<WarnUiEvent>(HandleBanner);
        }

        private void OnDestroy()
        {
            uiChannel.RemoveListener<WarnUiEvent>(HandleBanner);
        }
        private void HandleBanner(WarnUiEvent evt)
        {
            TitleTmp.SetText(evt.Title);
            MessageTmp.SetText(evt.Message);

            Open();
        }

        public override void Close()
        {
            targetPanel.SetActive(false);
        }

        public override void Open()
        {
            print($"OpenSequence {targetPanel.gameObject.name}이 가지고있습니다:{targetPanel}");
            targetPanel.gameObject.SetActive(true);
            targetPanel.SetActive(true);
        }
    }
}