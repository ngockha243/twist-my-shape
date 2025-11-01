using System;
using _0.Common.Scripts;
using _0.Common.Scripts.BaseCore;
using UnityEngine;

namespace _0.Game.Scripts
{
    public class GameSettingPanel : MonoBehaviour
    {
        public GameObject musicOnObj;
        public GameObject musicOffObj;
        public GameObject soundOnObj;
        public GameObject soundOffObj;

        public Transform content;

        private void OnEnable()
        {
            content.ShowPopup();
        }

        private void Start()
        {
            musicOffObj.SetActive(PlayerData.MusicVolume == 0);
            musicOnObj.SetActive(PlayerData.SfxVolume == 1);

            soundOffObj.SetActive(PlayerData.SfxVolume == 0);
            soundOnObj.SetActive(PlayerData.MusicVolume == 1);
        }

        public void OnMusicClick()
        {
            if (PlayerData.MusicVolume == 0) PlayerData.MusicVolume = 1;
            else PlayerData.MusicVolume = 0;
            musicOffObj.SetActive(PlayerData.MusicVolume == 0);
            musicOnObj.SetActive(PlayerData.MusicVolume == 1);

            AudioManager.instance.SetSfxVolume(PlayerData.SfxVolume);
            AudioManager.instance.SetMusicVolume(PlayerData.MusicVolume);
        }

        public void OnSFXClick()
        {
            if (PlayerData.SfxVolume == 0) PlayerData.SfxVolume = 1;
            else PlayerData.SfxVolume = 0;
            soundOffObj.SetActive(PlayerData.SfxVolume == 0);
            soundOnObj.SetActive(PlayerData.SfxVolume == 1);
            AudioManager.instance.SetSfxVolume(PlayerData.SfxVolume);
            AudioManager.instance.SetMusicVolume(PlayerData.MusicVolume);
        }

        public void Close()
        {
            Time.timeScale = 1;
            AudioManager.instance?.PlayButtonClick();
            gameObject.SetActive(false);
        }
    }
}