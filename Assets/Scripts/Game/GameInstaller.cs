using CrystalProject.EventBus;
using CrystalProject.Score;
using CrystalProject.Units;
using CrystalProject.Units.Create;
using UnityEngine;
using Zenject;

namespace CrystalProject.Game
{
    public class GameInstaller : MonoInstaller
    {
        private CustomEventBus _customEventBus;
        private ScoreModel _scoreModel;
        [SerializeField] private Transform _parentOfGameUnits;
        private UnitController _unitController;
        [SerializeField] private GameController _gameController;

        public override void InstallBindings()
        {
            // Create and bind EventBus
            _customEventBus = new CustomEventBus();
            Container.Bind<CustomEventBus>().FromInstance(_customEventBus).AsSingle();

            // Create, bind and inject ScoreModel
            _scoreModel = new ScoreModel();
            Container.QueueForInject(_scoreModel);
            Container.Bind<ScoreModel>().FromInstance(_scoreModel).AsSingle();
            Container.Bind<IScore>().FromInstance(_scoreModel).AsSingle();

            // Create and bind Unit Creator
            _unitController = new UnitController(_parentOfGameUnits);
            Container.QueueForInject(_unitController);
            Container.Bind<IUnitDispenser>().FromInstance(_unitController);

            // Bind and inject GameController
            Container.Bind<GameController>().FromInstance(_gameController);
        }
    }
}