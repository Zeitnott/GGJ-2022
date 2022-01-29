using UnityEngine;

namespace BonusLogic.Effects
{
	public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
	{
		private static T _instance;

		public static T Instance
		{
			get
			{
				_instance = FindObjectOfType<T>();

				if (_instance == null)
				{
					var go = new GameObject("[SINGLETON]" + typeof(T));
					_instance = go.AddComponent<T>();
				}

				return _instance;
			}
		}
	}
}