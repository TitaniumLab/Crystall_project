using UnityEngine;
using UnityEngine.UI;

namespace CrystalProject.UI
{
    public class UIActions : MonoBehaviour
    {
        [SerializeField] private Button _menuButton;
        [SerializeField] private RectTransform _menuTransform;
        [SerializeField] private bool _activeMenuOnStart = false;
        [SerializeField] private float _timeScaleInMenu = 0;
        [SerializeField] private float _defalultTimeScale = 1;

        private void Awake()
        {
            _menuButton.onClick.AddListener(OnMenuOpenClose);
            _menuTransform.gameObject.SetActive(_activeMenuOnStart);
        }
        private void OnMenuOpenClose()
        {
            if (!_menuTransform.gameObject.activeInHierarchy)
            {
                Time.timeScale = _timeScaleInMenu;
                _menuTransform.gameObject.SetActive(true);
            }
            else
            {
                Time.timeScale = _defalultTimeScale;
                _menuTransform.gameObject.SetActive(false);
            }
        }
    }
}

