using CrystalProject.EventBus.Signals;
using UnityEngine;

namespace CrystalProject.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class ScoreThresholdSoundController : MonoBehaviour
    {
        private static ScoreThresholdSoundController s_instance;
        [SerializeField] private AudioClip _clip;
        [SerializeField] private float _volumeMulti = 1f;
        private AudioSettings _settings;
        private AudioSource _audioSource;


        private void Awake()
        {
            if (s_instance is not null)
            {
                return;
            }
            s_instance = this;

            _settings = transform.root.GetComponentInParent<AudioSettings>();
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
            if (_settings is not null)
            {
                _settings.OnSceneChangeStart -= OnUnsubscribe;
                _settings.OnSceneChangeEnd -= OnSubscribe;
            }
        }


        private void PlayClip(ScoreThresholdSignal signal)
        {
            _audioSource.clip = _clip;
            _audioSource.volume = _settings.SFXValume * _volumeMulti;
            _audioSource.Play();
        }


        private void OnUnsubscribe()
        {
            _settings?.CustomEventBus.Unsubscribe<ScoreThresholdSignal>(PlayClip);
        }


        private void OnSubscribe()
        {
            _settings.CustomEventBus.Subscribe<ScoreThresholdSignal>(PlayClip);
        }
    }
}