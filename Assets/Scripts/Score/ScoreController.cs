using System;
using UnityEngine;
using Zenject;
using CrystalProject.EventBus;
using CrystalProject.EventBus.Signals;
using YG;
using YG.Utils.LB;

namespace CrystalProject.Score
{
    [RequireComponent(typeof(ScoreView))]
    public class ScoreController : MonoBehaviour
    {
        [SerializeField] private string _scoreBoardName = "Score";
        private int _oldScore;
        private ScoreView _scoreView;
        private CustomEventBus _eventBus;
        private ScoreModel _scoreModel;

        [Inject]
        private void Construct(CustomEventBus customEventBus, ScoreModel scoreModel)
        {
            _eventBus = customEventBus;
            _scoreModel = scoreModel;
        }

        private void Awake()
        {
            if (!TryGetComponent(out _scoreView))
                throw new Exception($"Missing {typeof(ScoreView)} component.");
            _eventBus.Subscribe<CombineSignal>(ScoreOnCombine);
            _eventBus.Subscribe<GameStartSignal>(OnGameStart);
            _eventBus.Subscribe<GameOverSignal>(OnGameOver);
            YandexGame.onGetLeaderboard += OnUpdateLB;
        }



        private void OnDestroy()
        {
            _eventBus.Unsubscribe<CombineSignal>(ScoreOnCombine);
            _eventBus.Unsubscribe<GameStartSignal>(OnGameStart);
            _eventBus.Unsubscribe<GameOverSignal>(OnGameOver);
            YandexGame.onGetLeaderboard -= OnUpdateLB;
        }

        private void OnUpdateLB(LBData data)
        {
            _oldScore = data.thisPlayer.score;
        }

        private void OnGameStart(GameStartSignal gameStartSignal)
        {
            _scoreView.ShowScore(_scoreModel.Score);
        }

        private void ScoreOnCombine(CombineSignal signal)
        {
            _scoreModel.AddScoreOnCombine(signal.CombinedUnitTier);
            _scoreView.ShowScore(_scoreModel.Score);
        }

        private void OnGameOver(GameOverSignal gameOverSignal)
        {
            if (_scoreModel.Score > _oldScore)
            {
                YandexGame.NewLeaderboardScores(_scoreBoardName, _scoreModel.Score);
                Debug.Log($"Score saved: {_scoreModel.Score}");
            }
        }
    }
}