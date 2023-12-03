using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chats
{

    public class Spawner : MonoBehaviour
    {
        public GameObject PrefabCat;
        public GameObject LeftArea;
        public GameObject RightArea;

        private int nbArea;

        private bool CanSpawn = true;

        public void OnEnable()
        {
            if (GameManager.Instance.GameInRue)
            {
                for (int i = 0; i < Random.Range(1, 4); i++)
                {
                    Vector2 pos = new Vector2(Random.Range(-2.5f, 3), Random.Range(-2.5f, -4));
                    // Add starter cats
                    GameObject cat = Instantiate(PrefabCat, pos, transform.rotation);
                    // Add 1 cat in the GameManager script
                    GameManager.Instance.NbCats += 1;
                    /*
                     GetComponent Random Description
                    */
                    cat.GetComponent<Cat>().Age = Random.Range(1, 12);
                }
            }
            if (GameManager.Instance.GameInMaison)
            {
                SpawnInMaison();
            }
        }

        public void Update()
        {
            if (GameManager.Instance.GameStarted && CanSpawn && GameManager.Instance.GameInRue)
            {
                StartCoroutine(SpawnInRue());
            }
        }

        public void SpawnInMaison()
        {
            for (int i = 1; i < GameManager.Instance.AllCats.Count; i++)
            {
                float screenX, screenY;
                Vector2 pos;
                screenX = Random.Range(-29, 28);
                screenY = Random.Range(-4.8f, -2);
                pos = new Vector2(screenX, screenY);
                GameObject cat = Instantiate(PrefabCat, pos, transform.rotation);
                // Set values
                // Cat Description
                cat.GetComponent<Cat>().Name = GameManager.Instance.AllCats[i].catName;
                cat.GetComponent<Cat>().Age = GameManager.Instance.AllCats[i].age;
                cat.GetComponent<Cat>().Adopted = true;
                // Cat Skins
                cat.GetComponent<AnimationControl>().nameAnimatorController = GameManager.Instance.AllCats[i].nameAnimatorController;

                /*cat.GetComponent<SpriteLibraryChanger>().skinName = GameManager.Instance.AllCats[i].skinName;
                cat.GetComponent<SpriteLibraryChanger>().body_label = GameManager.Instance.AllCats[i].body;
                cat.GetComponent<SpriteLibraryChanger>().oreille1_label = GameManager.Instance.AllCats[i].oreille;
                cat.GetComponent<SpriteLibraryChanger>().head_label = GameManager.Instance.AllCats[i].head;
                cat.GetComponent<SpriteLibraryChanger>().queue_label = GameManager.Instance.AllCats[i].queue;*/
                //
            }
        }

        IEnumerator SpawnInRue()
        {
            CanSpawn = false;
            // MeshCollider c1 = LeftArea.GetComponent<MeshCollider>();
            // MeshCollider c2 = RightArea.GetComponent<MeshCollider>();
            nbArea = Random.Range(1, 3);

            if (nbArea == 1)
            {
                float screenX1, screenY1;
                Vector2 pos1;

                // screenX1 = Random.Range(c1.bounds.min.x, c1.bounds.max.x);
                // screenY1 = Random.Range(c1.bounds.min.y, c1.bounds.max.y);
                screenX1 = Random.Range(-44, -40);
                screenY1 = Random.Range(-5, -2);
                pos1 = new Vector2(screenX1, screenY1);

                GameObject cat = Instantiate(PrefabCat, pos1, transform.rotation);
                // Add 1 cat in the GameManager script
                GameManager.Instance.NbCats += 1;
                /*
                 GetComponent Random Description and Name
                */
                cat.GetComponent<Cat>().Age = Random.Range(1, 12);
                Debug.Log("SPAWN : " + cat);
            }
            else
            {
                float screenX2, screenY2;
                Vector2 pos2;

                // screenX2 = Random.Range(c2.bounds.min.x, c2.bounds.max.x);
                // screenY2 = Random.Range(c2.bounds.min.y, c2.bounds.max.y);
                screenX2 = Random.Range(44, 40);
                screenY2 = Random.Range(-5, -2);
                pos2 = new Vector2(screenX2, screenY2);

                GameObject cat = Instantiate(PrefabCat, pos2, transform.rotation);
                // Add 1 cat in the GameManager script
                GameManager.Instance.NbCats += 1;
                /*
                 GetComponent Random Description and Name
                */
                cat.GetComponent<Cat>().Age = Random.Range(1, 12);
                Debug.Log("SPAWN : " + cat);
            }
            yield return new WaitForSeconds(Random.Range(8, 20));
            CanSpawn = true;
        }
    }
}
