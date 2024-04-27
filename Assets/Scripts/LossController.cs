using UnityEngine;
using UnityEngine.UI;

public class LossController : MonoBehaviour
{
    [SerializeField] private float _lossDelay;
    [SerializeField] private Slider _slider; //slider for debugging------------
    [SerializeField] private float _maxTime;
    [SerializeField] private float _counterDecreaseMulti;
    [SerializeField] private float _counterIncreaseMulti;

    private void Awake()
    {
        ILossSender.LossCountChanged += CheckLoss;

        _slider.maxValue = _lossDelay;//-----------------
    }

    private void CheckLoss(float time)
    {
        if(time == 0 )
            _maxTime
        _slider.value = time;//-----------------
    }
}
