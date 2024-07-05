using DG.Tweening;
using System.Drawing;
using UnityEngine;

namespace CrystalProject.UI
{
    public class LoadingScreenAnimator : MonoBehaviour
    {
        [SerializeField] private RectTransform _midElement;
        [SerializeField] private float _amplitude = 1.1f;
        [SerializeField] private float _duration = 1f;
        [SerializeField] private RectTransform _sideElement;
        [SerializeField] private float _rotationSpeed = 60;

        private void Start()
        {
            _midElement.DOScale(_amplitude, _duration).SetLoops(-1, LoopType.Yoyo);

            float dur = 360 / _rotationSpeed;
            _sideElement.DOLocalRotate(new Vector3(0, 0, 360), dur, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1);
        }

        private void OnDestroy()
        {
            _midElement.DOKill();
            _sideElement.DOKill();
        }
    }
}