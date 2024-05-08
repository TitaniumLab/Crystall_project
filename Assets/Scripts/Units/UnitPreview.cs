using UnityEngine;

namespace CrystalProject.Units
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public class UnitPreview : MonoBehaviour, IPreview
    {
        private Collider _collider;
        private Rigidbody _rb;

        private void Awake()
        {
            if (TryGetComponent(out Collider collider)) _collider = collider;
            else throw new System.Exception($"Missing {typeof(Collider).Name} component.");

            if (TryGetComponent(out Rigidbody rigidbody)) _rb = rigidbody;
            else throw new System.Exception($"Missing {typeof(Rigidbody).Name} component.");
        }

        public void DisablePreviewState()
        {
            _collider.enabled = true;
            _rb.useGravity = true;
        }

        public void EnablePreviewState()
        {
            _collider.enabled = false;
            _rb.useGravity = false;
        }
    }
}

