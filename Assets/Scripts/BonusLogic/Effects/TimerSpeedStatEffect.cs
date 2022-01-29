namespace BonusLogic.Effects
{
	public class TimerSpeedStatEffect : TimerFloatStatEffect
	{
		protected override void IncreaseFloatValue(IStatsEffectReceiver receiver, float value)
		{
			receiver.stats.speed.Increase(value);
		}

		protected override void DecreaseFloatValue(IStatsEffectReceiver receiver, float value)
		{
			receiver.stats.speed.Decrease(value);
		}
	}
}