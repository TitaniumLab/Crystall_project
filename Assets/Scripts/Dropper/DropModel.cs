using CrystalProject.Units;
using System;
using UnityEngine;

namespace CrystalProject.Dropper
{
    public class DropModel : MonoBehaviour
    {
        private IPreview _currentUnitPreview;
        [field: SerializeField] public Transform CurrentUnitTransform { get; private set; }
        [SerializeField] private Transform _appearPoint;
        [SerializeField] private float _zDropPos;
        [SerializeField] private float _dropHeight;
        [SerializeField] private Transform _leftBorder;
        [SerializeField] private float _leftBorderOffset;
        [SerializeField] private Transform _rightBorder;
        [SerializeField] private float _rightBorderOffset;
        public event Action<Transform> OnUnitGet;

        public void GetUnit(Transform unitTransform)
        {
            _currentUnitPreview?.DisablePreviewState();
            CurrentUnitTransform = unitTransform;
            if (CurrentUnitTransform.TryGetComponent(out IPreview preview)) _currentUnitPreview = preview;
            else throw new Exception($"Missing {typeof(IPreview).Name} component.");
            _currentUnitPreview.EnablePreviewState();
            CurrentUnitTransform.position = _appearPoint.position;
            OnUnitGet?.Invoke(CurrentUnitTransform);
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
            Vector3 dropPoint = new Vector3(xPos, _dropHeight, _zDropPos);
            return dropPoint;
        }
    }
}
