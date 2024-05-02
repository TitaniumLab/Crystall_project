using UnityEngine;

namespace CrystalProject.Loss
{
    public class LossArea : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out ILossIncreaser lossCount))
                lossCount.StartIncrease();
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out ILossIncreaser lossCount))
                lossCount.StopIncreaser();
        }
    }
}
