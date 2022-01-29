namespace BonusLogic.Effects
{
	public abstract class TimerBaseEffect
	{
		private float _timer;
		private float _duration;

		protected void ApplyEffect(float duration)
		{
			_duration = duration;
		}

		public abstract void DisableEffect();

		public bool Tick(float time)
		{
			_timer += time;
			if (_timer >= _duration)
			{
				ClearData();
				DisableEffect();
				return false;
			}

			return true;
		}

		private void ClearData()
		{
			_timer = 0;
			_duration = 0;
		}
	}
}