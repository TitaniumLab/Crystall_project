using CrystalProject.Dropper;
using CrystalProject.UI;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace CrystalProject.SpecialActions
{
    public class ShakeAction : MonoBehaviour
    {
        [Header("Shake actions")]
        [SerializeField] private Transform _gameUnitsParent;
        [SerializeField] private UIActions _uiActions;
        [SerializeField] private DropController _dropController;
        [SerializeField] private DragToShake _shakeBlockScreen;
        [SerializeField] private RectTransform _shakeDirectionImg;
        [SerializeField] private Image _shakePromtImg;
        [SerializeField] private Slider _shakePowerSlider;
        [SerializeField] private float _maxShakePower = 1000;
        [SerializeField] private float _shakePowerMultiplier = 1;
        [SerializeField] private int _currentAttempt = 0;
        [SerializeField] private int _maxAttempts = 1;
        private float _sizeScale;


        #region Internal
        private void Awake()
        {
            _shakeBlockScreen.OnMoveStart += DragStart;
            _shakeBlockScreen.OnDirectionChanged += DragChange;
            _shakeBlockScreen.OnMoveEnd += DragStop;
            _shakeBlockScreen.OnCencel += DragCencel;


        }

        private void Start()
        {
            _uiActions.SetShakeAttemptsText(_currentAttempt, _maxAttempts);
        }

        private void OnDestroy()
        {
            _shakeBlockScreen.OnMoveStart -= DragStart;
            _shakeBlockScreen.OnDirectionChanged -= DragChange;
            _shakeBlockScreen.OnMoveEnd -= DragStop;
            _shakeBlockScreen.OnCencel -= DragCencel;
        }
        #endregion


        #region UI elements
        /// <summary>
        /// Preparing all UIe elements for action
        /// </summary>
        public void EnableShake()
        {
            if (_gameUnitsParent.childCount > 1)
            {
                ShowAd.ShowAdWithChance();
                _dropController.enabled = false;
                _uiActions.EnableCencelButton(true, OnShakeCencel);
                _uiActions.EnableMainUIButtons(false);

                _shakeBlockScreen.gameObject.SetActive(true);
                _shakePromtImg.gameObject.SetActive(true);
            }
        }

        /// <summary>
        /// Enable UI elements while dragging
        /// </summary>
        /// <param name="isEnabled"></param>
        public void EnableShakeElements(bool isEnabled)
        {
            _shakePromtImg.gameObject.SetActive(!isEnabled);
            _shakeDirectionImg.gameObject.SetActive(isEnabled);
            _shakePowerSlider.gameObject.SetActive(isEnabled);
        }


        public void OnShakeCencel()
        {
            EnableShakeElements(false);
            _shakeBlockScreen.gameObject.SetActive(false);
            _uiActions.EnableMainUIButtons(true);
            _uiActions.EnableCencelButton(false, OnShakeCencel);
        }
        #endregion

        #region Drag
        /// <summary>
        /// On dragging start
        /// </summary>
        /// <param name="pos"></param>
        /// <exception cref="Exception"></exception>
        private void DragStart(Vector2 pos)
        {
            if (_shakeDirectionImg.root.TryGetComponent(out CanvasScaler canvasScaler))
            {
                _sizeScale = canvasScaler.referenceResolution.y / Screen.height;
            }
            else
            {
                throw new Exception($"Missing {canvasScaler} component.");
            }
            EnableShakeElements(true);
            _shakeDirectionImg.anchoredPosition = pos;
        }

        /// <summary>
        /// Action on dragging
        /// </summary>
        /// <param name="direction"></param>
        private void DragChange(Vector2 direction)
        {
            //arrow
            var scale = (direction.magnitude * _sizeScale) / _shakeDirectionImg.sizeDelta.y;
            _shakeDirectionImg.localScale = Vector3.one * scale;
            _shakeDirectionImg.up = direction;

            //slider
            var power = direction.magnitude / _maxShakePower;
            _shakePowerSlider.value = power;
        }

        /// <summary>
        /// On dragging stop
        /// </summary>
        /// <param name="direction"></param>
        private void DragStop(Vector2 direction)
        {
            EnableShakeElements(false);
            Vector3 power = direction.magnitude <= _maxShakePower ? direction : direction.normalized * _maxShakePower;
            foreach (Transform gameUnit in _gameUnitsParent)
            {
                if (gameUnit.TryGetComponent(out Rigidbody rb) && rb.useGravity)
                {
                    rb.AddForce(power * _shakePowerMultiplier, ForceMode.Impulse);
                }
            }
            _currentAttempt++;
            if (_currentAttempt >= _maxAttempts)
            {
                _uiActions.SetShakeButtonInteractable(false);
            }
            _uiActions.SetShakeAttemptsText(_currentAttempt, _maxAttempts);

            EnableShakeElements(false);
            _shakePromtImg.gameObject.SetActive(false);
            _uiActions.EnableMainUIButtons(true);
            _uiActions.EnableCencelButton(false, OnShakeCencel);
            _shakeBlockScreen.gameObject.SetActive(false);
            _dropController.enabled = true;
        }

        /// <summary>
        /// On cenceling shake action
        /// </summary>
        private void DragCencel()
        {
            EnableShakeElements(false);
        }
        #endregion
    }
}
