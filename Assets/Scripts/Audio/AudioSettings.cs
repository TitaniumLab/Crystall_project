using CrystalProject.EventBus;
using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CrystalProject.Audio
{
    public class AudioSettings : MonoBehaviour
    {
        private static AudioSettings s_instance;
        [SerializeField, Range(0, 1f)] private float _sfxValume = 1f;
        [SerializeField, Range(0, 1f)] private float _musicValume = 1f;
        [SerializeField] private Slider _sfxVolumeSlider;
        [SerializeField] private Slider _musicVolumeSlider;

        public CustomEventBus CustomEventBus { get; private set; }
        public float SFXValume { get => _sfxValume; }
        public float MusicValume { get => _musicValume; }
        public Slider SFXSlider { get => _sfxVolumeSlider; }

        public event Action OnMusicVolumeChange;
        public event Action OnSceneChangeStart;
        public event Action OnSceneChangeEnd;


        [Inject]
        private void Construct(CustomEventBus customEventBus)
        {
            CustomEventBus = customEventBus;
        }


        private void Awake()
        {
            if (s_instance is not null)
            {
                s_instance.InitPrevious(_sfxVolumeSlider, _musicVolumeSlider, CustomEventBus);
                Destroy(gameObject);
                return;
            }

            s_instance = this;
            SetSliders();
            _sfxVolumeSlider.onValueChanged.AddListener(delegate { ChangeSFXVolume(); });
            _musicVolumeSlider.onValueChanged.AddListener(delegate { ChangeMusicVolume(); });
            DontDestroyOnLoad(gameObject);
        }


        private void OnDestroy()
        {
            _sfxVolumeSlider.onValueChanged.RemoveListener(delegate { ChangeSFXVolume(); });
            _musicVolumeSlider.onValueChanged.RemoveListener(delegate { ChangeMusicVolume(); });
        }


        private void ChangeSFXVolume()
        {
            _sfxValume = _sfxVolumeSlider.value;
        }


        private void ChangeMusicVolume()
        {
            _musicValume = _musicVolumeSlider.value;
            OnMusicVolumeChange?.Invoke();
        }


        private void InitPrevious(Slider sfxSlider, Slider musicSlider, CustomEventBus customEventBus)
        {
            _sfxVolumeSlider.onValueChanged.RemoveListener(delegate { ChangeSFXVolume(); });
            _musicVolumeSlider.onValueChanged.RemoveListener(delegate { ChangeMusicVolume(); });
            OnSceneChangeStart?.Invoke();

            _sfxVolumeSlider = sfxSlider;
            _musicVolumeSlider = musicSlider;
            CustomEventBus = customEventBus;
            OnSceneChangeEnd?.Invoke();

            _sfxVolumeSlider.onValueChanged.AddListener(delegate { ChangeSFXVolume(); });
            _musicVolumeSlider.onValueChanged.AddListener(delegate { ChangeMusicVolume(); });

            SetSliders();
        }


        private void SetSliders()
        {
            _sfxVolumeSlider.value = _sfxValume;
            _musicVolumeSlider.value = _musicValume;
        }
    }
}

