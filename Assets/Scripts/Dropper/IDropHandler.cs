using System;
using UnityEngine;

namespace CrystalProject.Dropper
{
    public interface IDropHandler
    {
        public event Action<Transform> OnAppear;
        public event Action<Transform, Vector3> OnMove;
        public event Action<Transform, Vector3> OnDrop;
    }
}

