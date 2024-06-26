using CrystalProject.EventBus.Signals;
using UnityEngine;

namespace CrystalProject.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class ScoreThresholdSoundController : MonoBehaviour
    {
        [SerializeField] private AudioClip _clip;
        [SerializeField] private float _volumeMulti = 1f;
        private AudioSettings _settings;
        private AudioSource _audioSource;


        private void Awake()
        {
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
            _settings.OnSceneChangeStart -= OnUnsubscribe;
            _settings.OnSceneChangeEnd -= OnSubscribe;
        }


        private void PlayClip(ScoreThresholdSignal signal)
        {
            _audioSource.clip = _clip;
            _audioSource.volume = _settings.SFXValume * _volumeMulti;
            _audioSource.Play();
        }


        private void OnUnsubscribe()
        {
            _settings.CustomEventBus.Unsubscribe<ScoreThresholdSignal>(PlayClip);
        }


        private void OnSubscribe()
        {
            _settings.CustomEventBus.Subscribe<ScoreThresholdSignal>(PlayClip);
        }
    }
}