using System;
using System.Collections.Generic;
using System.Linq;
using _0.Common.Scripts;
using _0.Common.Scripts.BaseCore;
using TMPro;
using UnityEngine;

namespace _0.Game.Scripts.Gameplay
{
    public class UIController : BaseUIGameplay
    {
        public static UIController instance;
        public GameSettingPanel settting;

        public GameObject startObj;
        public Transform buttonGroup;
        public Transform suggestGroup;
        public List<Sprite> icon;
        public List<ButtonShape> buttons;
        public List<ResultUI> results;
        public TextMeshProUGUI levelText;
        public GameObject fx;
        public GameObject tutorial;
        private void Awake()
        {
            instance = this;
            levelText.text = $"Level {PlayerData.currentLevel + 1}";
            
        }

        public void ShowTut()
        {
            AudioManager.instance?.PlayButtonClick();
            tutorial.SetActive(true);
        }
        public void HideTut()
        {
            AudioManager.instance?.PlayButtonClick();
            tutorial.SetActive(false);
        }
        public void CorrectShape(int currentShape)
        {
            results[currentShape].Correct();
        }

        public void SetUpResult(List<GameController.ShapeType> shape)
        {
            for (int i = 0; i < results.Count; i++)
            {
                var resultUI = results[i];
                resultUI.gameObject.SetActive(i < shape.Count);
                if (i < shape.Count)
                {
                    var shapeType = shape[i];
                    resultUI.setUp(icon[(int)shapeType]);
                }
            }
        }

        public void TapToStart()
        {
            AudioManager.instance?.PlaySfx(AudioManager.instance.start);
            startObj.SetActive(false);
            GameController.instance.gameOver = false;
            GameController.instance.StartGroup();
            suggestGroup.gameObject.SetActive(true);
            buttonGroup.gameObject.SetActive(true);
        }

        public void SetUpButton()
        {
            var ll = GameController.instance.shapeInWave.ToList();
            foreach (var a in buttons)
            {
                a.gameObject.SetActive(ll.Contains(a.shape));
            }
        }
        
        public void ShowGameOver(bool result)
        {
            gameOver.SetResult(result);
            fx.SetActive(result);
            gameOver.gameObject.SetActive(true);
        }

        public override void Pause()
        {
            base.Pause();
            Time.timeScale = 0;
            settting.gameObject.SetActive(true);
        }
    }
}