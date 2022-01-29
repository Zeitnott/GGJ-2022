using BonusLogic;
using UnityEngine;

public abstract class BaseBonus : MonoBehaviour
{
    public virtual void Receive(BonusReceiver bonusReceiver)
    {
        Destroy(gameObject);
    }
}
