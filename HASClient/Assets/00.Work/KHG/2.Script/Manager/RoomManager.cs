using AKH.Network;
using DewmoLib.Utiles;
using KHG.Events;
using System;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] private EventChannelSO packetChannel;

    [SerializeField] private RoomUI roomUI;

    private void Awake()
    {
        packetChannel.AddListener<RoomListEvent>(HandleRoomList);
    }

    private void Start()
    {
        C_RoomList c_RoomList = new C_RoomList();
        NetworkManager.Instance.SendPacket(c_RoomList);
    }

    private void HandleRoomList(RoomListEvent evt)
    {
        roomUI.CreateRoomList(evt.infoPackets);
    }
}
