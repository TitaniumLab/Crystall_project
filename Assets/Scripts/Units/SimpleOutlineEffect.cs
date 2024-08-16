using UnityEngine;

namespace CrystalProject.Units.Outline
{
    [RequireComponent(typeof(MeshFilter))]
    public class SimpleOutlineEffect : MonoBehaviour
    {
        private MeshFilter _mFilter;

        private void Awake()
        {
            _mFilter = GetComponent<MeshFilter>();
        }
        public void SetToTransform(Transform parent, float scale)
        {
            transform.parent = parent;
            if (parent.TryGetComponent(out MeshFilter filter))
            {
                _mFilter.mesh = filter.mesh;
            }
            transform.SetPositionAndRotation(parent.position, parent.rotation);
            transform.localScale = Vector3.one * scale;
        }

        public void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }
    }
}