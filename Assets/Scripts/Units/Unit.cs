using CrystalProject.Units.Create;
using System;
using UnityEngine;

namespace CrystalProject.Units
{
    /// <summary>
    /// Main game unit (crystal)
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public class Unit : MonoBehaviour, ICombinable
    {
        private int _unitTier;
        public int UnitTier { set { _unitTier = value; } }
        private CustomUnityPool _pool;
        public CustomUnityPool Pool { set { if (_pool is null) _pool = value; } }
        private bool _canBeCombined;
        public bool CanBeCombined { set { _canBeCombined = value; } }
        private Rigidbody _rb;
        private Quaternion _defaultRotation;
        public static event Action<Vector3, int> On—ombine;

        #region MonoBeh
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.TryGetComponent(out ICombinable combinable) && _canBeCombined && gameObject.activeSelf)
            {
                var otherUnitT = combinable.TryToCombine(_unitTier);
                if (otherUnitT is not null)
                {
                    var midPos = (otherUnitT.transform.position + transform.position) / 2;
                    On—ombine(midPos, _unitTier + 1);
                    _pool.Release(this);
                }
            }
        }

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _defaultRotation = transform.rotation;
        }
        private void OnEnable()
        {
            _rb.velocity = Vector3.zero;
            _rb.angularVelocity = Vector3.zero;
            transform.rotation = _defaultRotation;
        }
        #endregion

        #region Methods
        public Transform TryToCombine(int tier)
        {
            if (_unitTier == tier && gameObject.activeSelf)
            {
                _pool.Release(this);
                return transform;
            }
            return null;
        }
        #endregion
    }
}

