using CrystalProject.Units;
using System.Collections.Generic;
using UnityEngine;

public class UnitFactory
{
    private List<CustomUnityPool> _pools = new List<CustomUnityPool>();
    private int _counter = 0;
    public UnitFactory(UnitData[] unitDatas, Transform parentTransform)
    {
        int length = unitDatas.Length;
        for (int i = 0; i < length; i++)
        {
            bool isLastPool;
            if (i < length - 1)
                isLastPool = false;
            else
                isLastPool = true;
            CustomUnityPool pool = new CustomUnityPool(unitDatas[i].Unit, parentTransform, i, isLastPool);
            _pools.Add(pool);
        }
    }

    public Unit GetUnit(int tier)
    {
        Unit unit = _pools[tier].Get();
        unit.SetIndexNum(_counter);
        Debug.Log($"Unit ¹{_counter} spawned.");
        _counter++;
        //OnSpawn(_unitBundleData.UnitData[tier].ScoreOnSpawn);
        return unit;
    }
}

