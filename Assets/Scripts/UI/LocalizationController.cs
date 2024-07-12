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
        [SerializeField] private Button _optionsOk;

        private void Awake()
        {
            if (s_instance is not null)
            {
                Debug.Log(s_instance);
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
            _localizationDropDown.onValueChanged.AddListener(ChangeLanguage);
            _localizationDropDown.value = _currentLocIndex;
        }


        private void OnDestroy()
        {
            _localizationDropDown.onValueChanged.RemoveListener(ChangeLanguage);
        }


        private void SetInstance(TMP_Dropdown dropdown, Image image, Sprite[] flagSprites,Button okButton)
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