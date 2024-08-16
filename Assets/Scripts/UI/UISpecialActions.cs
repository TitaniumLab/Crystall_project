using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CrystalProject.UI
{
    public class UISpecialActions : MonoBehaviour
    {
        [SerializeField] private Button _okSpecialButton;
        [SerializeField] private Button _cencelSpecialButton;
        [Header("Main buttons")]
        [SerializeField] private GameObject _mainMenuButton;
        [SerializeField] private Button _shakeButton;
        [SerializeField] private Button _deleteUnitButton;
        [SerializeField] private Button _switchButton;

        [Header("Attempts Text Fields")]
        [SerializeField] private TextMeshProUGUI _shakeAttemptsText;
        [SerializeField] private TextMeshProUGUI _deleteAttemptsText;
        [SerializeField] private TextMeshProUGUI _switchAttemptsText;
        [Header("On action elements")]
        [SerializeField] private GameObject _shakePromtImg;
        [SerializeField] private GameObject _changeUnitButtons;

        #region SpecialActions
        #region Ok Button
        public void EnableOkButton(bool isEnabled, Action action)
        {
            if (isEnabled)
            {
                _okSpecialButton.onClick.AddListener(() => action());
            }
            else
            {
                _okSpecialButton.onClick.RemoveAllListeners();
            }
            _okSpecialButton.gameObject.SetActive(isEnabled);
        }

        public void InteractableOkButton(bool isInteractable)
        {
            _okSpecialButton.interactable = isInteractable;
        }
        #endregion

        #region Cencel Button
        public void EnableCencelButton(bool isEnabled, Action action)
        {
            if (isEnabled)
            {
                _cencelSpecialButton.onClick.AddListener(() => action());
            }
            else
            {
                _cencelSpecialButton.onClick.RemoveAllListeners();
            }
            _cencelSpecialButton.gameObject.SetActive(isEnabled);
        }

        public void InteractableCencelButton(bool isInteractable)
        {
            _cencelSpecialButton.interactable = isInteractable;
        }
        #endregion

        #region UI Main
        /// <summary>
        /// Enable all special action and menu buttons
        /// </summary>
        /// <param name="isEnabled"></param>
        public void EnableMainUIButtons(bool isEnabled)
        {
            _mainMenuButton.SetActive(isEnabled);
            _shakeButton.gameObject.SetActive(isEnabled);
            _deleteUnitButton.gameObject.SetActive(isEnabled);
            _switchButton.gameObject.SetActive(isEnabled);
        }
        #endregion

        #region Shake
        public void SetShakeButtonInteractable(bool isInteractable)
        {
            _shakeButton.interactable = isInteractable;
        }

        public void SetShakeAttemptsText(int currentAttempt, int maxAttempt)
        {
            _shakeAttemptsText.text = $"{currentAttempt}/{maxAttempt}";
        }
        #endregion

        #region Delete
        public void SetDeleteButtonInteractable(bool isInteractable)
        {
            _deleteUnitButton.interactable = isInteractable;
        }

        public void SetDeleteAttemptsText(int currentAttempt, int maxAttempt)
        {
            _deleteAttemptsText.text = $"{currentAttempt}/{maxAttempt}";
        }
        #endregion

        #region Switch
        public void InteractableSwitchButton(bool isInteractable)
        {
            _switchButton.interactable = isInteractable;
        }

        public void SetSwitchAttemptsText(int currentAttempt, int maxAttempt)
        {
            _switchAttemptsText.text = $"{currentAttempt}/{maxAttempt}";
        }

        public void EnableSwitchArrowButtons(bool isEnabled)
        {
            _changeUnitButtons.SetActive(isEnabled);
        }
        #endregion

        #endregion
    }
}