using DG.Tweening;
using System;
using UnityEngine;


namespace CrystalProject.Dropper
{
    /// <summary>
    /// Animate a given object
    /// </summary>
    public class DropAnimator : MonoBehaviour
    {
        [SerializeField] private float _appearDuration = 0.5f;
        [SerializeField] private Vector3 _appearStartSize = Vector3.zero;
        [SerializeField] private float _moveDelay = 0.5f;
        [SerializeField] private float _dropDelay = 0.1f;
        private Tween _tween;
        private bool _canBeMoved;
        public bool CanBeMoved { get { return _canBeMoved; } }
        public event Action OnDropEnd;
        public event Action<bool> OnAppearAnimating;

        private void OnDestroy()
        {
            _tween.Kill();
        }

        /// <summary>
        /// Play appear animation.
        /// </summary>
        /// <param name="unitTransform"></param>
        /// <param name="appearPoint"></param>
        public async void AppearAnimation(Transform unitTransform, Vector3 appearPoint)
        {
            _canBeMoved = false;
            OnAppearAnimating?.Invoke(false);
            unitTransform.position = appearPoint;
            Vector3 defaultSize = unitTransform.localScale;
            unitTransform.localScale = _appearStartSize;
            await (_tween = unitTransform.DOScale(defaultSize, _appearDuration).SetEase(Ease.OutBounce)).AsyncWaitForCompletion();
            OnAppearAnimating?.Invoke(true);
            _canBeMoved = true;
        }

        /// <summary>
        /// Move object to position.
        /// </summary>
        /// <param name="unitTransform"></param>
        /// <param name="point"></param>
        public void MoveTo(Transform unitTransform, Vector3 point)
        {
            if (_canBeMoved)
            {
                _tween?.Kill(false);
                _tween = unitTransform.DOMove(point, _moveDelay, false);
            }
        }

        /// <summary>
        /// Drop unit on position.
        /// </summary>
        /// <param name="unitTransform"></param>
        /// <param name="point"></param>
        public async void Drop(Transform unitTransform, Vector3 point)
        {
            if (_canBeMoved)
            {
                _canBeMoved = false;
                _tween?.Kill(false);
                await (_tween = unitTransform.DOMove(point, _dropDelay, false)).AsyncWaitForCompletion();
                _canBeMoved = true;
                OnDropEnd?.Invoke();
            }
        }
    }
}
