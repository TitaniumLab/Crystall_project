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

        public UnitFactory(Unit unitPrefab, Transform parentTransform, int unitTier, bool canBeCombined, CustomUnityPool pool)
        {
            _unitPrefab = unitPrefab;
            _parentTransform = parentTransform;
            _unitTier = unitTier;
            _canBeCombined = canBeCombined;
            _pool = pool;
        }

        public Unit CreateUnit()
        {
            var unit = Object.Instantiate(_unitPrefab, _parentTransform);
            unit.Init(_unitTier, _pool, _canBeCombined);
            Debug.Log($"Unit {unit.name} ¹{_counter} created.");
            _counter++;
            return unit;
        }
    }
}