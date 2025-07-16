using Assets._00.Work.YHB.Scripts.Entities;
using UnityEngine;

namespace Assets._00.Work.YHB.Scripts.ExecuteBehaviour.DataTypes
{
	public class CameraValueChangeData
	{
		public EntityMovement entityMovementComp;
		public Transform cameraParent;
		public Vector2 cameraRotateValue;
		public float zoomValue;
		public float rotationXMin;
		public float rotationXMax;
	}
}
