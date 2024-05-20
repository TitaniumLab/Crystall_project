using System;
using UnityEditor;
using UnityEngine;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

namespace CrystalProject.Units
{
    // SCRIPT WORK IN PROGRESS
    [Serializable]
    public class UnitData
    {
        [field: SerializeField] public Unit Unit { get; private set; }
        [field: SerializeField] public int ScoreOnCombine { get; private set; }
        [field: SerializeField] public bool CanBeDropped { get; private set; }
        public int ScoreToDrop/* { get; private set; }*/;
    }
}