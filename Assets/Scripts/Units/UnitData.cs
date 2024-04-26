using System;
using UnityEngine;

namespace CrystalProject.Units
{
    [Serializable]
    public class UnitData
    {
        [field: SerializeField] public Unit Unit { get; private set; }
        [field: SerializeField] public int ScoreOnSpawn { get; private set; }
        [field: SerializeField] public bool CanBeDropped { get; private set; }
        [field: SerializeField] public int ScoreToDrop { get; private set; }
    }
}

