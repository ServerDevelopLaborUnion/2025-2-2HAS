using AKH.Network;
using DewmoLib.Utiles;
using KHG.Events;
using TMPro;
using UnityEngine;

namespace KHG.UIs
{
    public static class DropdownExtension
    {
        public static string GetValue(this TMP_Dropdown dropdown)
        {
            int index = dropdown.value;
            return dropdown.options[index].text;
        }
    }
    public class RoomCreate : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown roomPlrCount;
        [SerializeField] private TMP_InputField roomNameField;

        [SerializeField] private EventChannelSO uiChannel;

        private int _roomCount;
        private string _roomName;

        public void OnCreatePressed()
        {
            SetRoomInfo();
            SendRoomInfo();
        }

        private void SetRoomInfo()
        {
            _roomCount = int.Parse(DropdownExtension.GetValue(roomPlrCount));
            _roomName = roomNameField.text;
        }

        private void SendRoomInfo()
        {
            C_CreateRoom c_CreateRoom = new()
            {
                roomName = _roomName,
                maxCount = _roomCount
            };
            NetworkManager.Instance.SendPacket(c_CreateRoom);

            uiChannel.InvokeEvent(UserInterfaceEvents.ServerConnectEvent);
        }
    }
}
