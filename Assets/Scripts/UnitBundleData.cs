using UnityEngine;

[CreateAssetMenu(fileName = "New UnitBundleData", menuName = "UnitBundleData", order = 51)]
public class UnitBundleData : ScriptableObject
{
    [field: SerializeField] public UnitData[] UnitData { get; private set; }
}
