using Dropper.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dropper.Controller
{
    [RequireComponent(typeof(DropModel))]
    public class DropController : MonoBehaviour, IDropHandler
    {
        private DropModel _dropModel;
        public event Action<Transform> OnAppear;
        public event Action<Transform> OnMove;
        public event Action<Transform> OnRelease;

        private void Awake()
        {
            _dropModel = GetComponent<DropModel>();
        }

        
    }
}