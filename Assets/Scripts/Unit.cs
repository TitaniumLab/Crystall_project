using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Unit : MonoBehaviour
{
    private ObjectPool<Unit> _pool;
    public int IndexNum { get; private set; }
    private Collider _collider;
    private Rigidbody _rb;
    private Quaternion _defaultRotation;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        _defaultRotation = transform.rotation;
    }

    public void SetPool(ObjectPool<Unit> pool)
    {
        _pool = pool;
    }

    public void SetIndexNum(int index)
    {
        IndexNum = index;
    }

    public void EnablePreviewState()
    {
        _collider.enabled = false;
        _rb.useGravity = false;
    }

    public void DisablePreviewState()
    {
        _collider.enabled = true;
        _rb.useGravity = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Unit unit))
        {
            _pool.Release(this);
        }
    }

    private void OnEnable()
    {
        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
        transform.rotation = _defaultRotation;
    }
}
