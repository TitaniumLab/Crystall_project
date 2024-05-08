using CrystalProject.Dropper;
using CrystalProject.Score;
using UnityEngine;
using Zenject;

namespace CrystalProject
{
    public class UnitInstaller : MonoInstaller
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
