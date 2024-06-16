using UnityEngine;
using UnityEngine.Pool;

namespace CrystalProject.Combine
{
    public class CombineParticlePool
    {
        private ObjectPool<CombineParticle> _pool;
        private CombineParticle _combineParticle;
        private Transform _parentTransform;

        public CombineParticlePool(CombineParticle psPrefab, Transform parentTransform)
        {
            _pool = new ObjectPool<CombineParticle>(CreateUnit, GetUnit, ReleaseUnit);
            _combineParticle = psPrefab;
            _parentTransform = parentTransform;
        }

        public ParticleSystem Get()
        {
            _pool.Get().TryGetComponent(out ParticleSystem ps);
            return ps;
        }

        public void Release(CombineParticle particalS)
        {
            _pool.Release(particalS);
        }

        private CombineParticle CreateUnit()
        {
            var unit = Object.Instantiate(_combineParticle, _parentTransform);
            unit.SetPool(this);
            return unit;
        }

        private void GetUnit(CombineParticle particalS) =>
            particalS.gameObject.SetActive(true);

        private void ReleaseUnit(CombineParticle particalS) =>
            particalS.gameObject.SetActive(false);
    }
}