using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Unit : MonoBehaviour
{
    private ObjectPool<Unit> _pool;

    public void SetPool(ObjectPool<Unit> pool)
    {
        _pool = pool;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Unit unit))
        {
            _pool.Release(this);
        }
    }
}
