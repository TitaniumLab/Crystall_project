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
        public List<Unit> ContactUnit { get; private set; } = new List<Unit>();
        private CustomUnityPool _pool;
        private Collider _collider;
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
            if (ContactUnit.Count > 0)
            {
                if (ContactUnit.Count == 1 && ContactUnit[0].ContactUnit.Count < 2) //dont combine if contacted unit have more then 1 cotacts
                {
                    if (IndexNum > ContactUnit[0].IndexNum)
                    {
                        Vector3 pos = (ContactUnit[0].transform.position + transform.position) / 2;
                        On—ombine(pos, _pool.Tier + 1);
                    }
                    _pool.Release(this);
                }
                else if (ContactUnit.Count > 1)
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
                    On—ombine(pos, _pool.Tier + 1);
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
            _collider = GetComponent<Collider>();
            _defaultRotation = transform.rotation;
        }
        #endregion

        #region Methods
        public void SetPool(CustomUnityPool pool)
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
        #endregion
    }
}

