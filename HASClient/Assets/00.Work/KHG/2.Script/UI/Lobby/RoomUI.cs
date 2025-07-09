using System.Collections.Generic;
using UnityEngine;

public class RoomUI : MonoBehaviour
{
    [SerializeField] private Transform roomHandleTrm;

    [SerializeField] private GameObject roomPrefab;

    public void CreateRoomList(List<RoomInfoPacket> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            BuildRoom(list[i]);
        }
    }

    private void BuildRoom(RoomInfoPacket roomInfo)
    {
        GameObject roomBar = Instantiate(roomPrefab, roomHandleTrm);

        if (roomBar.TryGetComponent(out RoomBar roomUi))
        {
            roomUi.SetRoomInfo(roomInfo);
        }
    }
}
