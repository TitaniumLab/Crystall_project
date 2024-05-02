using DG.Tweening;
using System;
using UnityEngine;


namespace CrystalProject.Dropper
{
    [RequireComponent(typeof(IDropHandler))]
    public class DropAnimator : MonoBehaviour
    {
        private IDropHandler _dropHandler;
        private Tween _tween;
        private bool _canBeMoved;
        [SerializeField] private float _appearDuration = 0.5f;
        [SerializeField] private float _moveDelay = 0.5f;
        [SerializeField] private float _dropDelay = 0.1f;
        public event Action OnDropEnd;


        private void Awake()
        {
            _dropHandler = GetComponent<IDropHandler>();
            _dropHandler.OnAppear += AppearAnimation;
            _dropHandler.OnMove += MoveTo;
            _dropHandler.OnDrop += Drop;
        }


        private async void AppearAnimation(Transform unitTransform)
        {
            _canBeMoved = false;
            Vector3 defaultSize = unitTransform.localScale;
            unitTransform.localScale = Vector3.zero;
            await (_tween = unitTransform.DOScale(defaultSize, _appearDuration).SetEase(Ease.OutBounce)).AsyncWaitForCompletion();
            _canBeMoved = true;
        }

        private void MoveTo(Transform unitTransform, Vector3 point)
        {
            if (_canBeMoved)
            {
                _tween?.Kill(false);
                _tween = unitTransform.DOMove(point, _moveDelay, false);
            }
        }

        private async void Drop(Transform unitTransform, Vector3 point)
        {
            if (_canBeMoved)
            {
                _tween?.Kill(false);
                await (_tween = unitTransform.DOMove(point, _dropDelay, false)).AsyncWaitForCompletion();
                OnDropEnd();
            }
        }
    }
}
