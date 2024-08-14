using CrystalProject.Dropper;
using CrystalProject.UI;
using CrystalProject.Units;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CrystalProject.SpecialActions
{
    public class DeleteUnitAction : MonoBehaviour
    {
        [SerializeField] private UIActions _uIActions;
        [SerializeField] private DropController _dropController;


        #region Internal
        private void Awake()
        {
            enabled = false;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Ray ray = new Ray(worldPos, Vector3.forward);
                if (Physics.Raycast(ray, out RaycastHit hitInfo) && hitInfo.collider.TryGetComponent(out Unit unit))
                {
                    Debug.Log("name");
                }
            }
        }
        #endregion
        public void EnableDeleteState(bool isEnabled)
        {
            enabled = isEnabled;
            _dropController.enabled = !isEnabled;
        }
    }
}