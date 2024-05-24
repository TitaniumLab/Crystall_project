using CrystalProject.EventBus;
using CrystalProject.EventBus.Signals;
using CrystalProject.Units;
using CrystalProject.Units.Create;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace CrystalProject.Dropper
{
    public class DropModel : MonoBehaviour
    {
        [SerializeField] private Transform _appearPoint;
        [SerializeField] private float _zDropPos;
        [SerializeField] private float _dropHeight;
        [SerializeField] private Transform _leftBorder;
        [SerializeField] private float _leftBorderOffset;
        [SerializeField] private Transform _rightBorder;
        [SerializeField] private float _rightBorderOffset;
        private IPreview _currentUnitPreview;
        private Transform _ñurtUnitTransform;
        public Transform CurrentUnitTransform { get => _ñurtUnitTransform; }
        private IUnitDispenser _unitDispenser;
        private CustomEventBus _eventBus;
        private IDropData[] _dropData;

        public event Action<Transform> OnUnitGet;

        [Inject]
        private void Construct(IUnitDispenser unitDispenser, CustomEventBus eventBus, IDropData[] dropData)
        {
            _unitDispenser = unitDispenser;
            _eventBus = eventBus;
            _dropData = dropData;
        }

        private void Awake()
        {
            _eventBus.Subscribe<GameStartSignal>(OnGameStart);
        }

        private void OnGameStart(GameStartSignal signal)
        {
            _currentUnitPreview?.DisablePreviewState();
            int tier = GetRandomUnitTier();
            _ñurtUnitTransform = _unitDispenser.GetUnit(tier).transform;
            if (_ñurtUnitTransform.TryGetComponent(out IPreview preview)) _currentUnitPreview = preview;
            else throw new Exception($"Missing {typeof(IPreview).Name} component.");
            _currentUnitPreview.EnablePreviewState();
        }



        private int GetRandomUnitTier()
        {
            List<int> dropUnitTiers = new List<int>();
            for (int i = 0; i < _dropData.Length; i++)
            {
                if (_dropData[i].CanBeDropped)
                    dropUnitTiers.Add(i);
            }
            int index;
            if (dropUnitTiers.Count > 0)
                index = Random.Range(0, dropUnitTiers.Count);
            else
                throw new Exception("Can't get game unit tier.");
            return dropUnitTiers[index];
        }

        public void GetUnit(Transform unitTransform)
        {
            //_currentUnitPreview?.DisablePreviewState();
            //_ñurtUnitTransform = unitTransform;
            //if (_ñurtUnitTransform.TryGetComponent(out IPreview preview)) _currentUnitPreview = preview;
            //else throw new Exception($"Missing {typeof(IPreview).Name} component.");
            //_currentUnitPreview.EnablePreviewState();
            //_ñurtUnitTransform.position = _appearPoint.position;
            //OnUnitGet?.Invoke(_ñurtUnitTransform);
        }

        public Vector3 GetDropPosition()
        {
            //float leftBorder = _leftBorder.position.x + _leftBorder.lossyScale.x / 2 + _ñurtUnitTransform.lossyScale.x + _leftBorderOffset;
            //float rightBorder = _rightBorder.position.x - _rightBorder.lossyScale.x / 2 - _ñurtUnitTransform.lossyScale.x - _rightBorderOffset;
            //float xPos = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
            //if (xPos < leftBorder)
            //    xPos = leftBorder;
            //else if (xPos > rightBorder)
            //    xPos = rightBorder;
            //Vector3 dropPoint = new Vector3(xPos, _dropHeight, _zDropPos);
            //return dropPoint;
        }
    }
}
