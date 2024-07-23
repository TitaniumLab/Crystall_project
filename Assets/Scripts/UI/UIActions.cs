using CrystalProject.EventBus;
using CrystalProject.EventBus.Signals;
using CrystalProject.Game;
using CrystalProject.Internal;
using CrystalProject.Score;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace CrystalProject.UI
{
    public class UIActions : MonoBehaviour
    {
        [Header("Rects")]
        [SerializeField] private RectTransform _gameOverTransform;
        [SerializeField] private RectTransform _menuTransform;
        [SerializeField] private RectTransform _promtTransform;
        [SerializeField] private RectTransform _optionsTransform;
        [SerializeField] private RectTransform _aboutTransform;
        [SerializeField] private RectTransform _loadingScreen;
        [Header("Other")]
        [SerializeField] private int _mainMenuSceneInd = 0;
        [SerializeField] private int _gameSceneIndex = 1;
        [SerializeField] private TextMeshProUGUI _scoreValueText;
        [SerializeField] private float _timeScaleInMenu = 0;
        [SerializeField] private float _defalultTimeScale = 1;
        [SerializeField] private bool _enableAd = true;
        [SerializeField, ConditionalHide(nameof(_enableAd)), Range(0, 1)] private float _restartAdChanse = 1;
        [SerializeField, ConditionalHide(nameof(_enableAd)), Range(0, 1)] private float _menuAdChanse = 0.5f;
        [SerializeField] private float _uiSecDel = 0.1f; // for some reason waiting for end of frame doesn't prevent crystal drop when UI is closed on smartphones
        private GameController _gameController;
        private CustomEventBus _eventBus;
        private IScore _score;

        [Inject]
        private void Construct(GameController gameController, CustomEventBus eventBus, IScore score)
        {
            _gameController = gameController;
            _eventBus = eventBus;
            _score = score;
        }

        #region MonoBeh //////////////////////////////////////////
        private void Awake()
        {
            // Disable/Active UI elements
            if (_gameOverTransform)
                _gameOverTransform.gameObject.SetActive(false);
            if (_menuTransform)
                _menuTransform.gameObject.SetActive(false);
            if (_optionsTransform)
                _optionsTransform.gameObject.SetActive(false);
            if (_aboutTransform)
                _aboutTransform.gameObject.SetActive(false);
            if (_loadingScreen)
                _loadingScreen.gameObject.SetActive(false);

            // Other events
            _eventBus?.Subscribe<GameOverSignal>(OnGameOver);
            _eventBus?.Subscribe<FirstGameStartSignal>(OnFirstGameStart);
        }

        private void OnDestroy()
        {
            _eventBus?.Unsubscribe<GameOverSignal>(OnGameOver);
            _eventBus?.Unsubscribe<FirstGameStartSignal>(OnFirstGameStart);
        }
        #endregion


        #region Methods //////////////////////////////////////////
        private void OnFirstGameStart(FirstGameStartSignal signal)
        {
            _promtTransform.gameObject.SetActive(true);
        }

        private void OnGameOver(GameOverSignal signal)
        {
            _scoreValueText.text = _score.Score.ToString();
            _gameOverTransform.gameObject.SetActive(true);
        }

        public void OnPlay()
        {
            if (_loadingScreen)
            {
                _loadingScreen.gameObject.SetActive(true);
            }
            SceneManager.LoadSceneAsync(_gameSceneIndex);
        }


        public void OnMenuOpenClose()
        {
            StartCoroutine(IEnumOnMenuOpenClose());
        }

        private IEnumerator IEnumOnMenuOpenClose()
        {
            if (!_menuTransform.gameObject.activeInHierarchy)
            {
                Time.timeScale = _timeScaleInMenu;
                _menuTransform.gameObject.SetActive(true);
            }
            else
            {
                Time.timeScale = _defalultTimeScale;
                yield return new WaitForSeconds(_uiSecDel);
                _menuTransform.gameObject.SetActive(false);
            }
        }


        public void OnAboutOpenClose()
        {
            if (!_aboutTransform.gameObject.activeInHierarchy)
            {
                _aboutTransform.gameObject.SetActive(true);
            }
            else
            {
                _aboutTransform.gameObject.SetActive(false);
            }
        }


        public void OpenURL(string url)
        {
            Application.OpenURL(url);
        }


        public void OnOpenClosePromt()
        {
            StartCoroutine(IEnumOnOpenClosePromt());
        }

        private IEnumerator IEnumOnOpenClosePromt()
        {
            if (!_promtTransform.gameObject.activeInHierarchy)
            {
                _promtTransform.gameObject.SetActive(true);
            }
            else
            {
                if (Time.timeScale != 0)
                {
                    yield return new WaitForSeconds(_uiSecDel);
                }
                _promtTransform.gameObject.SetActive(false);
            }
        }

        public void OnOpenCloseOptions()
        {
            if (!_optionsTransform.gameObject.activeInHierarchy)
            {
                _optionsTransform.gameObject.SetActive(true);
            }
            else
            {
                _optionsTransform.gameObject.SetActive(false);
            }
        }


        public void OnRestart()
        {
            if (_enableAd)
            {
                ShowAd.ShowAdWithChance(_restartAdChanse);
            }

            if (_loadingScreen)
            {
                _loadingScreen.gameObject.SetActive(true);
            }
            _gameController.RestartGame();
            Time.timeScale = _defalultTimeScale;
        }

        public void ToMainMenu()
        {
            if (_enableAd)
            {
                ShowAd.ShowAdWithChance(_menuAdChanse);
            }

            Time.timeScale = _defalultTimeScale;

            if (_loadingScreen)
            {
                _loadingScreen.gameObject.SetActive(true);
            }
            SceneManager.LoadSceneAsync(_mainMenuSceneInd);
        }
        #endregion
    }
}