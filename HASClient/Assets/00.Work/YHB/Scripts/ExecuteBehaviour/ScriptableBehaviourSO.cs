using UnityEngine;

namespace Assets._00.Work.YHB.Scripts.ExecuteBehaviour
{
	[CreateAssetMenu(fileName = "BehaviourSO", menuName = "SO/ScriptableBehaviour/Behaviour", order = 0)]
	public abstract class ScriptableBehaviourSO : ScriptableObject
	{
		/// <summary>
		/// 명령 단위를 실행합니다.
		/// </summary>
		/// <typeparam name="T">처리할 데이터 타입 (여러 데이터를 처리해야한다면 랩핑하십시오.)</typeparam>
		/// <param name="data">실제로 넣을 데이터</param>
		/// <returns>동작의 성공 여부</returns>
		public abstract bool Execute<T>(T data);
	}
}
