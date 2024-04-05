using System;
using System.Runtime.InteropServices;
using UnityEngine;

[Serializable]
public class UnitData
{
    [field: SerializeField] public Unit Unit { get; private set; }
    [field: SerializeField] public bool CanBeDropped { get; private set; }
    [field: SerializeField] public int BoundSpawnScore { get; private set; }
}
