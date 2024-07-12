using CrystalProject.EventBus;
using System;
using UnityEngine;
using UnityEngine.UI;
using YG;
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
        [SerializeField] private Button _optionsOk;

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
                s_instance.InitPrevious(_sfxVolumeSlider, _musicVolumeSlider, CustomEventBus, _optionsOk);
                Destroy(gameObject);
                return;
            }

            s_instance = this;

            _optionsOk.onClick.AddListener(SaveAudio);
            _sfxVolumeSlider.onValueChanged.AddListener(delegate { ChangeSFXVolume(); });
            _musicVolumeSlider.onValueChanged.AddListener(delegate { ChangeMusicVolume(); });
        }


        private void Start()
        {
            _sfxValume = YandexGame.savesData.SFXValue;
            _musicValume = YandexGame.savesData.MusicValue;

            SetSliders();
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

        private void OnApplicationPause(bool pause)
        {
            AudioListener.volume = pause ? 0 : 1;
        }

        private void ChangeMusicVolume()
        {
            _musicValume = _musicVolumeSlider.value;
            OnMusicVolumeChange?.Invoke();
        }


        private void InitPrevious(Slider sfxSlider, Slider musicSlider, CustomEventBus customEventBus, Button okButton)
        {
            _sfxVolumeSlider.onValueChanged.RemoveListener(delegate { ChangeSFXVolume(); });
            _musicVolumeSlider.onValueChanged.RemoveListener(delegate { ChangeMusicVolume(); });
            _optionsOk.onClick.RemoveListener(SaveAudio);
            OnSceneChangeStart?.Invoke();

            _optionsOk = okButton;
            _sfxVolumeSlider = sfxSlider;
            _musicVolumeSlider = musicSlider;
            CustomEventBus = customEventBus;
            OnSceneChangeEnd?.Invoke();

            _optionsOk.onClick.AddListener(SaveAudio);
            _sfxVolumeSlider.onValueChanged.AddListener(delegate { ChangeSFXVolume(); });
            _musicVolumeSlider.onValueChanged.AddListener(delegate { ChangeMusicVolume(); });

            SetSliders();
        }


        private void SetSliders()
        {
            _sfxVolumeSlider.value = _sfxValume;
            _musicVolumeSlider.value = _musicValume;
        }

        public void SaveAudio()
        {
            YandexGame.savesData.SFXValue = _sfxValume;
            YandexGame.savesData.MusicValue = _musicValume;
            YandexGame.SaveProgress();
        }
    }
}

