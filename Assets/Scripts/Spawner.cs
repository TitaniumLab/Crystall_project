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
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private UnitBundleData _unitBundleData;
        private List<CustomUnityPool> _pools = new List<CustomUnityPool>();
        [SerializeField] private int _spawnCounter = 0;

        private DropModel _dropModel;
        private DropAnimator _dropAnimator;
        private ScoreModel _scoreModel;
        public static event Action<int> OnSpawn;

        [Inject]
        private void Construct(DropModel dropModel, DropAnimator dropAnimator, ScoreModel scoreModel)
        {
            _dropModel = dropModel;
            _dropAnimator = dropAnimator;
            _scoreModel = scoreModel;
        }

        private void Start()
        {
            int length = _unitBundleData.UnitData.Length;
            for (int i = 0; i < length; i++)
            {
                bool isLastPool;
                if (i < length - 1)
                    isLastPool = false;
                else
                    isLastPool = true;
                CustomUnityPool pool = new CustomUnityPool(_unitBundleData.UnitData[i].Unit, transform, i, isLastPool);
                _pools.Add(pool);
            }

            Unit.On�ombine += NextCombinedUnit;
            _dropAnimator.OnDropEnd += NextDroppedUnit;

            NextDroppedUnit();
        }

        private void NextDroppedUnit()
        {
            int randomTier = GetUnitTier();
            Unit unit = GetUnit(randomTier);
            _dropModel.GetUnit(unit);
        }

        private void NextCombinedUnit(Vector3 pos, int tier)
        {
            Unit unit = GetUnit(tier);
            unit.transform.position = pos;
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

        private Unit GetUnit(int tier)
        {
            Unit unit = _pools[tier].Get();
            unit.SetIndexNum(_spawnCounter);
            Debug.Log($"Unit �{_spawnCounter} spawned.");
            _spawnCounter++;
            OnSpawn(_unitBundleData.UnitData[tier].ScoreOnSpawn);
            return unit;
        }
    }
}

