using CrystalProject.EventBus;
using CrystalProject.EventBus.Signals;
using CrystalProject.Units.Create;
using UnityEngine;
using Zenject;

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
        private CustomEventBus _eventBus;
        private Rigidbody _rb;
        private Quaternion _defaultRotation; // For setting rotation after pooling

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
                if (combinable.TryToCombine(_unitTier, transform.position))
                {
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
        //[Inject]
        //private void Construct(CustomEventBus customEventBus)
        //{
        //    _eventBus = customEventBus;

        //}

        /// <summary>
        /// Set object fields
        /// </summary>
        /// <param name="unitTier"></param>
        /// <param name="pool"></param>
        /// <param name="canBeCombined"></param>
        public void Init(int unitTier, CustomUnityPool pool, bool canBeCombined, CustomEventBus customEventBus)
        {
            _unitTier = unitTier;
            _pool = pool;
            _canBeCombined = canBeCombined;

            _eventBus = customEventBus;
        }


        public bool TryToCombine(int tier, Vector3 position)
        {
            if (_unitTier == tier && gameObject.activeSelf)
            {
                Vector3 midPos = (position + transform.position) / 2;
                _eventBus.Invoke(new CombineSignal(_unitTier, midPos));
                _pool.Release(this);
                return true;
            }
            return false;
        }
        #endregion
    }
}

