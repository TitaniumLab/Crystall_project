using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using YG;
using Zenject;

namespace CrystalProject.UI
{
    public class GameLoadingWaiter : MonoBehaviour
    {
        private static GameLoadingWaiter s_instance;
        private LocalizationController _localizationController;
        [SerializeField] private RectTransform _loadingRT;
        [SerializeField] private float _langWaitSec = 1f;

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
            _loadingRT.gameObject.SetActive(true);
        }

        private void OnDestroy()
        {
            if (_localizationController)
            {
                _localizationController.OnLanguageSet -= OnLanguageSet;
            }
        }

        private void OnLanguageSet()
        {
            StartCoroutine(StartDelay());
        }

        private IEnumerator StartDelay()
        {
            yield return new WaitForSeconds(_langWaitSec);
            _loadingRT.gameObject.SetActive(false);
            YandexGame.GameReadyAPI();
        }
    }
}