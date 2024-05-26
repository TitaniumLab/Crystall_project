using System;
using Zenject;

namespace CrystalProject.Score
{
    public class ScoreModel : IScore
    {
        private int _score = 0;
        public int Score { get { return _score; } }
        private IScoreData[] _data;

        [Inject]
        private void Contsruct(IScoreData[] data)
        {
            _data = data;
        }

        public void AddScoreOnCombine(int tier)
        {
            _score += _data[tier].ScoreOnCombine;
        }
    }
}
