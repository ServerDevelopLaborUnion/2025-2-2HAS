using System.Linq;
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
		public virtual bool Execute<T>(T data)
		{
			bool result = LogicExecute<T>(data);
#if UNITY_EDITOR
			DebugExecute(data, result);
#endif
			return result;
		}

		/// <summary>
		/// 실질적인 데이터를 받고 실행시킵니다.
		/// </summary>
		/// <typeparam name="T">처리할 데이터 타입 (여러 데이터를 처리해야한다면 랩핑하십시오.)</typeparam>
		/// <param name="data">실제로 넣을 데이터</param>
		/// <returns>동작의 성공 여부</returns>
		protected abstract bool LogicExecute<T>(T data);


		/// <summary>
		/// 데이터와 실행결과를 받고 디버깅을 합니다.
		/// </summary>
		/// <param name="data">사용된 데이터 안 걸러진 들어온 자체의 데이터</param>
		/// <param name="logicResult">로직의 결과</param>
		protected virtual void DebugExecute<T>(T data, bool logicResult)
		{
			Debug.Log($"{this.name} : data type : {data.GetType().ToString().Split('.').Last()} / Logic Success : {logicResult}");
		}
	}
}
