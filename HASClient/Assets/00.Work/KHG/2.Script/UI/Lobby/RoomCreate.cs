using AKH.Network;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.iOS;
using UnityEngine.UI;

namespace KHG.UIs
{
    public class RoomCreate : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown roomPlrCount;
        [SerializeField] private TMP_InputField roomNameField;

        private int _roomCount;
        private string _roomName;

        public void OnCreatePressed()
        {
            print("방을 생성합니다");
            SetRoomInfo();
            SendRoomInfo();
        }
        public void OnValueChanged(int index)
        {
            switch (index)
            {
                case 0:
                    roomPlrCount.value = 5;
                    break;
                case 1:
                    roomPlrCount.value = 10;
                    break;
                case 2:
                    roomPlrCount.value = 15;
                    break;
                default:
                    roomPlrCount.value = 5;
                    break;
            }
        }

        private void SetRoomInfo()
        {
            _roomCount = roomPlrCount.value;
            _roomName = roomNameField.text;
            print($"방 정보가 입력됨:t{roomNameField.text}-r{_roomName},t{roomPlrCount.value}-r{_roomCount}");
        }

        private void SendRoomInfo()
        {
            C_CreateRoom c_CreateRoom = new()
            {
                roomName = _roomName,
                maxCount = _roomCount
            };
            NetworkManager.Instance.SendPacket(c_CreateRoom);

            print("방 생서ㅓㅇ 패킷 전달");
        }
    }
}
