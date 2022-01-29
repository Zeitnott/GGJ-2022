using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoBar : MonoBehaviour
{
    private Player player;
    private Slider slider;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        slider = GetComponent<Slider>();

        slider.maxValue = player.Ammo;
    }

    private void Update()
    {
        slider.value = player.Ammo;
    }
}
