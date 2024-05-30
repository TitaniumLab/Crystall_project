using CrystalProject.EventBus;
using CrystalProject.EventBus.Signals;
using CrystalProject.Units.Create;
using System;
using UnityEngine.UIElements;
using UnityEngine;
using Zenject;

namespace CrystalProject.Combine
{
    public class CombineController : IDisposable
    {
        private CustomEventBus _eventBus;
        private IUnitDispenser _dispenser;
        private ICombineData[] _data;

        [Inject]
        private void Construct(CustomEventBus eventBus, ICombineData[] combineDatas, IUnitDispenser unitDispenser)
        {

            _eventBus = eventBus;
            _eventBus.Subscribe<CombineSignal>(OnCombine);
            _data = combineDatas;
            _dispenser = unitDispenser;
        }

        private void OnCombine(CombineSignal signal)
        {
            int curTier = signal.CombinedUnitTier;
            int nextTier = curTier + _data[curTier].TierIncriminator;
            var unit = _dispenser.GetUnit(nextTier).transform;
            Vector3 midPos = (signal.FirstPos + signal.SecondPos) / 2;
            unit.position = midPos;
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe<CombineSignal>(OnCombine);
        }
    }
}
