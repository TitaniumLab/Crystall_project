using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CrystalProject.Score
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreTextMeshPro;
        [SerializeField] private string _scoreText = "Score: ";
        [SerializeField] private ScoreThreshold _scoreThreshold;

        public void ShowScore(int score)
        {
            _scoreTextMeshPro.text = _scoreText + score;
            _scoreThreshold.SetSliderValue(score);
        }


    }
}