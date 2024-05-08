using CrystalProject.Dropper;
using CrystalProject.Score;
using CrystalProject.Units;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace CrystalProject
{
    public class UnitController : MonoBehaviour
    {
        [SerializeField] private UnitBundleData _unitBundleData;
        private UnitFactory _unitFactory;

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
        }

        private void Start()
        {
            _unitFactory = new UnitFactory(_unitBundleData.UnitData, transform);

            Unit.On—ombine += NextCombinedUnit;
            _dropAnimator.OnDropEnd += NextDroppedUnit;

            NextDroppedUnit();
        }

        private void OnDestroy()
        {
            Unit.On—ombine -= NextCombinedUnit;
            _dropAnimator.OnDropEnd -= NextDroppedUnit;
        }

        private void NextDroppedUnit()
        {
            int randomTier = GetUnitTier();
            Unit unit = _unitFactory.GetUnit(randomTier);
            _dropModel.GetUnit(unit.transform);
        }

        private void NextCombinedUnit(Vector3 pos, int tier)
        {
            Unit unit = _unitFactory.GetUnit(tier);
            unit.transform.position = pos;
            ScoreOnCombine(_unitBundleData.UnitData[tier].ScoreOnCombine);
        }

        private int GetUnitTier()
        {
            List<int> tiers = new List<int>();
            for (int i = 0; i < _unitBundleData.UnitData.Length; i++)
                if (_unitBundleData.UnitData[i].CanBeDropped && _scoreModel.Score >= _unitBundleData.UnitData[i].ScoreToDrop)
                    tiers.Add(i);
            int index = Random.Range(0, tiers.Count);
            return tiers[index];
        }
    }
}

