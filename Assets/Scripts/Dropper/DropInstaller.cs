using UnityEngine;
using Zenject;

namespace CrystalProject.Dropper
{
    public class DropInstaller : MonoInstaller
    {
        [SerializeField] private DropModel _dropModel;
        [SerializeField] private DropAnimator _dropAnimator;
        public override void InstallBindings()
        {
            Container.Bind<DropModel>().FromInstance(_dropModel);
            Container.Bind<DropAnimator>().FromInstance(_dropAnimator);
        }
    }
}