using DG.Tweening;
using System;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

public class sDropAnimator : MonoBehaviour, IAnim
{
    [SerializeField] private float _dragDuration;
    [SerializeField] private float _dropDelay;
    private IDropper _dropper;
    private Tween _tween;

    public event Action OnDrop;



    [Inject]
    private void Constructor(IDropper dropper)
    {
        _dropper = dropper;
        //_dropper.OnMove += GoTo;
        //_dropper.OnRelease += GoToAsync;
    }

    private void GoTo(Transform transform, Vector3 position)
    {
        _tween?.Kill(false);
        _tween = transform.DOMove(position, _dragDuration, false);
    }

    private async void GoToAsync(IPreviewable previewableUnit, Transform unitTransform, Vector3 position)
    {
        _tween?.Kill(false);
        await (_tween = unitTransform.DOMove(position, _dropDelay, false)).AsyncWaitForCompletion();
        Debug.Log("Drop");
        previewableUnit?.EnablePreviewState(false);
        OnDrop();
        previewableUnit.EnablePreviewState(true);
    }

    //private void 
    //private async void OnAnimationComplete(Transform transform)
    //{
    //    _tween?.Kill(false);

    //    Vector3 dropPoint = GetDropPoint();
    //    var task = (_tween = transform.DOMove(dropPoint, 0.1f, false)).AsyncWaitForCompletion();

    //    await task;
    //    _tween.Kill(false);
    //    _currentUnit?.EnablePreviewState(false);
    //    OnDrop();

    //    _currentUnit.EnablePreviewState(true);
    //}

}
