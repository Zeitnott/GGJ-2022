using System;

namespace Stats
{
    public class FloatStat : Stat<float>
    {
        public event Action<float> onChangedStat;
        
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