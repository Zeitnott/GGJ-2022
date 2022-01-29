using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Stats;
public class AmmoBar : MonoBehaviour
{
    private Player player;
    private Slider slider;
    public PlayerContainer _stats;
    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        slider = GetComponent<Slider>();
        _stats = GetComponent<PlayerContainer>();
        slider.maxValue = _stats.ammo.Value;
    }
    private void OnEnable()
    {
        _stats.ammo.onChangedStat += ChangeAmmoHandler;
    }
    private void OnDisable()
    {
        _stats.ammo.onChangedStat -= ChangeAmmoHandler;
    }
    private void Update()
    {
        slider.value = _stats.ammo.Value;
    }
    private void ChangeAmmoHandler(float value)
    {
        slider.value = _stats.ammo.Value;
    }
}
