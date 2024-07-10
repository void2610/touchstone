namespace NManager
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using System.Linq;

    public class SoundManager : MonoBehaviour
    {
        [System.Serializable]
        public class SoundData
        {
            public string name;
            public AudioClip audioClip;
            public float volume = 1.0f;
        }

        [SerializeField]
        private SoundData[] soundDatas;



        public static SoundManager instance;

        [SerializeField]
        private AudioSource bgmAudioSource;
        [SerializeField]
        private AudioClip bgmAudioClip;
        private AudioSource[] seAudioSourceList = new AudioSource[20];
        private float seVolume = 0.5f;
        private float bgmVolume = 0.5f;

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

            for (var i = 0; i < seAudioSourceList.Length; ++i)
            {
                seAudioSourceList[i] = gameObject.AddComponent<AudioSource>();
            }
        }

        public float BgmVolume
        {
            get
            {
                return bgmVolume;
            }
            set
            {
                bgmVolume = value;
                PlayerPrefs.SetFloat("BgmVolume", value);
            }
        }

        public float SeVolume
        {
            get
            {
                return seVolume;
            }
            set
            {
                seVolume = value;
                PlayerPrefs.SetFloat("SeVolume", value);
            }
        }

        public void PlayBgm(AudioClip clip, float volume = 1.0f)
        {
            bgmAudioSource.clip = clip;

            if (clip == null)
            {
                return;
            }
            if (volume > 0)
            {
                bgmAudioSource.volume = volume * BgmVolume;
            }
            bgmAudioSource.Play();
        }

        public void StopBgm()
        {
            bgmAudioSource.Stop();
        }

        public void PlaySe(AudioClip clip, float volume = 1.0f)
        {
            var audioSource = GetUnusedAudioSource();
            if (clip == null || audioSource == null)
            {
                return;
            }

            audioSource.clip = clip;
            audioSource.volume = volume * SeVolume;
        }

        public void PlaySe(string name, float volume = 1.0f)
        {
            var soundData = soundDatas.FirstOrDefault(t => t.name == name);
            var audioSource = GetUnusedAudioSource();
            if (soundData == null || audioSource == null)
            {
                return;
            }

            audioSource.clip = soundData.audioClip;
            audioSource.volume = soundData.volume * volume * SeVolume;
        }

        private AudioSource GetUnusedAudioSource() => seAudioSourceList.FirstOrDefault(t => t.isPlaying == false);

        private void Start()
        {
            BgmVolume = 0.5f;
            SeVolume = 0.5f;
            PlayBgm(bgmAudioClip, 1.0f);
        }
    }
}
