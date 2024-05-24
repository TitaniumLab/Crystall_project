using CrystalProject.Units.Create;
using UnityEngine;
using Zenject;

namespace CrystalProject.Units
{
    public class UnitControllerInstaller : MonoInstaller
    {
        [SerializeField] private UnitController _unitController;
        public override void InstallBindings()
        {
            Container.Bind<IUnitDispenser>().FromInstance(_unitController);
        }
    }
}