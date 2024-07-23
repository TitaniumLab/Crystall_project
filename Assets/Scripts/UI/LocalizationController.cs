using CrystalProject.EventBus;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;
using YG;
using Zenject;

namespace CrystalProject.UI
{
    public class LocalizationController : MonoBehaviour
    {
        private static LocalizationController s_instance;
        private CustomEventBus _eventBus;
        [SerializeField] private int _currentLocIndex = 0;
        [SerializeField] private Sprite[] _languageSprite;
        [SerializeField] private Image _languageImage;
        [SerializeField] private TMP_Dropdown _localizationDropDown;
        [SerializeField] private Button _optionsOk;

        public event Action OnLanguageSet;


        [Inject]
        private void Constuct(CustomEventBus customEventBus)
        {
            _eventBus = customEventBus;
        }

        private void Awake()
        {
            if (s_instance is not null)
            {
                s_instance.SetInstance(_localizationDropDown, _languageImage, _languageSprite, _optionsOk);
                Destroy(this);
                return;
            }

            s_instance = this;

            _currentLocIndex = YandexGame.savesData.languageIndex;
        }

        private void Start()
        {
            _optionsOk.onClick.AddListener(SaveLanguage);
            _localizationDropDown.value = _currentLocIndex;
            _localizationDropDown.onValueChanged.AddListener(ChangeLanguage);
            ChangeLanguage(_currentLocIndex);
        }


        private void OnDestroy()
        {
            _localizationDropDown.onValueChanged.RemoveListener(ChangeLanguage);
        }


        private void SetInstance(TMP_Dropdown dropdown, Image image, Sprite[] flagSprites, Button okButton)
        {
            _localizationDropDown.onValueChanged.RemoveListener(ChangeLanguage);
            _optionsOk.onClick.RemoveListener(SaveLanguage);
            _optionsOk = okButton;
            _localizationDropDown = dropdown;
            _languageSprite = flagSprites;
            _languageImage = image;
            _optionsOk.onClick.AddListener(SaveLanguage);
            _localizationDropDown.onValueChanged.AddListener(ChangeLanguage);
            _localizationDropDown.value = _currentLocIndex;
        }

        private async void ChangeLanguage(int languageIndex)
        {
            _currentLocIndex = languageIndex;

            await LocalizationSettings.InitializationOperation.Task;
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[languageIndex];
            await LocalizationSettings.InitializationOperation.Task;
            _languageImage.sprite = _languageSprite[languageIndex];
            Debug.Log("Language changed.");
            OnLanguageSet?.Invoke();
        }


        public void SaveLanguage()
        {
            YandexGame.savesData.languageIndex = _currentLocIndex;
            YandexGame.SaveProgress();
        }
    }
}