using UnityEngine;
using Zenject;

namespace CrystalProject.Score
{
    public class ScoreInstaller : MonoInstaller
    {
        [SerializeField] private ScoreModel _scoreModel;

        public override void InstallBindings()
        {
            Container.Bind<ScoreModel>().FromInstance(_scoreModel);
        }
    }
}