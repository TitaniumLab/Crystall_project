using UnityEngine;

namespace CrystalProject.Loss
{
    public class UnitLossArea : MonoBehaviour, ILossIncreaser, ILossSender
    {
        [SerializeField] private float _lossIncreseValue = 1;
        [SerializeField] private bool _inLossZone = false;

        private void OnDisable()
        {
            if (_inLossZone)
                StopIncreaser();
        }

        public void StartIncrease()
        {
            ILossSender.Invoke(_lossIncreseValue);
            _inLossZone = true;
        }

        public void StopIncreaser()
        {
            ILossSender.Invoke(-_lossIncreseValue);
            _inLossZone = false;
        }
    }

}
