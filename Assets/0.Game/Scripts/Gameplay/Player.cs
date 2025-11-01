using System;
using System.Collections.Generic;
using _0.Common.Scripts;
using UnityEngine;

namespace _0.Game.Scripts.Gameplay
{
    public class Player : MonoBehaviour
    {
        public List<GameObject> shapeObject;
        public GameController.ShapeType shape;
        public GameObject particle;
        public GameObject particle2;
        public void ChangeShape(GameController.ShapeType shape)
        {
            for (int i = 0; i < shapeObject.Count; i++)
            {
                var obj = shapeObject[i];
                obj.gameObject.SetActive(i == (int)shape);
            }
            this.shape = shape;
            AudioManager.instance?.PlaySfx(AudioManager.instance.pop);
        }

        
    }
}