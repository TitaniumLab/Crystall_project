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
        public List<Unit> ContactUnit { get; private set; } = new List<Unit>();
        private CustomUnityPool _pool;
        private Rigidbody _rb;
        private Quaternion _defaultRotation;
        public static event Action<Vector3, int> On—ombine;


        #region MonoBeh
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.TryGetComponent(out Unit unit) && gameObject.activeSelf && unit._pool.Tier == _pool.Tier && !ContactUnit.Contains(unit) && !_pool.LastTier)
                ContactUnit.Add(unit);
        }

        private void Update()
        {
            if (ContactUnit.Count >= _minUnitsToCombine)
            {
                if (ContactUnit.Count == _minUnitsToCombine && ContactUnit[0].ContactUnit.Count <= _minUnitsToCombine) //dont combine if contacted unit have more then _minUnitsToCombine cotacts
                {
                    if (IndexNum > ContactUnit[0].IndexNum)
                    {
                        Vector3 pos = (ContactUnit[0].transform.position + transform.position) / 2;
                        On—ombine(pos, _pool.Tier + _tierIncreminator);
                    }
                    _pool.Release(this);
                }
                else if (ContactUnit.Count > _minUnitsToCombine)
                {
                    int otherIndex = ContactUnit[0].IndexNum;
                    int count = 0;
                    for (int i = 1; i < ContactUnit.Count; i++)
                    {
                        if (otherIndex > ContactUnit[i].IndexNum)
                        {
                            otherIndex = ContactUnit[i].IndexNum;
                            count = i;
                        }
                    }
                    Vector3 pos = (ContactUnit[count].transform.position + transform.position) / 2;
                    On—ombine(pos, _pool.Tier + _tierIncreminator);
                    _pool.Release(this);
                    _pool.Release(ContactUnit[count]);
                }
                ContactUnit.Clear();
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

