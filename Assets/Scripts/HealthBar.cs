using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]

public class HealthBar : MonoBehaviour
{
    private Player player;
    private Slider slider;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        slider = GetComponent<Slider>();

        slider.maxValue = player.Health;
    }

    private void Update()
    {
        slider.value = player.Health;
    }
}
