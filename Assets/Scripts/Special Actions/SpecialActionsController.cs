using CrystalProject.UI;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CrystalProject.Game
{
    public class SpecialActionsController : MonoBehaviour
    {
        [SerializeField] private GameObject _crystalsParent;

        [Header("Shake actions")]
        [SerializeField] private Button _shakeCrystallsButton;
        [SerializeField] private DragToShake _shakeBlockScreen;
        [SerializeField] private RectTransform _shakeDirectionImg;
        [SerializeField] private Image _shakePromtImg;
        [SerializeField] private Slider _shakePowerSlider;
        [SerializeField] private float _maxShakePower = 1000;
        [SerializeField] private float _shakePowerMultiplier = 1;
        [SerializeField] private Transform _gameUnitsParent;
        [SerializeField] private int _currentAttempt = 0;
        [SerializeField] private int _maxAttempt = 1;
        [SerializeField] private TextMeshProUGUI _attemptText;
        [SerializeField] private float _closeDelay = 0.25f;

        [Header("Other actions")]
        [SerializeField] private Button _okButton;
        [SerializeField] private Button _cancelButton;

        private float _sizeScale;


        #region Internal
        private void Awake()
        {
            _shakeBlockScreen.OnMoveStart += DragStart;
            _shakeBlockScreen.OnDirectionChanged += DragDisplay;
            _shakeBlockScreen.OnMoveEnd += DragStop;
            _shakeBlockScreen.OnCencel += DragCencel;

            SetShakeAttemptsText();
        }

        private void OnDestroy()
        {
            _shakeBlockScreen.OnMoveStart -= DragStart;
            _shakeBlockScreen.OnDirectionChanged -= DragDisplay;
            _shakeBlockScreen.OnMoveEnd -= DragStop;
            _shakeBlockScreen.OnCencel -= DragCencel;
        }
        #endregion


        #region Shake
        /// <summary>
        /// Enables all UI elemets for action
        /// </summary>
        /// <param name="isEnabled"></param>
        public void EnableShake(bool isEnabled)
        {
            if (_gameUnitsParent.childCount > 1)
            {
                ShowAd.ShowAdWithChance();
                _shakeCrystallsButton.gameObject.SetActive(!isEnabled);
                _cancelButton.gameObject.SetActive(isEnabled);
                _shakeBlockScreen.gameObject.SetActive(isEnabled);
                _shakePromtImg.gameObject.SetActive(isEnabled);
            }
        }

        /// <summary>
        /// Enables UI elements when dragging starts
        /// </summary>
        /// <param name="isEnabled"></param>
        private void EnableShakeElements(bool isEnabled)
        {
            _shakePromtImg.gameObject.SetActive(!isEnabled);
            _shakeDirectionImg.gameObject.SetActive(isEnabled);
            _shakePowerSlider.gameObject.SetActive(isEnabled);
        }

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
        private void DragDisplay(Vector2 direction)
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
            if (_currentAttempt >= _maxAttempt)
            {
                _shakeCrystallsButton.interactable = false;
            }
            SetShakeAttemptsText();
            StartCoroutine(CloseDelay());
        }

        /// <summary>
        /// On cenceling shake action
        /// </summary>
        private void DragCencel()
        {
            EnableShakeElements(false);
        }

        /// <summary>
        /// Set attempts text area
        /// </summary>
        private void SetShakeAttemptsText()
        {
            _attemptText.text = $"{_currentAttempt}/{_maxAttempt}";
        }

        /// <summary>
        /// Waiting "Awaitables" in next Unity version
        /// </summary>
        /// <returns></returns>
        private IEnumerator CloseDelay()
        {
            yield return new WaitForSeconds(_closeDelay);
            EnableShake(false);
            _shakePromtImg.gameObject.SetActive(false);
        }
        #endregion
    }
}

