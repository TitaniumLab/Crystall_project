using CrystalProject.EventBus;
using UnityEngine;

namespace CrystalProject.Units.Create
{
    public class UnitFactory
    {
        private Unit _unitPrefab;
        private Transform _parentTransform;
        private int _unitTier;
        private bool _canBeCombined;
        private CustomUnityPool _pool;
        private int _counter = 0;
        private CustomEventBus _eventBus;

        public UnitFactory(Unit unitPrefab, Transform parentTransform, int unitTier, bool canBeCombined, CustomUnityPool pool, CustomEventBus customEventBus)
        {
            _unitPrefab = unitPrefab;
            _parentTransform = parentTransform;
            _unitTier = unitTier;
            _canBeCombined = canBeCombined;
            _pool = pool;
            _eventBus = customEventBus;
        }

        public Unit CreateUnit()
        {
            var unit = Object.Instantiate(_unitPrefab, _parentTransform);
            unit.Init(_unitTier, _pool, _canBeCombined, _eventBus);
            Debug.Log($"Unit {unit.name} ¹{_counter} created.");
            _counter++;
            return unit;
        }
    }
}