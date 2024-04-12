using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public interface IDropper
{
    public void GetUnit(Previewable previewableUnit);


    //public event Action OnAppear;
    //public event Action<Transform, Vector3> OnMove;
    ////public delegate Task ReleaseHandler (Transform transform, Vector3 position);
    ////public event ReleaseHandler OnRelease;
    //public event Action<IPreviewable, Transform, Vector3> OnRelease;
    //public event Action OnDrop;
}
