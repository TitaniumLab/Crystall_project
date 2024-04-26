using System;
using UnityEngine;
using Zenject;

public class LossController : MonoBehaviour
{
    [field: SerializeField] public Transform _lossBorder { get; private set; }
    [field: SerializeField] public float LossDelay { get; private set; }

    private void Awake()
    {
        UnitLossArea.OnLossTimeChange += CheckLoss;
    }


    private void CheckLoss(float time)
    {

    }
    //public static event Action<float> OnLossTimeChange;
}
