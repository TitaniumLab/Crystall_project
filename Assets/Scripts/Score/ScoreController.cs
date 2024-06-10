using System;
using UnityEngine;
using Zenject;
using CrystalProject.EventBus;
using CrystalProject.EventBus.Signals;

namespace CrystalProject.Score
{
    [RequireComponent(typeof(ScoreView))]
    public class ScoreController : MonoBehaviour
    {
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
        }

        private void OnDestroy()
        {
            _eventBus.Unsubscribe<CombineSignal>(ScoreOnCombine);
            _eventBus.Unsubscribe<GameStartSignal>(OnGameStart);
        }

        private void OnGameStart(GameStartSignal gameStartSignal)
        {
            _scoreView.SetScoreText(_scoreModel.Score);
        }

        private void ScoreOnCombine(CombineSignal signal)
        {
            _scoreModel.AddScoreOnCombine(signal.CombinedUnitTier);
            _scoreView.SetScoreText(_scoreModel.Score);
        }
    }
}