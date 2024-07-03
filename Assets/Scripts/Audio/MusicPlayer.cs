using UnityEngine;

namespace CrystalProject.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class MusicPlayer : MonoBehaviour
    {
        private static MusicPlayer s_instance;
        [SerializeField] private AudioClip[] _clips;
        [SerializeField] private float _volumeMulti = 1;
        [SerializeField] private int _trackCount;
        private AudioSource _audioSource;
        private AudioSettings _audioSettings;

        private void Awake()
        {
            if (s_instance is not null)
            {
                Destroy(gameObject);
                return;
            }
            s_instance = this;

            _audioSource = GetComponent<AudioSource>();
            _audioSettings = GetComponentInParent<AudioSettings>();

            _audioSettings.OnMusicVolumeChange += ChangeVolume;
            RandomFirstTrack();
            PlayNextTrack();
        }


        private void OnDestroy()
        {
            if (_audioSettings is not null)
                _audioSettings.OnMusicVolumeChange -= ChangeVolume;
        }

        private void FixedUpdate()
        {
            if (_audioSource is not null && !_audioSource.isPlaying) // Why Audio end event doesnt exist? ¯\_(ツ)_/¯
            {
                PlayNextTrack();
            }
        }


        private void PlayNextTrack()
        {
            int nextTrack = (int)(_trackCount % _clips.Length);
            _audioSource.clip = _clips[nextTrack];
            _audioSource.volume = _audioSettings.MusicValume * _volumeMulti;
            _audioSource.Play();
            _trackCount++;
        }

        private void RandomFirstTrack()
        {
            _trackCount = Random.Range(0, _clips.Length);
        }

        private void ChangeVolume()
        {
            _audioSource.volume = _audioSettings.MusicValume;
        }
    }
}