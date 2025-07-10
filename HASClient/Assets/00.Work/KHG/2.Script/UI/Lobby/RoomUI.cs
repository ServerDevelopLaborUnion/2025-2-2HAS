using AKH.Network;
using Core.EventSystem;
using DewmoLib.Dependencies;
using DewmoLib.Utiles;
using KHG.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KHG.UIs
{
    public class RoomUI : MonoBehaviour
    {
        [Inject] private PacketResponsePublisher _publisher;
        [SerializeField] private Transform roomHandleTrm;
        [SerializeField] private EventChannelSO packetChannel;
        [SerializeField] private EventChannelSO uiChannel;

        [SerializeField] private GameObject roomPrefab;

        private void Awake()
        {
            packetChannel.AddListener<RoomListEvent>(HandleRoomList);
            _publisher.AddListener(PacketID.C_RoomEnter, IsEnterSuccess);
        }
        private void OnDestroy()
        {
            packetChannel.RemoveListener<RoomListEvent>(HandleRoomList);
            _publisher.RemoveListener(PacketID.C_RoomEnter, IsEnterSuccess);
        }
        private void IsEnterSuccess(bool val)
        {
            if (val) return;

            WarnUiEvent warnUiEvent = UserInterfaceEvents.WarnUiEvent;
            warnUiEvent.Title = "오류";
            warnUiEvent.Message = "방 입장에 실패하였습니다.";

            uiChannel.InvokeEvent(warnUiEvent);
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
                StartCoroutine(Build(list[i], i * 0.07f));
            }
        }

        private IEnumerator Build(RoomInfoPacket roomInfo,float num)
        {
            yield return new WaitForSeconds(num);
            BuildRoom(roomInfo);
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

        private void RemoveChildren(Transform target)
        {
            foreach (Transform child in target)
            {
                Destroy(child.gameObject);//나중에 풀링 해야댐
            }
        }
    }
}
