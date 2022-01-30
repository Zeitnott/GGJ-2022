using Stats;
using UnityEngine;

namespace BonusLogic.Bonuses
{
    public class AttackSpeedBonus : BaseBonus
    {
        [SerializeField] float increaseAttackSpeedValue;
        public override void Receive(BonusReceiver bonusReceiver)
        {
            if (bonusReceiver.TryGetComponent<StatsContainer>(out var statContainer))
            {
                statContainer.health.Increase(increaseAttackSpeedValue);
            }
            base.Receive(bonusReceiver);
        }
    }
}
