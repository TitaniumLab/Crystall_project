using Dropper.Animator;
using Dropper.Controller;
using Dropper.Model;
using System.Runtime.CompilerServices;
using UnityEngine;
using Zenject;

namespace Installers.Spawner
{
    public class SpawnerInstaller : MonoInstaller
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
