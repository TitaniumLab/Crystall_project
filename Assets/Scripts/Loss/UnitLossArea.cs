using UnityEngine;

namespace CrystalProject.Loss
{
    public class UnitLossArea : MonoBehaviour
    {
        [SerializeField] private float _lossIncValue = 1;
        private ILossIncrementer _lossIncrementer;


        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out _lossIncrementer))
            {
                _lossIncrementer.TotalLossInc += _lossIncValue;
            }

        }
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out ILossIncrementer lossCount) && _lossIncrementer is not null)
            {
                _lossIncrementer.TotalLossInc -= _lossIncValue;
                _lossIncrementer = null;
            }
        }

        private void OnDisable()
        {
            if (_lossIncrementer is not null)
            {
                _lossIncrementer.TotalLossInc -= _lossIncValue;
                _lossIncrementer = null;
            }
        }
    }
}