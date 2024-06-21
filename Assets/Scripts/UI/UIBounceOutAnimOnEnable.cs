using DG.Tweening;
using UnityEngine;

namespace CrystalProject.UI
{
    [RequireComponent(typeof(RectTransform))]
    public class UIBounceOutAnimOnEnable : MonoBehaviour
    {
        [SerializeField] private float _startAnimSize = 0;
        private float _defautlAnimSize;
        [SerializeField] private float _animDuration = 0.5f;
        private RectTransform _rectTransform;
        private Tween _tween;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _defautlAnimSize = _rectTransform.localScale.x;
        }

        private void Start()
        {
            PlayAnim();
        }

        private void OnDisable()
        {
            _tween.Kill(true);
        }

        private void OnDestroy()
        {
            _tween.Kill();
        }

        private void PlayAnim()
        {
            _tween = _rectTransform.DOScale(_startAnimSize, 0);
            _tween = _rectTransform.DOScale(_defautlAnimSize, _animDuration).SetEase(Ease.InOutBounce);
        }
    }
}

