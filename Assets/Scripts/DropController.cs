using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class sDropController : MonoBehaviour, IDropper
{
    private Previewable _currentUnit;
    [SerializeField] private float _dorpHeight;
    [SerializeField] private Transform _leftBorder;
    [SerializeField] private float _leftBorderOffset;
    [SerializeField] private Transform _rightBorder;
    [SerializeField] private float _rightBorderOffset;

    public event Action OnAppear;
    public event Action<Transform, Vector3> OnMove;
    //public delegate Task ReleaseHandler(Transform transform, Vector3 position);
    //public event ReleaseHandler OnRelease;
    public event Action<IPreviewable, Transform, Vector3> OnRelease;
    //public event Action OnDrop;


    private void Start()
    {
        //OnDrop();
        _currentUnit?.EnablePreviewState(true);
    }

    private void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{

        //}
        //if (Input.GetMouseButton(0))
        //{
        //    OnMove?.Invoke(_currentUnit.transform, GetDropPoint());
        //}
        //else if (Input.GetMouseButtonUp(0))
        //{
        //    OnRelease?.Invoke(_currentUnit, _currentUnit.transform, GetDropPoint());

        //}
    }

    public void GetUnit(Previewable previewableUnit)
    {
        _currentUnit = previewableUnit;
    }

    //private void GoToDropHeight(Transform transform, float duration)
    //{
    //    Vector3 dropBorderPoint = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, _dorpHeight, 0);
    //    _tween = transform.DOMove(dropBorderPoint, _animDuration, false);
    //}



    //private void GoTo(Transform transform)
    //{
    //    _tween?.Kill(false);
    //    Vector3 dropPoint = GetDropPoint();
    //    _tween = transform.DOMove(dropPoint, _animDuration, false);
    //}

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

    private Vector3 GetDropPoint()
    {
        float leftBorder = _leftBorder.position.x + _leftBorder.lossyScale.x / 2 + _currentUnit.transform.lossyScale.x + _leftBorderOffset;
        float rightBorder = _rightBorder.position.x - _rightBorder.lossyScale.x / 2 - _currentUnit.transform.lossyScale.x - _rightBorderOffset;
        float xPos = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        if (xPos < leftBorder)
            xPos = leftBorder;
        else if (xPos > rightBorder)
            xPos = rightBorder;
        Vector3 dropPoint = new Vector3(xPos, _dorpHeight, 0);
        return dropPoint;
    }

}
