using CrystalProject.EventBus;
using CrystalProject.EventBus.Signals;
using TMPro;
using UnityEngine;
using YG;
using YG.Utils.LB;
using Zenject;

namespace CrystalProject.Score
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreTextMeshPro;
        [SerializeField] private string _scoreText = "Score: ";
        [SerializeField] private ScoreThreshold _scoreThreshold;
        [SerializeField] private TextMeshProUGUI _scoreValueText;
        [SerializeField] private TextMeshProUGUI _oldScoreValueText;
        private IScore _score;
        private CustomEventBus _customEventBus;

        [Inject]
        private void Construct(CustomEventBus eventBus, IScore score)
        {
            _customEventBus = eventBus;
            _score = score;
        }

        private void Awake()
        {
            _customEventBus.Subscribe<GameOverSignal>(OnGameOver);
            YandexGame.onGetLeaderboard += OnUpdateLB;
        }


        private void Start()
        {
            YandexGame.GetLeaderboard("Score", 1, 1, 1, "nonePhoto");
        }


        private void OnDestroy()
        {
            YandexGame.onGetLeaderboard -= OnUpdateLB;
        }


        private void OnUpdateLB(LBData data)
        {
            _oldScoreValueText.text = data.thisPlayer.score.ToString();
        }


        private void OnGameOver(GameOverSignal signal)
        {
            _scoreValueText.text = _score.Score.ToString();
        }

        public void ShowScore(int score)
        {
            _scoreTextMeshPro.text = _scoreText + score;
            _scoreThreshold.SetSliderValue(score);
        }
    }
}