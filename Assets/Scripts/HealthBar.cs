using Stats;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HealthBar : MonoBehaviour
{
	[SerializeField] private StatsContainer _stats;

    private Slider slider;
    private float _maxHealth;

    private void Awake()
    {
	    slider = GetComponent<Slider>();
	    _maxHealth = _stats.health.Value;

	    DrawHealth(_stats.health.Value);
    }

    private void OnEnable()
    {
	    _stats.health.onChangedStat += ChangeHealthHandler;
    }

    private void OnDisable()
    {
	    _stats.health.onChangedStat -= ChangeHealthHandler;
    }

    private void ChangeHealthHandler(float value)
    {
	    DrawHealth(value);
    }

    private void DrawHealth(float value)
    {
	    slider.value = value / _maxHealth;
    }
}
