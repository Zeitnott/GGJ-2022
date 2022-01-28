using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PlayerController : MonoBehaviour
{
    public static event Action OnSwitchMode;
    public void SwitchMode()
    {
        OnSwitchMode?.Invoke();
    }
}
