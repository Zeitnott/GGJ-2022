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
                statContainer.power.Increase(increasePowerValue);
            }
            base.Receive(bonusReceiver);
        }
    }
}
