using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;
using Zenject.SpaceFighter;

namespace CrystalProject.Units
{
    public class CustomUnityPool
    {
        private Unit _unitPrefab;
        private Transform _parentTransform;
        private ObjectPool<Unit> _pool;
        public int Tier { get; private set; }
        public bool LastTier { get; private set; }
        public CustomUnityPool(Unit unitPrefab, Transform parentTransform, int poolTier, bool lastTier)
        {
            _pool = new ObjectPool<Unit>(CreateUnit, GetUnit, ReleaseUnit);
            _unitPrefab = unitPrefab;
            _parentTransform = parentTransform;
            Tier = poolTier;
            LastTier = lastTier;
        }

        public Unit Get()
        {
            return _pool.Get();
        }

        public void Release(Unit unit)
        {
            _pool.Release(unit);
        }

        private Unit CreateUnit()
        {
            var unit = Object.Instantiate(_unitPrefab, _parentTransform);
            unit.GetComponent<Unit>().SetPool(this);
            return unit;
        }

        private void GetUnit(Unit unit) =>
            unit.gameObject.SetActive(true);

        private void ReleaseUnit(Unit unit) =>
            unit.gameObject.SetActive(false);
    }

}
