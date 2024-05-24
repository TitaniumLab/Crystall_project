using System;
using UnityEngine;
using CrystalProject.Internal;
using CrystalProject.Dropper;

namespace CrystalProject.Units
{
    [Serializable]
    public class UnitData: IDropData
    {
        [field: SerializeField] public Unit Unit { get; private set; }

        [SerializeField] private bool _canBeCombined = true;
        public bool CanBeCombined { get { return _canBeCombined; } }
        [field: SerializeField]
        [field: ConditionalHide(nameof(_canBeCombined))] public int UnitTier { get; private set; }
        [field: SerializeField]
        [field: ConditionalHide(nameof(_canBeCombined))] public int ScoreOnCombine { get; private set; }

        [SerializeField] private bool _canBeDropped;
        public bool CanBeDropped { get { return _canBeDropped; } }
        [field: SerializeField]
        [field: ConditionalHide(nameof(_canBeDropped))] public int ScoreToDrop { get; private set; }
    }
}