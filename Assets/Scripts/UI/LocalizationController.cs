using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;
using YG;

namespace CrystalProject.UI
{
    public class LocalizationController : MonoBehaviour
    {
        private static LocalizationController s_instance;
        [SerializeField] private int _currentLocIndex = 0;
        [SerializeField] private Sprite[] _languageSprite;
        [SerializeField] private Image _languageImage;
        [SerializeField] private TMP_Dropdown _localizationDropDown;

        private void Awake()
        {
            if (s_instance is not null)
            {
                Debug.Log(s_instance);
                s_instance.SetInstance(_localizationDropDown, _languageImage, _languageSprite);
                Destroy(this);
                return;
            }

            s_instance = this;

        }

        private void Start()
        {
            _currentLocIndex = YandexGame.savesData.languageIndex;
            _localizationDropDown.onValueChanged.AddListener(ChangeLanguage);

            if (_localizationDropDown.value != _currentLocIndex)
            {
                _localizationDropDown.value = _currentLocIndex;
            }
        }


        private void SetInstance(TMP_Dropdown dropdown, Image image, Sprite[] flagSprites)
        {
            _localizationDropDown.onValueChanged.RemoveListener(ChangeLanguage);
            _localizationDropDown = dropdown;
            _languageSprite = flagSprites;
            _languageImage = image;
            _localizationDropDown.onValueChanged.AddListener(ChangeLanguage);
            _localizationDropDown.value = _currentLocIndex;
        }

        private void ChangeLanguage(int languageIndex)
        {
            _currentLocIndex = languageIndex;
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[languageIndex];
            _languageImage.sprite = _languageSprite[languageIndex];
            Debug.Log("Language changed.");
        }

        public void SaveLanguage()
        {
            YandexGame.savesData.languageIndex = _currentLocIndex;
            YandexGame.SaveProgress();
        }
    }
}