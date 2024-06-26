using UnityEngine;

namespace CrystalProject.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class MusicPlayer : MonoBehaviour
    {
        [SerializeField] private AudioClip[] _clips;
        [SerializeField] private float _volumeMulti = 1;
        [SerializeField] private int _trackCount;
        private float _nextTrackTime;
        private AudioSource _audioSource;
        private AudioSettings _audioSettings;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            if (!transform.root.TryGetComponent(out _audioSettings))
                Debug.LogError($"Missing {typeof(AudioSettings)} component.");

            _audioSettings.OnMusicVolumeChange += ChangeVolume;
            PlayNextTrack();
        }

        private void OnDestroy()
        {
            _audioSettings.OnMusicVolumeChange += ChangeVolume;
        }

        private void PlayNextTrack()
        {
            int nextTrack = (int)(_trackCount % _clips.Length);
            _audioSource.clip = _clips[nextTrack];
            _audioSource.volume = _audioSettings.MusicValume * _volumeMulti;
            _audioSource.Play();
            _nextTrackTime = Time.time + _clips[nextTrack].length;
            Debug.LogWarning(_nextTrackTime);
            Invoke(nameof(PlayNextTrack), _nextTrackTime);
            _trackCount++;
        }

        private void ChangeVolume()
        {
            _audioSource.volume = _audioSettings.MusicValume;
        }
    }
}