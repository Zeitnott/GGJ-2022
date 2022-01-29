namespace BonusLogic.Effects
{
	public abstract class TimerFloatStatEffect : TimerBaseEffect
	{
		private float _increaseValue;
		private IStatsEffectReceiver _receiver;

		public void ApplyEffect(IStatsEffectReceiver receiver, float increaseValue, float duration)
		{
			ApplyEffect(duration);

			_increaseValue = increaseValue;
			_receiver = receiver;
			IncreaseFloatValue(_receiver, _increaseValue);
		}

		protected abstract void IncreaseFloatValue(IStatsEffectReceiver receiver, float value);
		protected abstract void DecreaseFloatValue(IStatsEffectReceiver receiver, float value);

		public override void DisableEffect()
		{
			DecreaseFloatValue(_receiver, _increaseValue);
		}
	}
}