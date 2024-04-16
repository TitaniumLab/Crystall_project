using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dropper
{
    public interface IDropHandler
    {
        public event Action<Transform> OnAppear;
        public event Action<Transform, Vector3> OnMove;
        public event Action<Transform, Vector3> OnDrop;
    }
}

