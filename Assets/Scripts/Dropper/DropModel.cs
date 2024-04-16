using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dropper.Model
{
    public class DropModel : MonoBehaviour
    {
        private Unit _currentUnit;
        [field: SerializeField] public Transform CurrentUnitTransform { get; private set; }
        [SerializeField] private Transform _appearPoint;
        [SerializeField] private float _dropHeight;
        [SerializeField] private Transform _leftBorder;
        [SerializeField] private float _leftBorderOffset;
        [SerializeField] private Transform _rightBorder;
        [SerializeField] private float _rightBorderOffset;
        public event Action<Transform> OnUnitGet;

        public void GetUnit(Unit unit)
        {
            _currentUnit?.DisablePreviewState();
            _currentUnit = unit;
            _currentUnit.EnablePreviewState();
            CurrentUnitTransform = _currentUnit.GetComponent<Transform>();
            CurrentUnitTransform.position = _appearPoint.position;
            OnUnitGet(CurrentUnitTransform);
        }

        public Vector3 GetDropPosition()
        {
            float leftBorder = _leftBorder.position.x + _leftBorder.lossyScale.x / 2 + CurrentUnitTransform.lossyScale.x + _leftBorderOffset;
            float rightBorder = _rightBorder.position.x - _rightBorder.lossyScale.x / 2 - CurrentUnitTransform.lossyScale.x - _rightBorderOffset;
            float xPos = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
            if (xPos < leftBorder)
                xPos = leftBorder;
            else if (xPos > rightBorder)
                xPos = rightBorder;
            Vector3 dropPoint = new Vector3(xPos, _dropHeight, 0);
            return dropPoint;
        }
    }
}
