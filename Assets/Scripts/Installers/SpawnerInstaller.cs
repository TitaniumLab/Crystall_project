using CrystalProject.Dropper;
using UnityEngine;
using Zenject;

namespace CrystalProject.Installers
{
    public class SpawnerInstaller : MonoInstaller
    {
        [SerializeField] private DropModel _dropModel;
        [SerializeField] private DropAnimator _dropAnimator;
        [SerializeField] private ScoreModel _scoreModel;

        public override void InstallBindings()
        {
            Container.Bind<DropModel>().FromInstance(_dropModel);
            Container.Bind<DropAnimator>().FromInstance(_dropAnimator);
            Container.Bind<ScoreModel>().FromInstance(_scoreModel);
        }
    }
}
