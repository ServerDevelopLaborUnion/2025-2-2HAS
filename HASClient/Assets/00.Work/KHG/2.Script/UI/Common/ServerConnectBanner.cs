using Core.EventSystem;
using DewmoLib.Dependencies;
using DewmoLib.Utiles;
using KHG.Events;
using UnityEngine;

namespace KHG.UIs
{
    public class ServerConnectBanner : PanelController
    {
        [SerializeField] private Panel connectPanel;
        public override void Close()
        {
            connectPanel.SetActive(false);
        }

        public override void Open()
        {
            connectPanel.gameObject.SetActive(true);
            connectPanel.SetActive(true);
        }
    }
}
