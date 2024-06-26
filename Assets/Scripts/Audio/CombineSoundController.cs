using CrystalProject.EventBus.Signals;
using UnityEngine;

namespace CrystalProject.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class CombineSoundController : MonoBehaviour
    {
        [SerializeField] private AudioClip[] _combineSounds;
        [SerializeField] private float _nextSoundWaiting = 1f;
        [SerializeField] private float _volumeMulti = 0.5f;
        [SerializeField] private int _soundCount;
        private float _nextSoundTime;
        private AudioSettings _settings;
        private AudioSource _audioSource;


        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            if (!transform.root.TryGetComponent(out _settings))
                Debug.LogError($"Missing {typeof(AudioSettings)} component.");

            _settings.OnSceneChangeStart += OnUnsubscribe;
            _settings.OnSceneChangeEnd += OnSubscribe;

            OnSubscribe();
        }


        private void OnDestroy()
        {
            OnUnsubscribe();
            _settings.OnSceneChangeStart -= OnUnsubscribe;
            _settings.OnSceneChangeEnd -= OnSubscribe;
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
            _settings.CustomEventBus.Unsubscribe<CombineSignal>(PlaySound);
        }


        private void OnSubscribe()
        {
            _settings.CustomEventBus.Subscribe<CombineSignal>(PlaySound);
        }

    }
}