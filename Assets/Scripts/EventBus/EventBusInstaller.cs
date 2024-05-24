using Zenject;

namespace CrystalProject.EventBus
{
    public class EventBusInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<CustomEventBus>().FromNew().AsSingle();
        }
    }
}