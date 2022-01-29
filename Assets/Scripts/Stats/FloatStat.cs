using System;
using UnityEngine;

namespace Stats
{
	[Serializable]
	public class FloatStat
    {
	    public event Action<float> onChangedStat;

	    public float Value => _value;

	    [SerializeField]
	    private float _value;

	    public void Increase(float increaseValue)
        {
            _value += increaseValue;
            onChangedStat?.Invoke(_value);
        }

        public void Decrease(float decreaseValue)
        {
            _value -= decreaseValue;
            onChangedStat?.Invoke(_value);
        }
    }
}