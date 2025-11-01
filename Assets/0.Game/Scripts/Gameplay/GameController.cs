using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _0.Common.Scripts;
using _0.Common.Scripts.BaseCore;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace _0.Game.Scripts.Gameplay
{
    public class GameController : GameControllerBase
    {
        public static GameController instance;
        public Player player;
        public List<MapData> levelData;
        public Shape shapePrefabs;
        public List<ShapeParent> spawnParent;
        public Material yellowMat;
        public Material orangeMat;
        public Transform endPos;
        public HashSet<ShapeType> shapeInWave = new HashSet<ShapeType>();
        private MapData currentLevel;
        private int currentWave;
        private int currentGroup;

        public enum ShapeType
        {
            Circle,
            Cresent,
            Diamond,
            Hexagon,
            Square,
            Star,
        }

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            currentLevel = levelData[PlayerData.currentLevel];
            SpawnWave();
        }


        public void NextWave()
        {
            StartCoroutine(NextWaveIE());
        }

        private IEnumerator NextWaveIE()
        {
            currentWave += 1;
            yield return new WaitForSeconds(1);
            if (currentWave >= currentLevel.waves.Count)
            {
                GameOver(true);
            }
            else
            {
                SpawnWave();
                StartGroup();
            }
        }

        private void SpawnWave()
        {
            var wave = currentLevel.waves[currentWave];
            shapeInWave.Clear();
            var shapeInWaves = wave.shapeInWaves;
            for (int i = 0; i < shapeInWaves.Count; i++)
            {
                var shape = shapeInWaves[i];
                var parent = spawnParent[i];
                float posY = 0;
                for (int j = 0; j < shape.shapes.Count; j++)
                {
                    var shapeType = shape.shapes[j];
                    var obj = Instantiate(shapePrefabs, parent.transform);
                    obj.transform.localPosition = new Vector3(0, posY, 0);
                    obj.SetUp(shapeType, parent, j);
                    parent.shapes.Add(obj);
                    shapeInWave.Add(shapeType);
                    posY -= 1;
                }
            }

            UIController.instance.SetUpResult(shapeInWaves[0].shapes);
            UIController.instance.SetUpButton();
            currentGroup = 0;
        }

        public void NextGroup()
        {
            currentGroup += 1;
            if (currentGroup >= currentLevel.waves[currentWave].shapeInWaves.Count)
            {
                NextWave();
            }
            else StartCoroutine(NextParentIE());
        }

        public void StartGroup()
        {
            StartCoroutine(NextParentIE());
        }

        private IEnumerator NextParentIE()
        {
            UIController.instance.SetUpResult(currentLevel.waves[currentWave].shapeInWaves[currentGroup].shapes);
            var group = spawnParent[currentGroup];
            // player.transform.LookAt(group.transform);
            // var rot = player.transform.rotation.eulerAngles;
            // rot.x = 0f;
            // player.transform.rotation = Quaternion.Euler(rot);
            Vector3 dir = group.transform.position - player.transform.position;
            dir.y = 0f;

            Quaternion targetRot = Quaternion.LookRotation(dir);
            player.transform.DORotateQuaternion(targetRot, 0.5f);
            yield return new WaitForSeconds(1);
            group.Move();
        }

        public void ChangeShape(ShapeType shapeType)
        {
            player.ChangeShape(shapeType);
        }

        public override void GameOver(bool result)
        {
            base.GameOver(result);
            AudioManager.instance?.PlaySfx(AudioManager.instance.boom);
            if(result) player.particle2.gameObject.SetActive(true);
            else player.particle.gameObject.SetActive(true);
            DOVirtual.DelayedCall(1f, () =>
            {
                UIController.instance.ShowGameOver(result);
            });
        }
    }
}