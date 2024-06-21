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
        [SerializeField] private static int s_loadsCounter = 0;

        [Inject]
        private void Construct(CustomEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        void Start()
        {
            if (s_loadsCounter == 0)
            {
                _eventBus.Invoke(new FirstGameStartSignal());
            }
            _eventBus.Invoke(new GameStartSignal());
        }

        public async void RestartGame()
        {
            s_loadsCounter++;
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
            while (!asyncOperation.isDone)
            {
                await Task.Yield();
            }
        }
    }
}

