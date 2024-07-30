namespace NManager
{
    using UnityEngine;
    using System.Collections;
    using System.Collections.Generic;

    public class BGMManager : MonoBehaviour
    {
        [System.Serializable]
        public class SoundData
        {
            public AudioClip audioClip;
            public float volume = 1.0f;
        }

        public static BGMManager instance;
        [SerializeField]
        private bool playOnStart = true;
        [SerializeField]
        private List<SoundData> bgmList = new List<SoundData>();

        private AudioSource audioSource => this.GetComponent<AudioSource>();
        private bool isPlaying = false;
        private SoundData currentBGM;
        private float volume = 0.5f;

        public float BgmVolume
        {
            get
            {
                return volume;
            }
            set
            {
                volume = value;
                PlayerPrefs.SetFloat("BgmVolume", value);
                audioSource.volume = currentBGM != null ? volume * currentBGM.volume : volume;
            }
        }

        public void Play()
        {
            isPlaying = true;
            audioSource.Play();
        }

        public void Stop()
        {
            isPlaying = false;
            audioSource.Stop();
        }

        private void PlayRandomBGM()
        {
            var bgm = bgmList[Random.Range(0, bgmList.Count)];
            currentBGM = bgm;
            audioSource.clip = currentBGM.audioClip;
            audioSource.volume = volume * currentBGM.volume;
            audioSource.Play();
        }

        // TODO: フェード
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        private void Start()
        {
            volume = PlayerPrefs.GetFloat("BgmVolume", 0.5f);
            audioSource.volume = volume;
            if (playOnStart)
            {
                isPlaying = true;
                PlayRandomBGM();
            }
        }

        private void Update()
        {
            if (!audioSource.isPlaying && isPlaying)
            {
                PlayRandomBGM();
            }
        }
    }
}
