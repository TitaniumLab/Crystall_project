using TMPro;
using UnityEngine;

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
            _scoreModel = GetComponent<ScoreModel>();
            _scoreModel.OnScoreChange += ChangetScoreText;
            _scoreTextMeshPro.text = _scoreText;
        }

        private void ChangetScoreText(int score)
        {
            _scoreTextMeshPro.text = _scoreText + score;
        }
    }
}