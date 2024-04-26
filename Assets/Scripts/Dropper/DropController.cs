using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CrystalProject.Dropper
{
    [RequireComponent(typeof(DropModel))]
    public class DropController : MonoBehaviour, IDropHandler
    {
        private DropModel _dropModel;
        public event Action<Transform> OnAppear;
        public event Action<Transform, Vector3> OnMove;
        public event Action<Transform, Vector3> OnDrop;

        private void Awake()
        {
            _dropModel = GetComponent<DropModel>();
            _dropModel.OnUnitGet += OnAppear;
        }

        private void FixedUpdate()
        {
            if (Input.GetMouseButton(0))
            {
                var dropPos = _dropModel.GetDropPosition();
                OnMove(_dropModel.CurrentUnitTransform, dropPos);
            }
        }

        private void Update()
        {
            if (Input.GetMouseButtonUp(0))
            {
                var dropPos = _dropModel.GetDropPosition();
                OnDrop(_dropModel.CurrentUnitTransform, dropPos);
            }
        }
    }
}