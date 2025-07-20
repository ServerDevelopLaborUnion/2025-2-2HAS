using AKH.Scripts.Packet;
using Assets._00.Work.AKH.Scripts.Packet;
using Assets._00.Work.CDH.Code.DummyClients;
using DewmoLib.Utiles;
using KHG.Managers;
using ServerCore;
using System;
using UnityEngine;

public partial class PacketHandler
{
    private EventChannelSO _packetChannel;
    public PacketHandler(EventChannelSO packetChannel)
    {
        _packetChannel = packetChannel;
    }

    internal void S_PacketResponseHandler(PacketSession session, IPacket packet)
    {
        var response = (S_PacketResponse)packet;
        var evt = PacketEvents.PacketResponse;
        evt.success = response.success;
        evt.packetId = (PacketID)response.responsePacket;
        _packetChannel.InvokeEvent(evt);
    }
    public void S_MoveHandler(PacketSession session, IPacket packet)
    {
        S_Move move = (S_Move)packet;
        Vector3 velocity = move.direction.ToVector3() * move.speed;
        var other = EntityManager.Instance.GetObject<DummyClient>(move.index);
        other.HandleDummyClientMove(velocity);
    }
    public void S_RotateHandler(PacketSession session, IPacket packet)
    {
        S_Rotate rotate = (S_Rotate)packet;
        var other =EntityManager.Instance.GetObject<DummyClient>(rotate.index);
        other.HandleDummyClientRotation(rotate.rotation.ToQuaternion());
    }
    public void S_RoomEnterFirstHandler(PacketSession session, IPacket packet) //나만 확인용
    {
        S_RoomEnterFirst first = (S_RoomEnterFirst)packet;
        foreach(var item in first.inits)
        {
        }
    }
}
