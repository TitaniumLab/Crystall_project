using UnityEngine;

public class LossArea : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out ILossCount lossCount))
            lossCount.StartCount();
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out ILossCount lossCount))
            lossCount.StopCount();
    }
}
