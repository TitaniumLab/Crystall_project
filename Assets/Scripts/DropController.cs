using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropController : MonoBehaviour, IDropper
{
    private Previewable _currentUnit;
    public event Action OnDrop;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _currentUnit?.EnablePreviewState(false);
            OnDrop();
            _currentUnit.EnablePreviewState(true);
        }
    }

    public void GetUnit(Previewable previewableUnit)
    {
        _currentUnit = previewableUnit;
    }
}
