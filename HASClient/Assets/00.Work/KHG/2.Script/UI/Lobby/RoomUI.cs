using AKH.Network;
using DewmoLib.Utiles;
using KHG.Events;
using System.Collections.Generic;
using UnityEngine;

public class RoomUI : MonoBehaviour
{
    [SerializeField] private Transform roomHandleTrm;
    [SerializeField] private EventChannelSO packetChannel;

    [SerializeField] private GameObject roomPrefab;

    private void Awake()
    {
        packetChannel.AddListener<RoomListEvent>(HandleRoomList);
    }
     
    private void Start()
    {
        RequestRoomList();
    }

    public void CreateRoomList(List<RoomInfoPacket> list)
    {
        RemoveChildren(roomHandleTrm);

        for (int i = 0; i < list.Count; i++)
        {
            BuildRoom(list[i]);
        }
    }

    public void RequestRoomList()
    {
        C_RoomList c_RoomList = new C_RoomList();
        NetworkManager.Instance.SendPacket(c_RoomList);
    }

    private void BuildRoom(RoomInfoPacket roomInfo)
    {
        GameObject roomBar = Instantiate(roomPrefab, roomHandleTrm);

        if (roomBar.TryGetComponent(out RoomBar roomUi))
        {
            roomUi.SetRoomInfo(roomInfo);
        }
    }

    private void HandleRoomList(RoomListEvent evt)
    {
        CreateRoomList(evt.infoPackets);
    }

    private void RemoveChildren(Transform target)//새로고침을 만들어야합니다
    {
    }
}
