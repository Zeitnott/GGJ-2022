using Stats;
using UnityEngine;

namespace BonusLogic.Bonuses
{
	public class HealthBonus : BaseBonus
    {
        [SerializeField] 
        private float _increaseHealthValue;
        
        public override void Receive(BonusReceiver bonusReceiver)
        {
            if(bonusReceiver.TryGetComponent<StatsContainer>(out var statContainer))
            {
                statContainer.health.Increase(_increaseHealthValue);
            }
            base.Receive(bonusReceiver);
        }
    }
}