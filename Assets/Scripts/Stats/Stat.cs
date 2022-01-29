using System;
using UnityEngine;

namespace Stats
{
    [Serializable]
    public class Stat
    {
        [SerializeField] 
        private StatType _type;
    }

    [Serializable]
    public abstract class Stat<T> : Stat
    {
        public T value => _value;
        
        [SerializeField] 
        protected T _value;
    }
}