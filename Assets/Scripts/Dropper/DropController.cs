using System;
using UnityEngine;

namespace CrystalProject.Dropper
{
    [RequireComponent(typeof(DropModel))]
    public class DropController : MonoBehaviour, IDropHandler
    {
        private DropModel _dropModel;
        [SerializeField] private int _mouseButtonIndex;
        public event Action<Transform> OnAppear;
        public event Action<Transform, Vector3> OnMove;
        public event Action<Transform, Vector3> OnDrop;

        private void Awake()
        {
            if (TryGetComponent(out DropModel dropModel))
                _dropModel = dropModel;
            else
                throw new Exception($"Missing {typeof(DropModel).Name} component.");

            _dropModel.OnUnitGet += OnAppear;
        }

        private void OnDestroy()
        {
            _dropModel.OnUnitGet -= OnAppear;
        }

        private void FixedUpdate()
        {
            if (Input.GetMouseButton(_mouseButtonIndex))
            {
                var dropPos = _dropModel.GetDropPosition();
                OnMove?.Invoke(_dropModel.CurrentUnitTransform, dropPos);
            }
        }

        private void Update()
        {
            if (Input.GetMouseButtonUp(_mouseButtonIndex))
            {
                var dropPos = _dropModel.GetDropPosition();
                OnDrop?.Invoke(_dropModel.CurrentUnitTransform, dropPos);
            }
        }
    }
}