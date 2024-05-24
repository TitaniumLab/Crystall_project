using CrystalProject.EventBus;
using CrystalProject.EventBus.Signals;
using UnityEngine;
using Zenject;

namespace CrystalProject.Game
{
    public class GameController : MonoBehaviour
    {
        private CustomEventBus _eventBus;
        void Start()
        {
            _eventBus.Invoke(new GameStartSignal());
        }

        [Inject]
        private void Construct(CustomEventBus eventBus)
        {
            _eventBus = eventBus;
        }
    }
}

