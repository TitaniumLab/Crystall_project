using CrystalProject.Units;
using System;
using UnityEngine;
using Zenject;

namespace CrystalProject.Dropper
{
    /// <summary>
    /// Contains all information for dropping a game unit
    /// </summary>
    public class DropModel : MonoBehaviour
    {
        // Position data
        [SerializeField] private float _dropHeight = 5.5f;
        public float DropHeight { get { return _dropHeight; } }
        [SerializeField] private Transform _leftBorder;
        [SerializeField] private float _leftBorderOffset;
        [SerializeField] private Transform _rightBorder;
        [SerializeField] private float _rightBorderOffset;
        [field: SerializeField] public Transform AppearPoint { get; private set; }
        public float MinXValue { get; private set; } // Distance from left border
        public float MaxXValue { get; private set; } // Distance from right border

        // Current unit data
        private IPreview _curUnitPreview;
        public IPreview CurUnitPreview { get { return _curUnitPreview; } }
        private Transform _curUnitTransform;
        public Transform CurUnitTransform { get { return _curUnitTransform; } }
        public IDropData[] DropData { get; private set; }


        [Inject] // Dependency injection
        private void Construct(IDropData[] dropData)
        {
            DropData = dropData;
        }

        /// <summary>
        /// Set new game unit for dropper.
        /// </summary>
        /// <param name="unitTransform">Transform of setted unit.</param>
        /// <exception cref="Exception"></exception>
        public void SetNewUnit(Transform unitTransform)
        {
            _curUnitTransform = unitTransform;
            if (!_curUnitTransform.TryGetComponent(out _curUnitPreview))
                throw new Exception($"Missing {typeof(IPreview).Name} component.");
            MinXValue = _leftBorder.position.x + _leftBorder.lossyScale.x / 2 + _curUnitTransform.lossyScale.x + _leftBorderOffset;
            MaxXValue = _rightBorder.position.x - _rightBorder.lossyScale.x / 2 - _curUnitTransform.lossyScale.x - _rightBorderOffset;
        }
    }
}
