using AKH.Network;
using DewmoLib.Utiles;
using KHG.Events;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoomBar : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI roomNameTmp;
    [SerializeField] private TextMeshProUGUI roomCountTmp;
    [SerializeField] private TextMeshProUGUI roomOwnerTmp;
    [SerializeField] private EventChannelSO uiChannel;

    [SerializeField] private Button connectButton;

    private int _roomId;
    public string Name
    {
        get => gameObject.name;
        set => gameObject.name = value;
    }

    public void SetRoomInfo(RoomInfoPacket info)
    {
        CheckPlayerCount(info.currentCount,info.maxCount);

        Name = $"Room_{info.roomId}";
        roomNameTmp.SetText(info.roomName);
        roomCountTmp.SetText($"{info.currentCount}/{info.maxCount}");
        roomOwnerTmp.SetText(info.hostName);

        _roomId = info.roomId;
    }

    private void CheckPlayerCount(int cur,int max)
    {
        connectButton.interactable = cur < max;
    }

    public void ConnectRoom()
    {
        if (_roomId == 0) return;

        Connect();
    }

    private void Connect()
    {
        C_RoomEnter c_RoomEnter = new C_RoomEnter() { roomId = _roomId };

        NetworkManager.Instance.SendPacket(c_RoomEnter);

        uiChannel.InvokeEvent(UserInterfaceEvents.ServerConnectEvent);
    }
}
