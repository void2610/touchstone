namespace NManager
{
    using UnityEngine;
    using UnityEngine.Audio;
    using System.Collections;
    using System.Collections.Generic;
    using DG.Tweening;

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
        private AudioMixer mixer;
        [SerializeField]
        private List<SoundData> bgmList = new List<SoundData>();
        [SerializeField]
        private float cutoffFrequency = 200;
        private float defaultFrequency = 22000;
        private float cutoffVolume = 0.5f;

        private AudioSource audioSource => this.GetComponent<AudioSource>();
        private bool isPlaying = false;
        private SoundData currentBGM;
        private float volume = 0.5f;
        private float fadeTime = 1.5f;
        private float cutoffTime = 1.0f;
        private bool isFading = false;
        private AudioLowPassFilter lowPassFilter => this.GetComponent<AudioLowPassFilter>();

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

        public void EnableLowPassFilter()
        {
            float f;
            if (!mixer.GetFloat("LowPassCutoff", out f) || f == cutoffFrequency) return;
            audioSource.DOFade(volume * currentBGM.volume * cutoffVolume, cutoffTime).SetUpdate(true).SetEase(Ease.OutExpo);
            DOTween.To(() => f, x => mixer.SetFloat("LowPassCutoff", x), cutoffFrequency, cutoffTime).SetUpdate(true).SetEase(Ease.OutExpo);
        }

        public void DisableLowPassFilter()
        {
            float f;
            if (!mixer.GetFloat("LowPassCutoff", out f) || f == defaultFrequency) return;
            audioSource.DOFade(volume * currentBGM.volume, cutoffTime).SetUpdate(true).SetEase(Ease.OutExpo);
            DOTween.To(() => f, x => mixer.SetFloat("LowPassCutoff", x), defaultFrequency, cutoffTime).SetUpdate(true).SetEase(Ease.OutExpo);
        }


        public void Play()
        {
            if (currentBGM == null) return;

            isPlaying = true;
            audioSource.Play();
            audioSource.DOFade(volume * currentBGM.volume, fadeTime).SetUpdate(true).SetEase(Ease.InQuad);
        }

        public void Stop()
        {
            isPlaying = false;
            audioSource.DOFade(0, fadeTime).SetUpdate(true).SetEase(Ease.InQuad).OnComplete(() => audioSource.Stop());
        }

        private void PlayRandomBGM()
        {
            Debug.Log("PlayRandomBGM");
            audioSource.Stop();

            var bgm = bgmList[Random.Range(0, bgmList.Count)];
            currentBGM = bgm;
            audioSource.clip = currentBGM.audioClip;
            audioSource.volume = 0;

            audioSource.Play();
            audioSource.DOFade(volume * currentBGM.volume, fadeTime).SetUpdate(true).SetEase(Ease.InQuad).OnComplete(() => isFading = false);
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
            audioSource.volume = 0;
            if (playOnStart)
            {
                isPlaying = true;
                PlayRandomBGM();
            }
        }

        private void Update()
        {
            if (isPlaying && audioSource.clip != null)
            {
                float remainingTime = audioSource.clip.length - audioSource.time;
                if (remainingTime <= fadeTime && !isFading)
                {
                    isFading = true;
                    audioSource.DOFade(0, remainingTime).SetUpdate(true).SetEase(Ease.InQuad).OnComplete(() => PlayRandomBGM());
                }
            }
        }
    }
}
