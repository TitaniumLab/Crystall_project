using CrystalProject.EventBus;
using CrystalProject.EventBus.Signals;
using CrystalProject.Game;
using CrystalProject.Score;
using System.Collections;
using TMPro;
using UnityEngine;
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
        [Header("Other")]
        [SerializeField] private TextMeshProUGUI _scoreValueText;
        [SerializeField] private float _timeScaleInMenu = 0;
        [SerializeField] private float _defalultTimeScale = 1;

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
            _gameOverTransform.gameObject.SetActive(false);
            _menuTransform.gameObject.SetActive(false);
            _optionsTransform.gameObject.SetActive(false);
            _aboutTransform.gameObject.SetActive(false);

            // Other events
            _eventBus.Subscribe<GameOverSignal>(OnGameOver);
            _eventBus.Subscribe<FirstGameStartSignal>(OnFirstGameStart);
        }

        private void OnDestroy()
        {
            _eventBus.Unsubscribe<GameOverSignal>(OnGameOver);
            _eventBus.Unsubscribe<FirstGameStartSignal>(OnFirstGameStart);
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


        public void OnMenuOpenClose()
        {
            StartCoroutine(IEnumOnMenuOpenClose());
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

        public void OnOpenClosePromt()
        {
            if (!_promtTransform.gameObject.activeInHierarchy)
            {
                _promtTransform.gameObject.SetActive(true);
            }
            else
            {
                StartCoroutine(IEnumOnClosePromt());
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

        public IEnumerator IEnumOnMenuOpenClose()
        {
            if (!_menuTransform.gameObject.activeInHierarchy)
            {
                Time.timeScale = _timeScaleInMenu;
                _menuTransform.gameObject.SetActive(true);
            }
            else
            {
                yield return new WaitForEndOfFrame(); // Prevents actions when exiting the menu
                Time.timeScale = _defalultTimeScale;
                _menuTransform.gameObject.SetActive(false);
            }
        }

        private IEnumerator IEnumOnClosePromt()
        {
            yield return new WaitForEndOfFrame(); // Prevents actions when exiting the menu
            _promtTransform.gameObject.SetActive(false);
        }

        public void OnRestart()
        {
            _gameController.RestartGame();
            Time.timeScale = _defalultTimeScale;
        }
        #endregion
    }
}

