using UnityEngine;

namespace CrystalProject.Internal
{
    public class Singleton : MonoBehaviour
    {
        private static Singleton s_instance;

        private void Awake()
        {
            if (s_instance is not null)
            {
                Destroy(this);
                return;
            }

            s_instance = this;
            DontDestroyOnLoad(this);
        }
    }
}