using System;
using UnityEngine;

namespace CrystalProject.Loss
{
    public class LossArea : MonoBehaviour, ILossIncrementer
    {
        private float _totalIncrementer;
        public float TotalLossInc
        {
            get { return _totalIncrementer; }
            set
            {
                _totalIncrementer = value;
                OnValueChanged?.Invoke();
            }
        }
        public event Action OnValueChanged;
    }
}
