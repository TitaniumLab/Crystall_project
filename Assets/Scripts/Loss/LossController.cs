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
        public float LossDelayValue { get { return _lossDelayValue; } }
        [SerializeField] private float _countValue;
        public float CountValue { get { return _countValue; } }
        [SerializeField] private float _minValue;
        [SerializeField] private bool _isLossing = false;
        [SerializeField] private float _minIncValue = 1;
        [SerializeField] private float _decreasingValue = 1;
        [SerializeField] private LossArea _lossArea;
        private CustomEventBus _eventBus;
        public event Action OnLossCounterChange;



        #region MonoBeh
        private void Awake()
        {
            _lossArea.OnValueChanged += LossCount;
        }

        private void OnDestroy()
        {
            _lossArea.OnValueChanged -= LossCount;
        }

        private void FixedUpdate()
        {
            if (_isLossing && _countValue < _lossDelayValue)
            {
                _countValue += Time.deltaTime * _lossArea.TotalLossInc;
                OnLossCounterChange();
                if (_countValue >= _lossDelayValue)
                {
                    _eventBus.Invoke(new GameOverSignal());
                    Debug.LogWarning("You lose!");
                }
            }
            else if (_countValue > _minValue && _countValue < _lossDelayValue)
            {
                _countValue -= Time.deltaTime * _decreasingValue;
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

        private void LossCount() // There was a state machine, but the loss controller got smaller
        {
            if (_lossArea.TotalLossInc >= _minIncValue)
                _isLossing = true;
            else
                _isLossing = false;
        }
        #endregion
    }
}