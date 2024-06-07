using CrystalProject.EventBus;
using CrystalProject.EventBus.Signals;
using System;
using UnityEngine;
using Zenject;

namespace CrystalProject.Loss
{
    /// <summary>
    /// Counts down the time untill loss. 
    /// To show count progress use LossView.
    /// </summary>
    public class LossController : MonoBehaviour
    {
        [SerializeField] private float _lossDelayValue = 3;
        [SerializeField] private float _countValue;
        [SerializeField] private float _minValue;
        [SerializeField] private float _incValue;
        [SerializeField] private float _minIncValue = 1;
        [SerializeField] private float _decValue = 1;
        [SerializeField] private LossArea _lossArea;

        public float LossDelayValue { get { return _lossDelayValue; } }
        public float CountValue { get { return _countValue; } }

        private CustomEventBus _eventBus;
        public event Action OnLossCounterChange;

        #region MonoBeh
        private void Awake()
        {
            _lossArea.OnValueChanged += ChangeIncreminatorCount;
        }


        private void OnDestroy()
        {
            _lossArea.OnValueChanged -= ChangeIncreminatorCount;
        }

        private void FixedUpdate()
        {
            if (_incValue >= _minIncValue && _countValue < _lossDelayValue)
            {
                _countValue += Time.deltaTime * _incValue;
                OnLossCounterChange();
                if (_countValue >= _lossDelayValue)
                {
                    _eventBus.Invoke(new GameOverSignal());
                    Debug.LogWarning("You lose!");
                }
            }
            else if (_countValue > _minValue && _countValue < _lossDelayValue)
            {
                _countValue -= Time.deltaTime * _decValue;
                OnLossCounterChange();
                if (_countValue < _minValue)
                    _countValue = _minValue;
            }
        }
        #endregion

        #region Methods
        [Inject]
        private void Construct(CustomEventBus customEventBus)
        {
            _eventBus = customEventBus;
        }

        private void ChangeIncreminatorCount() // There was a state machine, but the loss controller got smaller
        {
            _incValue = _lossArea.TotalLossInc;
        }
        #endregion
    }
}