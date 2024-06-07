using CrystalProject.EventBus;
using CrystalProject.EventBus.Signals;
using CrystalProject.Game;
using CrystalProject.Score;
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
        [Header("Rects")]
        [SerializeField] private RectTransform _gameOverTransform;
        [SerializeField] private RectTransform _menuTransform;
        [Header("Other")]
        [SerializeField] private TextMeshProUGUI _scoreValueText;
        [SerializeField] private float _timeScaleInMenu = 0;
        [SerializeField] private float _defalultTimeScale = 1;

        private GameController _gameController;
        private CustomEventBus _eventBus;
        private IScore _score;

        private void Awake()
        {
            // Bind buttons
            _menuButton.onClick.AddListener(OnMenuOpenClose);
            _returnButton.onClick.AddListener(OnMenuOpenClose);
            foreach (var button in _restartButtons)
            {
                button.onClick.AddListener(OnRestart);
            }

            // DisActive UI elements
            _gameOverTransform.gameObject.SetActive(false);
            _menuTransform.gameObject.SetActive(false);

            _eventBus.Subscribe<GameOverSignal>(OnGameOver);
        }

        private void OnDestroy()
        {
            // UnBind buttons
            _menuButton.onClick.RemoveListener(OnMenuOpenClose);
            _returnButton.onClick.RemoveListener(OnMenuOpenClose);
            foreach (var button in _restartButtons)
            {
                button.onClick.RemoveListener(OnRestart);
            }
        }

        [Inject]
        private void Construct(GameController gameController, CustomEventBus eventBus, IScore score)
        {
            _gameController = gameController;
            _eventBus = eventBus;
            _score = score;
        }

        private void OnGameOver(GameOverSignal signal)
        {
            _scoreValueText.text = _score.Score.ToString();
            _gameOverTransform.gameObject.SetActive(true);
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

        private void OnRestart()
        {
            _gameController.RestartGame();
            Time.timeScale = _defalultTimeScale;
        }
    }
}

