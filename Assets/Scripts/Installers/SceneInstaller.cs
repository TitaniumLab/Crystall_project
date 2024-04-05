using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    [SerializeField] private DropController _dropController;
    public override void InstallBindings()
    {
        Container.Bind<IDropper>().FromInstance(_dropController).NonLazy();
    }
}