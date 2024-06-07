using CrystalProject.EventBus;
using CrystalProject.EventBus.Signals;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
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

        public async void RestartGame()
        {
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
            while (!asyncOperation.isDone)
            {
                await Task.Yield();
            }
        }
    }
}

