using Core.EventSystem;
using DewmoLib.Dependencies;
using DewmoLib.Utiles;
using KHG.Events;
using KHG.Managers;
using UnityEngine;

namespace KHG.UIs
{
    public class RoomManager : MonoBehaviour
    {
        [Inject] private PacketResponsePublisher _publisher;
        [SerializeField] private EventChannelSO packetChannel;
        [SerializeField] private EventChannelSO uiChannel;

        [SerializeField] private PanelController connectPanelController;

        private void Awake()
        {
            _publisher.AddListener(PacketID.C_RoomEnter, IsEnterSuccess);
            _publisher.AddListener(PacketID.C_CreateRoom, IsEnterSuccess);
            uiChannel.AddListener<ServerConnectEvent>(HandleConnectReq);
        }

        private void OnDestroy()
        {
            _publisher.RemoveListener(PacketID.C_RoomEnter, IsEnterSuccess);
            _publisher.RemoveListener(PacketID.C_CreateRoom, IsEnterSuccess);
            uiChannel.RemoveListener<ServerConnectEvent>(HandleConnectReq);
        }

        private void IsEnterSuccess(bool value)
        {
            connectPanelController.Close();

            SceneLoadController.LoadScene("SampleScene");
        }

        private void HandleConnectReq(ServerConnectEvent evt)
        {
            connectPanelController.Open();
        }
    }
}
