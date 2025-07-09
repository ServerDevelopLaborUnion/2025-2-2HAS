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
        [SerializeField] private GameObject target;

        [SerializeField] private TextMeshProUGUI TitleTmp;
        [SerializeField] private TextMeshProUGUI MessageTmp;

        private Panel targetPanel;

        private void Awake()
        {
            targetPanel = GetComponent<Panel>();
            uiChannel.AddListener<WarnUiEvent>(HandleBanner);
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
            target.SetActive(true);

            targetPanel.SetActive(true);
        }
    }
}