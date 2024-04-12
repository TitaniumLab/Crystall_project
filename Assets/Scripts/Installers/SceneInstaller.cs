using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    [SerializeField] private DropController _dropController;
    [SerializeField] private DropAnimator _dropAnimator;
    public override void InstallBindings()
    {
        Container.Bind<IDropper>().FromInstance(_dropController).NonLazy();
        Container.Bind<IAnim>().FromInstance(_dropAnimator).NonLazy();
    }
}