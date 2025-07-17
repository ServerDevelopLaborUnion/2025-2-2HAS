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
        [SerializeField] private ScriptableBehaviourSO moveInputBehaviour;
        [SerializeField] private ScriptableBehaviourSO jumpInputBehaviour;

        [Header("Value")]
        [SerializeField] private DummyClient dummyClient;
        [SerializeField] private EntityMovement entityMovement;

        private EntityMovementData _moveData;
        private Quaternion _rotation;
        private Vector2 _moveDirection;

        private void Awake()
        {
            _moveData = new EntityMovementData();
            _moveData.entityMovement = entityMovement;
            _moveData.moveRotation = Quaternion.identity;
            _moveData.moveDirection = Vector2.zero;

            dummyClient.OnMoveEvent += MoveHandler;
            dummyClient.OnRotationEvent += RotationHandler;
        }

        private void RotationHandler(DummyClientRotationEventHandler evt)
        {
            _rotation = new Quaternion(evt.rotation.x, evt.rotation.y, evt.rotation.z, evt.rotation.w);
            _moveData.moveRotation = _rotation;

            jumpInputBehaviour.Execute<EntityMovementData>(_moveData);
        }

        private void MoveHandler(DummyClientMoveEventHandler evt)
        {
            _moveDirection = new Vector2(evt.direction.x, evt.direction.z);
            _moveData.moveDirection = _moveDirection;

            moveInputBehaviour.Execute<EntityMovementData>(_moveData);
        }

        // 점프는 나중에 연결
    }
}
