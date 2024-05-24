using CrystalProject.Dropper;
using CrystalProject.Units;
using UnityEngine;
using Zenject;

public class UnitDataInstaller : MonoInstaller
{
    [SerializeField] private UnitBundleData _unitBundleData;
    private IDropData[] _dropData;
    public override void InstallBindings()
    {

        int length = _unitBundleData.UnitData.Length;
        _dropData = new IDropData[length];
        for (int i = 0; i < length; i++)
        {
            _dropData[i] = _unitBundleData.UnitData[i];
        }
        Container.Bind<IDropData[]>().FromInstance(_dropData);
    }
}