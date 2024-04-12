using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Dropper.Animator
{
    [RequireComponent(typeof(IDropHandler))]
    public class DropAnimator : MonoBehaviour
    {
        private IDropHandler _dropHandler;
        private Tween _tween;
        private bool _canBeMoved;
        [SerializeField] private float _appearDuration = 0.5f;


        private void Awake()
        {
            _dropHandler = GetComponent<IDropHandler>();
            _dropHandler.OnAppear += AppearAnimation;
        }

        private async void AppearAnimation(Transform dTransform)
        {
            _canBeMoved = false;
            Vector3 defaultSize = dTransform.localScale;
            dTransform.localScale = Vector3.zero;
            await (_tween = dTransform.DOScale(defaultSize, _appearDuration).SetEase(Ease.InBounce)).AsyncWaitForCompletion();
            _canBeMoved = true;
        }
    }
}
