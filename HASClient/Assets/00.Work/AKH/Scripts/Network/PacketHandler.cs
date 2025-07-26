using AKH.Scripts.Packet;
using Assets._00.Work.AKH.Scripts.Packet;
using Assets._00.Work.CDH.Code.DummyClients;
using Assets._00.Work.YHB.Scripts.Players;
using DewmoLib.Utiles;
using KHG.Managers;
using ServerCore;
using UnityEngine;

public partial class PacketHandler
{
    private EventChannelSO _packetChannel;
    private int _myIndex = -1;
    private bool _entered = false;
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
        if (!_entered)
            return;
        S_Move move = (S_Move)packet;
        if (move.index == _myIndex)
            return;
        Vector3 velocity = move.direction.ToVector3() * move.speed;
        //Debug.Log($"DummyMove: {velocity}");
        var other = EntityManager.Instance.GetObject<DummyClient>(move.index);
        other.transform.position = move.position.ToVector3();
        other.HandleDummyClientMove(velocity);
    }
    public void S_RotateHandler(PacketSession session, IPacket packet)
    {
        if (!_entered)
            return;
        S_Rotate rotate = (S_Rotate)packet;
        if (rotate.index == _myIndex)
            return;
        //Debug.Log($"DummyRotate: {rotate.rotation.ToQuaternion()}");
        var other = EntityManager.Instance.GetObject<DummyClient>(rotate.index);
        other.HandleDummyClientRotation(rotate.rotation.ToQuaternion());
    }
    public void S_RoomEnterFirstHandler(PacketSession session, IPacket packet) //나만 확인용
    {
        S_RoomEnterFirst first = (S_RoomEnterFirst)packet;
        _myIndex = first.myIndex;
        _entered = true;
        foreach (var item in first.inits)
        {
            if (item.index == first.myIndex)
            {
                EntityManager.Instance.CreateObject<Player>(
                    item.index,
                    ObjectType.Player,
                    item.position.ToVector3(),
                    out var player);
            }
            else
            {
                EntityManager.Instance.CreateObject<DummyClient>(
                    item.index,
                    ObjectType.OtherPlayer,
                    item.position.ToVector3(),
                    out var player);
                player.HandleDummyClientRotation(item.rotation.ToQuaternion());
            }
        }
    }
    public void S_RoomEnterHandler(PacketSession session, IPacket packet)// 전체 확인용
    {
        S_RoomEnter enter = (S_RoomEnter)packet;
        PlayerInitPacket init = enter.newPlayer;
        if (enter.newPlayer.index == _myIndex)
            return;
        EntityManager.Instance.CreateObject<DummyClient>(
            init.index,
            ObjectType.OtherPlayer,
            init.position.ToVector3(),
            out var player);
        player.HandleDummyClientRotation(init.rotation.ToQuaternion());
    }
}
