using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Zenject;

public class Spawner : MonoBehaviour
{
    [SerializeField] private UnitBundleData _unitBundleData;
    private List<CustomUnityPool> _pools = new List<CustomUnityPool>();
    [SerializeField] private int _spawnCounter = 0;
    //[SerializeField] private Transform _previewPoint;
    private IDropper _dropController;
    private IAnim _anim;

    [Inject]
    private void Constructor(IDropper dropper, IAnim anim)
    {
        _dropController = dropper;
        _anim = anim;
        //_anim.OnDrop += SpawnDroppedUnit;
    }

    private void Awake()
    {
        foreach (var item in _unitBundleData.UnitData)
        {
            CustomUnityPool pool = new CustomUnityPool(item.Unit, transform);
            _pools.Add(pool);
        }

        Unit unit = _pools[1].Get();
        _dropController.GetUnit(unit);
    }

    //private void SpawnDroppedUnit()
    //{
    //    int unitID = 1;
    //    var unit = _pools[unitID].Get();
    //    unit.transform.position = _previewPoint.position;
    //    var previewUnit = unit.GetComponent<Previewable>();
    //    _dropController.GetUnit(previewUnit);
    //}

    //private Unit SpawnUnit(int unitID)
    //{

    //    var unitToSpawn = _unitBundleData.UnitData[unitID].Unit;
    //    var spawnedUnit = Instantiate(unitToSpawn, position, unitToSpawn.transform.rotation, this.transform);
    //    return spawnedUnit;
    //}


    private void GetRandomUnit()
    {

    }
}
