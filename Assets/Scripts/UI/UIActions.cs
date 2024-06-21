using CrystalProject.EventBus;
using CrystalProject.EventBus.Signals;
using CrystalProject.Game;
using CrystalProject.Score;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CrystalProject.UI
{
    public class UIActions : MonoBehaviour
    {
        [Header("Buttons")]
        [SerializeField] private Button _menuButton;
        [SerializeField] private Button _returnButton;
        [SerializeField] private Button[] _restartButtons;
        [SerializeField] private Button _callPromtButton;
        [SerializeField] private Button _okPromtButton;
        [Header("Rects")]
        [SerializeField] private RectTransform _gameOverTransform;
        [SerializeField] private RectTransform _menuTransform;
        [SerializeField] private RectTransform _promtTransform;
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
            // Bind buttons
            _menuButton.onClick.AddListener(OnMenuOpenClose);
            _returnButton.onClick.AddListener(OnMenuOpenClose);
            foreach (var button in _restartButtons)
            {
                button.onClick.AddListener(OnRestart);
            }
            _okPromtButton.onClick.AddListener(OnOpenClosePromt);
            _callPromtButton.onClick.AddListener(OnOpenClosePromt);

            // Disable/Active UI elements
            _gameOverTransform.gameObject.SetActive(false);
            _menuTransform.gameObject.SetActive(false);

            // Other events
            _eventBus.Subscribe<GameOverSignal>(OnGameOver);
            _eventBus.Subscribe<FirstGameStartSignal>(OnFirstGameStart);
        }

        private void OnDestroy()
        {
            // UnBind buttons
            _menuButton.onClick.RemoveListener(OnMenuOpenClose);
            _returnButton.onClick.RemoveListener(OnMenuOpenClose);
            _okPromtButton.onClick.RemoveListener(OnOpenClosePromt);
            _callPromtButton.onClick.RemoveListener(OnOpenClosePromt);
            foreach (var button in _restartButtons)
            {
                button.onClick.RemoveListener(OnRestart);
            }
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


        private void OnMenuOpenClose()
        {
            StartCoroutine(IEnumOnMenuOpenClose());
        }

        private void OnOpenClosePromt()
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

        private IEnumerator IEnumOnMenuOpenClose()
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

        private void OnRestart()
        {
            _gameController.RestartGame();
            Time.timeScale = _defalultTimeScale;
        }
        #endregion
    }
}

