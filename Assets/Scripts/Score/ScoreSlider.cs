using UnityEngine;

namespace CrystalProject.Score
{
    [RequireComponent(typeof(ScoreModel))]
    public class ScoreSlider : MonoBehaviour
    {
        [SerializeField] private Sprite _thresholdValue;
        private ScoreModel _scoreModel;

        private void Awake()
        {
            if(TryGetComponent(out _scoreModel))
            {
               // _scoreModel.OnScoreChange
            }
        }
    }
}

