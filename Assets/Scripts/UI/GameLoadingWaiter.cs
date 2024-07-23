using System.Threading.Tasks;
using UnityEngine;

namespace CrystalProject.UI
{
    public class GameLoadingWaiter : MonoBehaviour
    {
        private static GameLoadingWaiter s_instance;
        private LocalizationController _localizationController;
        [SerializeField] private RectTransform _loadingRT;
        [SerializeField] private bool _languageSet = false;
        [SerializeField] private int _langWaitMSec = 1000;

        private void Awake()
        {
            if (s_instance is not null)
            {
                Destroy(this);
                return;
            }
            s_instance = this;

            _localizationController = transform.root.GetComponentInChildren<LocalizationController>();
            _localizationController.OnLanguageSet += OnLanguageSet;
        }


        private void Start()
        {
            WaitForLoading();
        }


        private void OnDestroy()
        {
            if (_localizationController)
            {
                _localizationController.OnLanguageSet -= OnLanguageSet;
            }
        }


        private async void WaitForLoading()
        {
            _loadingRT.gameObject.SetActive(true);
            Debug.Log("Start loading.");
            while (!_languageSet)
            {
                await Task.Yield();
            }
            Debug.Log("Language loaded.");
            _loadingRT.gameObject.SetActive(false);
        }

        private async void OnLanguageSet()
        {
            await Task.Delay(_langWaitMSec);
            _languageSet = true;
        }
    }
}