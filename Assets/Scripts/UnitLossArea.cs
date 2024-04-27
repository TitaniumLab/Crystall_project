using UnityEngine;

public class UnitLossArea : MonoBehaviour, ILossCount, ILossSender
{
    public void StartCount()=>
        _isCount = true;

    public void StopCount()
    {
        ILossSender.Invoke(_timer);
    }
}
