using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chats
{
    public class Cat : MonoBehaviour
    {
        [Header("Description")]
        public string Name;
        public string Description;
        public string LikedFood;
        public int Age;
        public bool Adopted;
        public float timeLived;

        private float initTime;

        [Header("FX")]
        public GameObject Coeur;

        public void Start()
        {
            // Attribue sa nourriture préférée 
            LikedFood = WhichFood();
            initTime = Time.timeSinceLevelLoad;
        }

        public void Update()
        {
            // Temps de vie du chat en secondes
            timeLived = Time.timeSinceLevelLoad - initTime;
        }

        private string WhichFood()
        {
            List<string> foods = new List<string>();
            foods.Add("milk");
            foods.Add("catfood");
            foods.Add("fish");
            string value = foods[Random.Range(0, foods.Count)];
            return value;
        }


        // COLLISION
        private void OnTriggerEnter2D(Collider2D col)
        {
            // CHECK PASSING
            if (col.gameObject.tag == "utilzone")
            {
                if (GameManager.Instance.GameInRue)
                {
                    if (!Adopted)
                    {
                        if (timeLived > 30)
                        {
                            Destroy(gameObject);
                            GameManager.Instance.NbCats -= 1;
                        }
                    }
                }
            }
        }
    }
}
