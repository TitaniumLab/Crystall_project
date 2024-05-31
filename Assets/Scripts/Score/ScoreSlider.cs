using UnityEngine;
using UnityEngine.UI;

namespace CrystalProject.Score
{
    public class ScoreSlider : MonoBehaviour
    {
        [SerializeField] private Slider _scoreSlider;
        [SerializeField] private Image _thresholdImage;

        private void Start()
        {
            Instantiate(_thresholdImage, _scoreSlider.transform);
        }
    }
}

