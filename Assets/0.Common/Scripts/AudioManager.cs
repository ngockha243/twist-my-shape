using System;
using UnityEngine;

namespace _0.Common.Scripts
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager instance;
        [Header("Music")] public AudioClip musicClip;

        [Header("Button SFX")] public AudioClip clickClip;
        public AudioClip wrong;
        public AudioClip right;
        public AudioClip win;
        public AudioClip lose;
        public AudioClip pop;
        public AudioClip ding;
        public AudioClip start;
        public AudioClip firework;
        public AudioClip boom;
        private AudioSource _musicSrc;
        private AudioSource _sfxSrc;
        private AudioSource _sfxCollider;

        private void Awake()
        {
            if (instance == null) instance = this;
            else
            {
                Destroy(gameObject);
                return;
            };
            DontDestroyOnLoad(gameObject);

            _musicSrc = gameObject.AddComponent<AudioSource>();
            _musicSrc.loop = true;

            _sfxSrc = gameObject.AddComponent<AudioSource>();
            _sfxCollider = gameObject.AddComponent<AudioSource>();
            _sfxSrc.loop = false;
            _sfxCollider.loop = false;

            SetSfxVolume(PlayerData.SfxVolume);
            SetMusicVolume(PlayerData.MusicVolume);
            Init();
        }
        public virtual void Init(){}
        private void Start()
        {
            if (musicClip != null) PlayMusic(musicClip, true);
        }

        // ===== Music =====
        public void PlayMusic(AudioClip clip, bool loop = true)
        {
            if (clip == null) return;
            _musicSrc.clip = clip;
            _musicSrc.loop = loop;
            _musicSrc.volume = PlayerData.MusicVolume;
            _musicSrc.Play();
        }

        public void StopMusic() => _musicSrc.Stop();

        public void SetMusicVolume(float v)
        {
            _musicSrc.volume = PlayerData.MusicVolume;
        }

        // ===== SFX =====
        public void PlaySfx(AudioClip clip)
        {
            if (clip == null) return;
            var src = gameObject.AddComponent<AudioSource>();
            src.clip = clip;
            src.volume = PlayerData.SfxVolume;
            src.Play();
            Destroy(src, clip.length);
        }

        public void SetSfxVolume(float v)
        {
            _sfxSrc.volume = PlayerData.SfxVolume;
            _sfxCollider.volume = PlayerData.SfxVolume;
        }

        // Nút phổ biến
        public void PlayButtonClick() => PlaySfx(clickClip);
        public void PlayWrong() => PlaySfx(wrong);
        public void PlayRight() => PlaySfx(right);

        public void PlayWin() => PlaySfx(win);
        public void PlayLose() => PlaySfx(lose);
        
        
        
    }
}