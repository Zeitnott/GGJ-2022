using UnityEngine;

namespace BonusLogic
{
    public class BonusReceiver : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<BaseBonus>(out var bonus))
            {
                bonus.Receive(this);
            }
        }
    }
}