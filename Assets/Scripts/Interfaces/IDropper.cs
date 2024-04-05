using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDropper
{
    public void GetUnit(Previewable previewableUnit);
    public event Action OnDrop;
}
