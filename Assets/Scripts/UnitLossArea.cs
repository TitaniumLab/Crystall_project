using System;
using UnityEngine;
using Zenject;

public class UnitLossArea : MonoBehaviour
{
    [SerializeField] private float _timeInZone;
    [SerializeField] private bool _inLossZone = false;
    public static event Action<float> OnLossTimeChange;


    private void OnTriggerEnter(Collider other)
    {
        _inLossZone = true;
    }
    private void OnTriggerExit(Collider other)
    {
        _inLossZone = false;
        _timeInZone = 0;
    }

    private void Update()
    {
        if (_inLossZone)
        {
            _timeInZone += Time.deltaTime;
            OnLossTimeChange(_timeInZone);
        }
    }


}
