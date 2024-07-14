namespace NMap
{
    using UnityEngine;
    using NManager;

    public class EnvironmentAudio : MonoBehaviour
    {
        private AudioSource bgmAudioSource => GetComponents<AudioSource>()[0];
        private AudioSource wdAudioSource => GetComponents<AudioSource>()[1];
        void PlayBgmAudioSource() => bgmAudioSource.Play();
        void PlayWdAudioSource() => wdAudioSource.Play();

        void Start()
        {
            bgmAudioSource.volume = 0.1f * SoundManager.instance.BgmVolume;
            wdAudioSource.volume = 0.5f * SoundManager.instance.BgmVolume;


            Invoke("PlayBgmAudioSource", 1f);
            Invoke("PlayWdAudioSource", 1f);
        }

        void Update()
        {

        }
    }
}
