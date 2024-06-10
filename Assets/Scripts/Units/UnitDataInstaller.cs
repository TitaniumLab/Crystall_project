using CrystalProject.Combine;
using CrystalProject.Dropper;
using CrystalProject.Score;
using CrystalProject.Units;
using CrystalProject.Units.Data;
using UnityEngine;
using Zenject;

public class UnitDataInstaller : MonoInstaller
{
    [SerializeField] private UnitBundleData _unitBundleData;
    private IDropData[] _dropData;
    private ICombineData[] _combineData;
    private IUnitData[] _unitData;
    private IScoreData[] _scoreData;
    private IScoreThresholdImages[] _scoreImages;
    public override void InstallBindings()
    {

        int length = _unitBundleData.UnitData.Length;
        _dropData = new IDropData[length];
        _combineData = new ICombineData[length];
        _unitData = new IUnitData[length];
        _scoreData = new IScoreData[length];
        _scoreImages = new IScoreThresholdImages[length];
        for (int i = 0; i < length; i++)
        {
            _dropData[i] = _unitBundleData.UnitData[i];
            _combineData[i] = _unitBundleData.UnitData[i];
            _unitData[i] = _unitBundleData.UnitData[i];
            _scoreData[i] = _unitBundleData.UnitData[i];
            _scoreImages[i] = _unitBundleData.UnitData[i];
        }
        Container.Bind<IDropData[]>().FromInstance(_dropData);
        Container.Bind<ICombineData[]>().FromInstance(_combineData);
        Container.Bind<IUnitData[]>().FromInstance(_unitData);
        Container.Bind<IScoreData[]>().FromInstance(_scoreData);
        Container.Bind<IScoreThresholdImages[]>().FromInstance(_scoreImages);
    }
}