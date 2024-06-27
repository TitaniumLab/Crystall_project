using CrystalProject.EventBus.Signals;
using UnityEngine;
using UnityEngine.UI;

namespace CrystalProject.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class CombineSoundController : MonoBehaviour, ISoundCheckable
    {
        private static CombineSoundController s_instance;
        [SerializeField] private AudioClip[] _combineSounds;
        [SerializeField] private float _nextSoundWaiting = 1f;
        [SerializeField] private float _volumeMulti = 0.5f;
        [SerializeField] private int _soundCount;
        [SerializeField] private int _soundIndexOnCheck;

        [SerializeField] private Slider _slider;
        [SerializeField] private int count = 0;

        private float _nextSoundTime;
        private AudioSettings _settings;
        private AudioSource _audioSource;

        private void Awake()
        {
            if (s_instance is not null)
            {
                Destroy(gameObject);
                return;
            }
            s_instance = this;

            _audioSource = GetComponent<AudioSource>();
            if (!transform.root.TryGetComponent(out _settings))
                Debug.LogError($"Missing {typeof(AudioSettings)} component.");

            _settings.OnSceneChangeStart += OnUnsubscribe;
            _settings.OnSceneChangeEnd += OnSubscribe;
            count++;
            OnSubscribe();
        }


        private void OnDestroy()
        {
            OnUnsubscribe();
            if (_settings is not null)
            {
                _settings.OnSceneChangeStart -= OnUnsubscribe;
                _settings.OnSceneChangeEnd -= OnSubscribe;
            }

        }


        private void PlaySound(CombineSignal signal)
        {
            if (Time.time < _nextSoundTime && _soundCount < _combineSounds.Length - 1)
            {
                _soundCount++;
            }
            else if (Time.time > _nextSoundTime && _soundCount < _combineSounds.Length)
            {
                _soundCount = 0;
            }

            _audioSource.clip = _combineSounds[_soundCount];
            _audioSource.volume = _settings.SFXValume * _volumeMulti;
            _audioSource.Play();

            _nextSoundTime = Time.time + _nextSoundWaiting;
        }


        private void OnUnsubscribe()
        {
            _settings?.CustomEventBus.Unsubscribe<CombineSignal>(PlaySound);
        }


        private void OnSubscribe()
        {
            _settings.CustomEventBus.Subscribe<CombineSignal>(PlaySound);
            if (_settings.SFXSlider.TryGetComponent(out ISoundChecker component))
            {
                component.SoundChecker = this;
            }
            else
            {
                Debug.LogError($"Missing {typeof(ISoundChecker)} component.");
            }
        }

        public void SoundCheck()
        {
            _audioSource.clip = _combineSounds[_soundIndexOnCheck];
            _audioSource.volume = _settings.SFXValume * _volumeMulti;
            _audioSource.Play();
        }

    }
}