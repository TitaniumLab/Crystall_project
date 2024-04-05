using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Spawner : MonoBehaviour
{

    [SerializeField] private UnitBundleData _unitBundleData;
    [SerializeField] private int _spawnCounter = 0;
    [SerializeField] private Transform _previewPoint;
    private IDropper _dropController;

    [Inject]
    private void Constructor(IDropper dropper)
    {
        _dropController = dropper;
        _dropController.OnDrop += SpawnDroppedUnit;
    }

    private void Awake()
    {

    }

    private void SpawnDroppedUnit()
    {
        int unitID = 0;
        var previewUnit = SpawnNewUnit(unitID, _previewPoint.position).GetComponent<Previewable>();
        _dropController.GetUnit(previewUnit);
    }

    private Unit SpawnNewUnit(int unitID, Vector3 position)
    {
        var unitToSpawn = _unitBundleData.UnitData[unitID].Unit;
        var spawnedUnit = Instantiate(unitToSpawn, position, Quaternion.identity, this.transform);
        return spawnedUnit;
    }


    private void GetRandomUnit()
    {

    }
}
