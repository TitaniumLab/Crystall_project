using TMPro;
using UnityEngine;
using System;

namespace CrystalProject.Score
{
    [RequireComponent(typeof(ScoreModel))]
    public class ScoreController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreTextMeshPro;
        [SerializeField] private string _scoreText = "Score: ";
        private ScoreModel _scoreModel;

        private void Awake()
        {
            if (TryGetComponent(out ScoreModel scoreModel)) _scoreModel = scoreModel;
            else throw new Exception($"Missing {typeof(ScoreModel).Name} component.");

            _scoreModel.OnScoreChange += ChangetScoreText;
            _scoreTextMeshPro.text = _scoreText;
        }

        private void OnDestroy()
        {
            _scoreModel.OnScoreChange -= ChangetScoreText;
        }

        private void ChangetScoreText(int score)
        {
            _scoreTextMeshPro.text = _scoreText + score;
        }
    }
}