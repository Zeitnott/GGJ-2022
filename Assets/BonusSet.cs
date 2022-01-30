using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class BonusSet : MonoBehaviour
{
    public static Action OnBonusPickUped;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            OnBonusPickUped?.Invoke();
        }
    }
}
