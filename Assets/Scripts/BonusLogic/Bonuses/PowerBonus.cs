using Stats;
using UnityEngine;

namespace BonusLogic.Bonuses
{
    public class PowerBonus : BaseBonus
    {
        [SerializeField] float increasePowerValue;
        public override void Receive(BonusReceiver bonusReceiver)
        {
            if (bonusReceiver.TryGetComponent<StatsContainer>(out var statContainer))
            {
                statContainer.health.Increase(increasePowerValue);
            }
            base.Receive(bonusReceiver);
        }
    }
}
