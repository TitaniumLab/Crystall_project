using System;
using UnityEngine;

namespace CrystalProject.Loss
{
    [RequireComponent(typeof(LossController))]
    public class LossView : MonoBehaviour
    {
        [SerializeField] private LossIndicator[] _lossIndicator;
        [SerializeField] private float _sensitivity; // Minimum value after which the indicator is triggered 
        private float _countPerIndicator;
        private LossController _controller;

        private void Awake()
        {
            if (!TryGetComponent(out _controller))
                throw new Exception($"Missing {typeof(LossController).Name} component.");
            _controller.OnLossCounterChange += TriggerLossIndicator;

            _countPerIndicator = _controller.LossDelayValue / _lossIndicator.Length;
        }

        private void OnDestroy()
        {
            _controller.OnLossCounterChange -= TriggerLossIndicator;
        }

        private void TriggerLossIndicator()
        {
            int lossIndex = (int)(_controller.CountValue / _countPerIndicator);
            if (lossIndex < _lossIndicator.Length)
            {
                float indicatorValue;
                if (lossIndex == 0 && _controller.CountValue > _sensitivity) // First indicator
                {
                    indicatorValue = (_controller.CountValue - _sensitivity) / (_countPerIndicator - _sensitivity);
                    _lossIndicator[lossIndex].SetIndicatorValue(indicatorValue);
                }
                else if (lossIndex != 0) // If not first indicator - dont use sensitivity
                {
                    indicatorValue = (_controller.CountValue / _countPerIndicator) - lossIndex;
                    _lossIndicator[lossIndex].SetIndicatorValue(indicatorValue);
                }
            }
        }
    }
}

