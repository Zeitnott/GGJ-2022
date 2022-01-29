using System.Collections.Generic;
using UnityEngine;

namespace BonusLogic.Effects
{
	public class EffectController : MonoBehaviour
	{
		private Dictionary<EffectType, TimerBaseEffect> _activeEffects = new Dictionary<EffectType, TimerBaseEffect>();

		private void OnEnable()
		{
			TimerController.Instance.onSecondTick += SecondTickHandler;
		}

		private void OnDisable()
		{
			TimerController.Instance.onSecondTick -= SecondTickHandler;
		}

		public void ApplyEffect(IStatsEffectReceiver receiver, EffectType type, float increaseValue, float duration)
		{
			if (_activeEffects.TryGetValue(type, out var oldEffect))
			{
				oldEffect.DisableEffect();
			}

			_activeEffects.Remove(type);

			var newEffect = GetEffectByType(type);
			if (newEffect != null)
			{
				newEffect.ApplyEffect(receiver, increaseValue, duration);
				_activeEffects.Add(type, newEffect);
			}
		}

		private void SecondTickHandler()
		{
			var endedEffectsType = new List<EffectType>();

			foreach (var pair in _activeEffects)
			{
				var canTick = pair.Value.Tick(1);
				if (!canTick)
				{
					endedEffectsType.Add(pair.Key);
				}
			}

			foreach (var endedEffectType in endedEffectsType)
			{
				_activeEffects.Remove(endedEffectType);
			}
		}

		private TimerFloatStatEffect GetEffectByType(EffectType type)
		{
			switch (type)
			{
				case EffectType.UpSpeed:
					return new TimerSpeedStatEffect();
			}

			return null;
		}
	}
}