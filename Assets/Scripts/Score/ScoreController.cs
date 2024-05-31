using TMPro;
using UnityEngine;
using Zenject;
using UnityEngine.UI;
using CrystalProject.EventBus;
using CrystalProject.EventBus.Signals;

namespace CrystalProject.Score
{
    public class ScoreController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreTextMeshPro;
        [SerializeField] private string _scoreText = "Score: ";
        private CustomEventBus _eventBus;
        private ScoreModel _scoreModel;

        private void Awake()
        {
            _scoreTextMeshPro.text = _scoreText + _scoreModel.Score;
            _eventBus.Subscribe<CombineSignal>(ScoreOnCombine);
        }

        private void OnDestroy()
        {
            _eventBus.Unsubscribe<CombineSignal>(ScoreOnCombine);
        }

        [Inject]
        private void Construct(CustomEventBus customEventBus, ScoreModel scoreModel)
        {
            _eventBus = customEventBus;
            _scoreModel = scoreModel;
        }

        private void ScoreOnCombine(CombineSignal signal)
        {
            _scoreModel.AddScoreOnCombine(signal.CombinedUnitTier);
            _scoreTextMeshPro.text = _scoreText + _scoreModel.Score;
        }
    }
}