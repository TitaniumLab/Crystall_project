using CrystalProject.EventBus;
using CrystalProject.EventBus.Signals;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CrystalProject.Loss
{
    public class LossController : MonoBehaviour
    {
        [SerializeField] private float _lossDelayValue = 3;
        [SerializeField] private float _currentValue;
        [SerializeField] private float _minValue;
        [SerializeField] private bool _isLossing = false;
        [SerializeField] private float _minIncValue = 1;
        [SerializeField] private float _decreasingValue = 1;
        [SerializeField] private LossArea _lossArea;
        private CustomEventBus _eventBus;



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
            if (_isLossing)
            {
                _currentValue += Time.deltaTime * _lossArea.TotalLossInc;
                if (_currentValue > _lossDelayValue)
                    _eventBus.Invoke(new GameOverSignal());
            }
            else if (_currentValue > _minValue)
            {
                _currentValue -= Time.deltaTime * _decreasingValue;
                if (_currentValue < _minValue)
                    _currentValue = _minValue;
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