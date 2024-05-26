using CrystalProject.EventBus;
using CrystalProject.Units.Create;
using CrystalProject.Units.Data;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace CrystalProject.Units
{
    public class UnitController : IUnitDispenser
    {
        private Transform _parentTransform;
        private List<CustomUnityPool> _pools = new List<CustomUnityPool>();


        public UnitController(Transform gameUnitParent)
        {
            _parentTransform = gameUnitParent;
        }


        [Inject]
        private void Construct(IUnitData[] unitData,CustomEventBus customEventBus)
        {
            for (int i = 0; i < unitData.Length; i++)
            {
                CustomUnityPool pool = new CustomUnityPool(unitData[i].Unit, _parentTransform, i, unitData[i].CanBeCombined,customEventBus);
                _pools.Add(pool);
            }
        }

        public Unit GetUnit(int unitTier)
        {
            Unit unit = _pools[unitTier].Get();
            return unit;
        }
    }
}

