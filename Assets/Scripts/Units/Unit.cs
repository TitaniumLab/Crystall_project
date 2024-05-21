using System;
using System.Collections.Generic;
using UnityEngine;

namespace CrystalProject.Units
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public class Unit : MonoBehaviour
    {
        [field: SerializeField] public int IndexNum { get; private set; }
        [SerializeField] private int _minUnitsToCombine = 1;
        [SerializeField] private int _tierIncreminator = 1;
        public List<Unit> ContactUnits { get; private set; } = new List<Unit>();
        private CustomUnityPool _pool;
        private Rigidbody _rb;
        private Quaternion _defaultRotation;
        public static event Action<Vector3, int> On—ombine;


        #region MonoBeh
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.TryGetComponent(out Unit unit) && gameObject.activeSelf && unit._pool.Tier == _pool.Tier && !ContactUnits.Contains(unit) && !_pool.LastTier)
            {
                ContactUnits.Add(unit);
                
            }
            Debug.LogWarning(collision.conta);
        }

        private void Update()
        {
            if (ContactUnits.Count >= _minUnitsToCombine)
            {
                if (ContactUnits.Count == _minUnitsToCombine && ContactUnits[0].ContactUnits.Count <= _minUnitsToCombine) //dont combine if contacted unit have more then _minUnitsToCombine cotacts
                {
                    if (IndexNum > ContactUnits[0].IndexNum)
                    {
                        Vector3 pos = (ContactUnits[0].transform.position + transform.position) / 2;
                        On—ombine(pos, _pool.Tier + _tierIncreminator);
                    }
                    _pool.Release(this);
                }
                else if (ContactUnits.Count > _minUnitsToCombine)
                {
                    int otherIndex = ContactUnits[0].IndexNum;
                    int count = 0;
                    for (int i = 1; i < ContactUnits.Count; i++)
                    {
                        if (otherIndex > ContactUnits[i].IndexNum)
                        {
                            otherIndex = ContactUnits[i].IndexNum;
                            count = i;
                        }
                    }
                    Vector3 pos = (ContactUnits[count].transform.position + transform.position) / 2;
                    On—ombine(pos, _pool.Tier + _tierIncreminator);
                    _pool.Release(this);
                    _pool.Release(ContactUnits[count]);
                }
                ContactUnits.Clear();
            }
        }


        private void OnEnable()
        {
            _rb.velocity = Vector3.zero;
            _rb.angularVelocity = Vector3.zero;
            transform.rotation = _defaultRotation;
        }

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _defaultRotation = transform.rotation;
        }
        #endregion

        #region Methods
        public void SetPool(CustomUnityPool pool) =>
            _pool = pool;

        public void SetIndexNum(int index) =>
            IndexNum = index;
        #endregion
    }
}

