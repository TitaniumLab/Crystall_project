using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;

namespace CrystalProject.UI
{
    public class LocalizationController : MonoBehaviour
    {
        private static LocalizationController s_instance;
        [SerializeField] private int _currentLocIndex = 0;
        [SerializeField] private TMP_Dropdown _localizationDropDown;

        private void Awake()
        {
            if (s_instance is not null)
            {
                Debug.Log(s_instance);
                s_instance.SetInstance(_localizationDropDown);
                Destroy(this);
                return;
            }

            s_instance = this;
            _localizationDropDown.onValueChanged.AddListener(ChangeLanguage);
            _localizationDropDown.value = _currentLocIndex;
        }

        private void SetInstance(TMP_Dropdown dropdown)
        {
            _localizationDropDown.onValueChanged.RemoveListener(ChangeLanguage);
            _localizationDropDown = dropdown;
            _localizationDropDown.value = _currentLocIndex;
            _localizationDropDown.onValueChanged.AddListener(ChangeLanguage);
        }

        private void ChangeLanguage(int languageIndex)
        {
            _currentLocIndex = languageIndex;
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[languageIndex];
        }
    }
}