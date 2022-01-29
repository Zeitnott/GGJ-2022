using System;
using System.Collections;
using UnityEngine;

namespace BonusLogic.Effects
{
	public class TimerController : Singleton<TimerController>
	{
		public event Action onSecondTick;
		private Coroutine _tickLoop;

		private void OnEnable()
		{
			_tickLoop = StartCoroutine(TickLoop());
		}

		private void OnDisable()
		{
			StopCoroutine(_tickLoop);
		}

		private IEnumerator TickLoop()
		{
			while (true)
			{
				yield return new WaitForSeconds(1f);
				onSecondTick?.Invoke();
			}
		}
	}
}