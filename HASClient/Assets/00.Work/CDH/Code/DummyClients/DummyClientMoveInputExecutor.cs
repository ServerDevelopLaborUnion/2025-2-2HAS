using Assets._00.Work.YHB.Scripts.Entities;
using Assets._00.Work.YHB.Scripts.ExecuteBehaviour;
using Assets._00.Work.YHB.Scripts.ExecuteBehaviour.DataTypes;
using DewmoLib.Utiles;
using System;
using UnityEngine;

namespace Assets._00.Work.CDH.Code.DummyClients
{
    public class DummyClientMoveInputExecutor : MonoBehaviour
    {
        [Header("Value")]
        [SerializeField] private EventChannelSO packetChannel;
        [SerializeField] private ScriptableBehaviourSO moveInputBehaviour;
        [SerializeField] private ScriptableBehaviourSO jumpInputBehaviour;

        [Header("Value")]
        [SerializeField] private EntityMovement entityMovement;

        private EntityMovementData _moveData;

        private void Awake()
        {
            _moveData = new EntityMovementData();
            _moveData.entityMovement = entityMovement;
        }

        private void Start()
        {
            packetChannel.AddListener<DummyClientMoveEventHandler>(MoveEventHandler);
        }

        private void MoveEventHandler(DummyClientMoveEventHandler evt)
        {
            Vector2 moveDirection = new Vector2(evt.direction.x, evt.direction.z);
            _moveData.moveDirection = moveDirection;
            // _moveData.moveRotation = evt.

            // jumpInputBehaviour.Execute<EntityMovementData>(_moveData);
            moveInputBehaviour.Execute<EntityMovementData>(_moveData);
        }
    }
}
