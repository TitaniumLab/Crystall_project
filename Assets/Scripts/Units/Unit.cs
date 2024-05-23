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
        private CustomUnityPool _pool;
        private bool _canBeCombined;
        private Rigidbody _rb;
        private Quaternion _defaultRotation; // For setting rotation after pooling
        public static event Action<Vector3, int> On—ombine;

        #region MonoBeh
        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _defaultRotation = transform.rotation;
        }

        private void OnCollisionEnter(Collision collision)
        {
            // Combine with vombinable units
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

        // Set default state after pooling
        private void OnEnable()
        {
            _rb.velocity = Vector3.zero;
            _rb.angularVelocity = Vector3.zero;
            transform.rotation = _defaultRotation;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Set object fields
        /// </summary>
        /// <param name="unitTier"></param>
        /// <param name="pool"></param>
        /// <param name="canBeCombined"></param>
        public void Init(int unitTier, CustomUnityPool pool, bool canBeCombined)
        {
            _unitTier = unitTier;
            _pool = pool;
            _canBeCombined = canBeCombined;
        }


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

