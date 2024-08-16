using CrystalProject.Dropper;
using CrystalProject.UI;
using CrystalProject.Units;
using CrystalProject.Units.Outline;
using UnityEngine;

namespace CrystalProject.SpecialActions
{
    public class DeleteUnitAction : MonoBehaviour
    {
        [SerializeField] private UISpecialActions _uIActions;
        [SerializeField] private DropController _dropController;
        [SerializeField] private SimpleOutlineEffect _outline;
        [SerializeField] private int _currentAttempt = 0;
        [SerializeField] private int _maxAttempts = 2;
        [SerializeField] private float _outlineThickness = 0.1f;
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private Unit _unit;

        #region Internal
        private void Start()
        {
            SetAttempts();
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
                    _unit = unit;
                    float scale = 1 + _outlineThickness * 2;
                    _outline.SetActive(true);
                    _outline.SetToTransform(_unit.transform, scale);
                    _uIActions.InteractableOkButton(true);
                }
            }
        }
        #endregion

        #region Methods
        public void EnableDeleteState(bool isEnabled)
        {
            enabled = isEnabled;
            _dropController.enabled = !isEnabled;
            if (isEnabled)
            {
                ShowAd.ShowAdWithChance();
                _uIActions.InteractableOkButton(!isEnabled);
            }
            _uIActions.EnableOkButton(isEnabled, OnDeleteAccept);
            _uIActions.EnableCencelButton(isEnabled, OnDeleteCencel);
            _uIActions.EnableMainUIButtons(!isEnabled);
        }

        public void OnDeleteAccept()
        {
            _particleSystem.transform.position = _unit.transform.position;
            _particleSystem.Play();
            _unit.PoolIt();
            _unit = null;
            _currentAttempt++;
            SetAttempts();
            _outline.SetActive(false);
            EnableDeleteState(false);
        }

        public void OnDeleteCencel()
        {
            _outline.SetActive(false);
            _unit = null;
            EnableDeleteState(false);
        }

        private void SetAttempts()
        {
            _uIActions.SetDeleteAttemptsText(_currentAttempt, _maxAttempts);
            if (_currentAttempt >= _maxAttempts)
            {
                _uIActions.SetDeleteButtonInteractable(false);
            }
        }
        #endregion
    }
}