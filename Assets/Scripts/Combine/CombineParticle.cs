using UnityEngine;

namespace CrystalProject.Combine
{
    [RequireComponent(typeof(ParticleSystem))]
    public class CombineParticle : MonoBehaviour
    {
        private CombineParticlePool _pool;

        private void Awake()
        {
            ParticleSystem.MainModule main = GetComponent<ParticleSystem>().main;
            main.stopAction = ParticleSystemStopAction.Callback;
        }
        private void OnParticleSystemStopped()
        {
            SelfPooling();
        }

        public void SetPool(CombineParticlePool pool)
        {
            _pool = pool;
        }

        private void SelfPooling()
        {
            if (isActiveAndEnabled)
                _pool.Release(this);
        }
    }
}