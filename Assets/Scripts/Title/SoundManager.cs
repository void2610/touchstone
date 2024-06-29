namespace NTitle
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class SoundManager : MonoBehaviour
    {
        public static SoundManager instance;
        void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
        [SerializeField]
        private AudioSource bgmAudioSource;
        [SerializeField]
        private AudioSource seAudioSource;

        [SerializeField]
        private AudioClip bgmAudioClip;

        public float BgmVolume
        {
            get
            {
                return bgmAudioSource.volume;
            }
            set
            {
                bgmAudioSource.volume = Mathf.Clamp01(value);
            }
        }

        public float SeVolume
        {
            get
            {
                return seAudioSource.volume;
            }
            set
            {
                seAudioSource.volume = Mathf.Clamp01(value);
            }
        }

        public void PlayBgm(AudioClip clip, float volume = -1.0f)
        {
            bgmAudioSource.clip = clip;

            if (clip == null)
            {
                return;
            }
            if (volume > 0)
            {
                bgmAudioSource.volume = volume;
            }
            bgmAudioSource.Play();
        }

        public void StopBgm()
        {
            bgmAudioSource.Stop();
        }

        public void PlaySe(AudioClip clip, float volume = -1.0f)
        {
            if (clip == null)
            {
                return;
            }
            if (volume > 0)
            {
                seAudioSource.volume = volume;
            }
            seAudioSource.PlayOneShot(clip);
        }

        private void Start()
        {
            BgmVolume = 0.5f;
            SeVolume = 0.5f;
            PlayBgm(bgmAudioClip);
        }

    }
}
