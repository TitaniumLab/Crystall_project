using System;
using UnityEngine;

namespace CrystalProject.Score
{
    public class ScoreModel : MonoBehaviour
    {
        [field: SerializeField] public int Score { get; private set; }
        public event Action<int> OnScoreChange;

        private void Awake()
        {
            Spawner.OnSpawn += AddScore;
        }

        private void AddScore(int score)
        {
            Score += score;
            OnScoreChange(Score);
        }
    }
}
