using UnityEngine;
using UnityEngine.Pool;

namespace CrystalProject.Units.Create
{
    public class CustomUnityPool
    {
        private ObjectPool<Unit> _pool;
        private UnitFactory _factory;

        public CustomUnityPool(Unit unitPrefab, Transform parentTransform, int unitTier, bool canBeCombined)
        {
            _pool = new ObjectPool<Unit>(CreateUnit, GetUnit, ReleaseUnit);
            _factory = new UnitFactory(unitPrefab, parentTransform, unitTier, canBeCombined, this);
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
            var unit = _factory.CreateUnit();
            return unit;
        }

        private void GetUnit(Unit unit) =>
            unit.gameObject.SetActive(true);

        private void ReleaseUnit(Unit unit) =>
            unit.gameObject.SetActive(false);
    }

}
