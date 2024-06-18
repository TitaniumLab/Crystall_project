using System;
using UnityEngine;

namespace CrystalProject.Loss
{
    public class LossArea : MonoBehaviour
    {
        private bool _isTriggered = false;

        public event Action IsTriggered;
        public event Action IsNotTriggered;

        // OnTriggerEnter/Exit works incorrect (Coz of pool system)
        // Order of execution: OnTriggerStay => Update
        // If get GameUnit in LossArea call IsTriggered event and prevent other GameUnit to trigger it
        // if the GameUnit is not detected call IsNotTriggered event
        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent(out UnitLossArea component) && !_isTriggered)
            {
                IsTriggered?.Invoke();
                _isTriggered = true;
            }
        }

        private void FixedUpdate()
        {
            if (_isTriggered)
            {
                _isTriggered = false;
            }
            else
            {
                IsNotTriggered?.Invoke();
            }

        }
    }
}
