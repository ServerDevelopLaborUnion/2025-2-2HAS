using AKH.Network;
using Assets._00.Work.AKH.Scripts.Packet;
using Assets._00.Work.YHB.Scripts.Entities;
using Assets._00.Work.YHB.Scripts.Events;
using DewmoLib.Utiles;
using System;
using UnityEngine;

namespace AKH.Scripts.Test
{
    [DefaultExecutionOrder(-10)]
    public class MovePacketSender : MonoBehaviour
    {
        [SerializeField] private EventChannelSO gameChannel;
        private float _speed;
        private Vector2 _currentDirection;
        private EntityMovement _movement;

        private void Awake()
        {
            gameChannel.AddListener<RotateEvent>(HandleRotate);
            gameChannel.AddListener<MoveSpeedChangeEvent>(HandleSpeedChange);
            gameChannel.AddListener<MoveDirectionChangeEvent>(HandleDirectionChange);
        }

        private void HandleDirectionChange(MoveDirectionChangeEvent @event)
        {
            _currentDirection = @event.inputDirection;
            _movement = @event.entityMovement;
            SendMovePacket();
        }

        private void HandleSpeedChange(MoveSpeedChangeEvent @event)
        {
            _speed = @event.newMoveSpeed;
            _movement = @event.entityMovement;
            SendMovePacket();
        }
        private void SendMovePacket()
        {
            Console.WriteLine(_speed);
            C_Move move = new()
            {
                direction = _currentDirection.ToPacket(),
                position = _movement.transform.position.ToPacket(),
                speed = _speed
            };
            NetworkManager.Instance.SendPacket(move);

        }
        private void HandleRotate(RotateEvent @event)
        {
            C_Rotate rotate = new()
            {
                rotation = @event.rotateValue.ToPacket()
            };
            NetworkManager.Instance.SendPacket(rotate);
        }
    }
}
