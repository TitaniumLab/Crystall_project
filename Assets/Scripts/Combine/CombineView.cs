using UnityEngine;

namespace CrystalProject.Combine
{
    public class CombineView : MonoBehaviour
    {
        [SerializeField] private CombineParticle _combinePS;
        [SerializeField] private float _speedMulty;
        [SerializeField] private Color[] _colors;
        private CombineParticlePool _pool;

        private void Awake()
        {
            _pool = new CombineParticlePool(_combinePS, transform);
        }

        public void PlayParticles(Vector3 position, float size, Color color)
        {
            var ps = _pool.Get();
            ps.transform.position = position;
            ParticleSystem.MainModule main = ps.main;
            main.startColor = color;
            main.startSpeed = _speedMulty * size;
            ps.Play();

        }
    }
}

