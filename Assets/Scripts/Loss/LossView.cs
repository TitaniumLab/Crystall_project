using UnityEngine;

namespace CrystalProject.Loss
{
    [RequireComponent(typeof(LossController))]
    public class LossView : MonoBehaviour
    {
        [SerializeField] private Material material;

        private void Awake()
        {
            
        }
    }
}

