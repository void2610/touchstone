namespace NMap
{
    using UnityEngine;
    using System.Collections.Generic;
    using NManager;

    public class EnvironmentAudio : MonoBehaviour
    {
        [SerializeField]
        private List<AudioClip> wdAudioClips = new List<AudioClip>();
        private AudioSource bgmAudioSource => GetComponents<AudioSource>()[0];
        private AudioSource wdAudioSource => GetComponents<AudioSource>()[1];

        void PlayBgmAudioSource() => bgmAudioSource.Play();
        private void PlayWdAudioSource()
        {
            wdAudioSource.clip = wdAudioClips[Random.Range(0, wdAudioClips.Count)];
            wdAudioSource.Play();
            Invoke("PlayWdAudioSource", Random.Range(2f, 4f));
        }

        void Start()
        {
            bgmAudioSource.volume = 0.2f * BGMManager.instance.BgmVolume;
            wdAudioSource.volume = 0.75f * BGMManager.instance.BgmVolume;


            Invoke("PlayBgmAudioSource", 1f);
            Invoke("PlayWdAudioSource", 1f);
        }

        void Update()
        {

        }
    }
}
