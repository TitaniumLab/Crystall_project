using System;
using UnityEngine;
using CrystalProject.Internal;
using CrystalProject.Dropper;
using CrystalProject.Combine;
using CrystalProject.Units.Data;
using CrystalProject.Score;

namespace CrystalProject.Units
{
    [Serializable]
    public class UnitData : IDropData, ICombineData, IUnitData, IScoreData
    {
        // Data for unit creation
        [field: SerializeField] public Unit Unit { get; private set; }

        [SerializeField] private bool _canBeCombined = true;
        public bool CanBeCombined { get { return _canBeCombined; } }
        // Combine data
        [field: SerializeField]
        [field: ConditionalHide(nameof(_canBeCombined))] public int TierIncriminator { get; private set; } = 1;
        // Score data
        [field: SerializeField]
        [field: ConditionalHide(nameof(_canBeCombined))] public int ScoreOnCombine { get; private set; }
        // Dropper data
        [SerializeField] private bool _canBeDropped;
        public bool CanBeDropped { get { return _canBeDropped; } }
        [field: SerializeField]
        [field: ConditionalHide(nameof(_canBeDropped))] public int ScoreToDrop { get; private set; }


    }
}