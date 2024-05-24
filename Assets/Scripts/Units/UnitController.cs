using CrystalProject.Dropper;
using CrystalProject.Score;
using CrystalProject.Units.Create;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace CrystalProject.Units
{
    public class UnitController : MonoBehaviour, IUnitDispenser
    {
        [SerializeField] private UnitBundleData _unitBundleData;
        private List<CustomUnityPool> _pools = new List<CustomUnityPool>();
        private DropModel _dropModel;
        private DropAnimator _dropAnimator;
        private ScoreModel _scoreModel;
        public static event Action<int> ScoreOnCombine;

        [Inject]
        private void Construct(DropModel dropModel, DropAnimator dropAnimator, ScoreModel scoreModel)
        {
            _dropModel = dropModel;
            _dropAnimator = dropAnimator;
            _scoreModel = scoreModel;

            for (int i = 0; i < _unitBundleData.UnitData.Length; i++)
            {
                UnitData data = _unitBundleData.UnitData[i];
                CustomUnityPool pool = new CustomUnityPool(data.Unit, transform, i, data.CanBeCombined);
                _pools.Add(pool);
            }
        }

        private void Start()
        {


            //Unit.OnÑombine += NextCombinedUnit;
            //_dropAnimator.OnDropEnd += NextDroppedUnit;

            //NextDroppedUnit();
        }

        private void OnDestroy()
        {
            //Unit.OnÑombine -= NextCombinedUnit;
            //_dropAnimator.OnDropEnd -= NextDroppedUnit;
        }

        //private void NextDroppedUnit()
        //{
        //    int randomTier = GetUnitTier();
        //    Unit unit = _pools[randomTier].Get();
        //    _dropModel.GetUnit(unit.transform);
        //}

        //private void NextCombinedUnit(Vector3 pos, int tier)
        //{
        //    Unit unit = _pools[tier].Get();
        //    unit.transform.position = pos;
        //    ScoreOnCombine(_unitBundleData.UnitData[tier].ScoreOnCombine);
        //}

        //private int GetUnitTier()
        //{
        //    List<int> tiers = new List<int>();
        //    for (int i = 0; i < _unitBundleData.UnitData.Length; i++)
        //        if (_unitBundleData.UnitData[i].CanBeDropped && _scoreModel.Score >= _unitBundleData.UnitData[i].ScoreToDrop)
        //            tiers.Add(i);
        //    int index = Random.Range(0, tiers.Count);
        //    return tiers[index];
        //}

        public Unit GetUnit(int unitTier)
        {
            Unit unit = _pools[unitTier].Get();
            //unit.transform.position = pos;
            //ScoreOnCombine(_unitBundleData.UnitData[tier].ScoreOnCombine);
            return unit;
        }
    }
}

