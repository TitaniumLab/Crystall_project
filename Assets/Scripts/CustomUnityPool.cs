using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Zenject.SpaceFighter;

public class CustomUnityPool
{
    private Unit _unitPrefab;
    private Transform _parentTransform;
    private ObjectPool<Unit> _pool;
    public CustomUnityPool(Unit unitPrefab, Transform parentTransform)
    {
        _pool = new ObjectPool<Unit>(CreateUnit, GetUnit, ReleaseUnit);
        _unitPrefab = unitPrefab;
        _parentTransform = parentTransform;
    }

    public Unit Get()
    {
        return _pool.Get();
    }

    public void Release(Unit unit)
    {
        _pool.Release(unit);
    }

    private Unit CreateUnit()
    {
        var unit = Object.Instantiate(_unitPrefab, _parentTransform);
        unit.GetComponent<Unit>().SetPool(_pool);
        return unit;
    }

    private void GetUnit(Unit unit) =>
        unit.gameObject.SetActive(true);

    private void ReleaseUnit(Unit unit) =>
        unit.gameObject.SetActive(false);
}
