using CrystalProject.EventBus;
using CrystalProject.EventBus.Signals;
using CrystalProject.Units.Create;
using UnityEngine;
using Zenject;

namespace CrystalProject.Combine
{
    [RequireComponent(typeof(CombineView))]
    public class CombineController : MonoBehaviour
    {
        private CustomEventBus _eventBus;
        private IUnitDispenser _dispenser;
        private ICombineData[] _data;
        private CombineView _view;
        [SerializeField] private float _particleSizeMulty = 2;

        [Inject]
        private void Construct(CustomEventBus eventBus, ICombineData[] combineDatas, IUnitDispenser unitDispenser)
        {
            _eventBus = eventBus;
            _data = combineDatas;
            _dispenser = unitDispenser;
        }

        private void Awake()
        {
            if (!TryGetComponent(out _view))
                Debug.LogError($"Missing {typeof(CombineView)} component.");
            _eventBus.Subscribe<CombineSignal>(OnCombineSignal);
        }

        private void OnDestroy()
        {
            _eventBus.Unsubscribe<CombineSignal>(OnCombineSignal);
        }

        private void OnCombineSignal(CombineSignal signal)
        {
            // Get and set new unit
            int curTier = signal.CombinedUnitTier;
            int nextTier = curTier + _data[curTier].TierIncriminator;
            var unit = _dispenser.GetUnit(nextTier).transform;
            Vector3 midPos = (signal.FirstPos + signal.SecondPos) / 2;
            unit.position = midPos;

            // Play particles
            Color color = Color.white;
            if (unit.TryGetComponent(out MeshRenderer mesh))
                color = mesh.material.color;
            _view.PlayParticles(midPos, unit.transform.localScale.y * _particleSizeMulty, color);
        }
    }
}
