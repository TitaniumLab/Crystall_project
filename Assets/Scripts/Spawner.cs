using Dropper.Animator;
using Dropper.Model;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Zenject;

public class Spawner : MonoBehaviour
{
    [SerializeField] private UnitBundleData _unitBundleData;
    private List<CustomUnityPool> _pools = new List<CustomUnityPool>();
    [SerializeField] private int _spawnCounter = 0;

    private DropModel _dropModel;
    private DropAnimator _dropAnimator;

    [Inject]
    private void Construct(DropModel dropModel, DropAnimator dropAnimator)
    {
        _dropModel = dropModel;
        _dropAnimator = dropAnimator;
    }

    private void Start()
    {
        foreach (var item in _unitBundleData.UnitData)
        {
            CustomUnityPool pool = new CustomUnityPool(item.Unit, transform);
            _pools.Add(pool);
        }

        //get first unit
        Unit unit = _pools[1].Get();

        _dropModel.GetUnit(unit);

        _dropAnimator.OnDropEnd += NextDroppedUnit;
    }

    private void NextDroppedUnit()
    {
        Unit unit = _pools[1].Get();
        unit.SetIndexNum(_spawnCounter);
        _dropModel.GetUnit(unit);
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
