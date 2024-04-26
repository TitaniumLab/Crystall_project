using UnityEngine;
using Zenject;

public class LossInstaller : MonoInstaller
{
    [SerializeField] private LossController _lossController;
    public override void InstallBindings()
    {
        Container.Bind<LossController>().FromInstance(_lossController);
    }
}