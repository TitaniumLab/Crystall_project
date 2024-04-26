using CrystalProject;
using System;
using UnityEngine;
using Zenject;

public class ScoreModel : MonoBehaviour
{
    [field: SerializeField] public int Score { get; private set; }
    public event Action<int> OnScoreChange;

    private void Awake()
    {
        Spawner.OnSpawn += AddScore;
    }
    //[Inject]
    //private void Construct(Spawner spawner)
    //{
    //    _spawner = spawner;
    //    _spawner.OnSpawn += AddScore;
    //}

    private void AddScore(int score)
    {
        Score += score;
        OnScoreChange(Score);
    }
}
