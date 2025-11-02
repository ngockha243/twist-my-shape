using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _0.Common.Scripts.BaseCore
{
    public class BaseGameOverPanel : UIPanelBase
    {
        public TextMeshProUGUI title;
        public Image titleImg;
        public Button btnNext;
        public Transform content;
        private bool isWin = false;
        private void OnEnable()
        {
            if(content != null)
             content?.ShowPopup();
        }

        public virtual void SetResult(bool isWin)
        {
            if (isWin) AudioManager.instance?.PlayWin();
            else AudioManager.instance?.PlayLose();
            string t = isWin ? "Mission Complete" : "Mission Failed";
            titleImg.color = isWin ? Color.green : Color.red;
            title.text = t;
            btnNext.interactable = isWin;
            this.isWin = isWin;
            //
        }

        public void Home()
        {
            AudioManager.instance?.PlayButtonClick();
            Time.timeScale = 1;
            if(isWin) PlayerData.currentLevel += 1;
            string sceneName = $"Gameplay";
            SceneManager.LoadScene(sceneName);
        }

        public void RestartScene()
        {
            AudioManager.instance?.PlayButtonClick();
            Time.timeScale = 1;

            string sceneName = $"{Common.GetLevelName(PlayerData.currentLevel)}";
            SceneManager.LoadScene(sceneName);
        }

        public void RestartSceneData()
        {
            AudioManager.instance?.PlayButtonClick();
            Time.timeScale = 1;

            string sceneName = $"Gameplay";
            SceneManager.LoadScene(sceneName);
        }

        public void NextScene()
        {
            AudioManager.instance?.PlayButtonClick();
            Time.timeScale = 1;
            PlayerData.currentLevel += 1;
            string sceneName = $"{Common.GetLevelName(PlayerData.currentLevel)}";
            PlayerData.currentGold += (PlayerData.currentLevel * 100);
            SceneManager.LoadScene(sceneName);
        }

        public void NextSceneData()
        {
            AudioManager.instance?.PlayButtonClick();
            Time.timeScale = 1;
            PlayerData.currentLevel += 1;
            string sceneName = $"Gameplay";
            PlayerData.currentGold += (PlayerData.currentLevel * 100);
            SceneManager.LoadScene(sceneName);
        }
    }
}