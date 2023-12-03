using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Chats
{

    public class AnimationControl : MonoBehaviour
    {
        private GameObject ThisCat;
        Animator animator;

        [Header("All Skins/Animations")]
        public AnimatorOverrideController[] ListAnimatorController;
        public string nameAnimatorController;
        [Header("Portrait")]
        public Image[] portraits;
        public Image portrait;

        public void Start()
        {
            ThisCat = gameObject;
            animator = GetComponent<Animator>();
            if (GameManager.Instance.GameInMaison)
            {
                GetSkin();
            }
            else
            {
                GetRandomSkin();
            }
        }

        public void Update()
        {
            if (ThisCat.GetComponent<AgentScript>().isMoving)
            {
                animator.SetBool("Move", true);
            }
            else
            {
                animator.SetBool("Move", false);
            }

            // Flip
            if (ThisCat.GetComponent<AgentScript>().flip)
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().flipX = false;
            }
        }

        public void GetRandomSkin()
        {
            int rand = Random.Range(0, 101);
            if (rand >= 0 && rand < 60)
            {
                // 60% prob.
                Debug.Log("60% prob.");
                int num = Random.Range(0, 10);
                switch (num)
                {
                    case 0:
                        GetComponent<Animator>().runtimeAnimatorController = ListAnimatorController[16];
                        nameAnimatorController = ListAnimatorController[16].name;
                        Debug.Log("Random Skin : " + ListAnimatorController[16].name);
                        break;
                    case 1:
                        GetComponent<Animator>().runtimeAnimatorController = ListAnimatorController[2];
                        nameAnimatorController = ListAnimatorController[2].name;
                        Debug.Log("Random Skin : " + ListAnimatorController[2].name);
                        break;
                    case 2:
                        GetComponent<Animator>().runtimeAnimatorController = ListAnimatorController[3];
                        nameAnimatorController = ListAnimatorController[3].name;
                        Debug.Log("Random Skin : " + ListAnimatorController[3].name);
                        break;
                    case 3:
                        GetComponent<Animator>().runtimeAnimatorController = ListAnimatorController[18];
                        nameAnimatorController = ListAnimatorController[18].name;
                        Debug.Log("Random Skin : " + ListAnimatorController[18].name);
                        break;
                    case 4:
                        GetComponent<Animator>().runtimeAnimatorController = ListAnimatorController[17];
                        nameAnimatorController = ListAnimatorController[17].name;
                        Debug.Log("Random Skin : " + ListAnimatorController[17].name);
                        break;
                    case 5:
                        GetComponent<Animator>().runtimeAnimatorController = ListAnimatorController[26];
                        nameAnimatorController = ListAnimatorController[26].name;
                        Debug.Log("Random Skin : " + ListAnimatorController[26].name);
                        break;
                    case 6:
                        GetComponent<Animator>().runtimeAnimatorController = ListAnimatorController[30];
                        nameAnimatorController = ListAnimatorController[30].name;
                        Debug.Log("Random Skin : " + ListAnimatorController[30].name);
                        break;
                    case 7:
                        GetComponent<Animator>().runtimeAnimatorController = ListAnimatorController[12];
                        nameAnimatorController = ListAnimatorController[12].name;
                        Debug.Log("Random Skin : " + ListAnimatorController[12].name);
                        break;
                    case 8:
                        GetComponent<Animator>().runtimeAnimatorController = ListAnimatorController[9];
                        nameAnimatorController = ListAnimatorController[9].name;
                        Debug.Log("Random Skin : " + ListAnimatorController[9].name);
                        break;
                    case 9:
                        GetComponent<Animator>().runtimeAnimatorController = ListAnimatorController[5];
                        nameAnimatorController = ListAnimatorController[5].name;
                        Debug.Log("Random Skin : " + ListAnimatorController[5].name);
                        break;
                }
                // end 60% prob.
            }
            else if (rand >= 60 && rand < 90)
            {
                Debug.Log("40% prob.");
                int num = Random.Range(0, 8);
                switch (num)
                {
                    case 0:
                        GetComponent<Animator>().runtimeAnimatorController = ListAnimatorController[0];
                        nameAnimatorController = ListAnimatorController[0].name;
                        Debug.Log("Random Skin : " + ListAnimatorController[0].name);
                        break;
                    case 1:
                        GetComponent<Animator>().runtimeAnimatorController = ListAnimatorController[1];
                        nameAnimatorController = ListAnimatorController[1].name;
                        Debug.Log("Random Skin : " + ListAnimatorController[1].name);
                        break;
                    case 2:
                        GetComponent<Animator>().runtimeAnimatorController = ListAnimatorController[21];
                        nameAnimatorController = ListAnimatorController[21].name;
                        Debug.Log("Random Skin : " + ListAnimatorController[21].name);
                        break;
                    case 3:
                        GetComponent<Animator>().runtimeAnimatorController = ListAnimatorController[23];
                        nameAnimatorController = ListAnimatorController[23].name;
                        Debug.Log("Random Skin : " + ListAnimatorController[23].name);
                        break;
                    case 4:
                        GetComponent<Animator>().runtimeAnimatorController = ListAnimatorController[8];
                        nameAnimatorController = ListAnimatorController[8].name;
                        Debug.Log("Random Skin : " + ListAnimatorController[8].name);
                        break;
                    case 5:
                        GetComponent<Animator>().runtimeAnimatorController = ListAnimatorController[6];
                        nameAnimatorController = ListAnimatorController[6].name;
                        Debug.Log("Random Skin : " + ListAnimatorController[6].name);
                        break;
                    case 6:
                        GetComponent<Animator>().runtimeAnimatorController = ListAnimatorController[27];
                        nameAnimatorController = ListAnimatorController[27].name;
                        Debug.Log("Random Skin : " + ListAnimatorController[27].name);
                        break;
                    case 7:
                        GetComponent<Animator>().runtimeAnimatorController = ListAnimatorController[19];
                        nameAnimatorController = ListAnimatorController[19].name;
                        Debug.Log("Random Skin : " + ListAnimatorController[19].name);
                        break;
                }
            }
            else if (rand >= 90 && rand <= 99)
            {
                Debug.Log("9% prob.");
                int num = Random.Range(0, 6);
                switch (num)
                {
                    case 0:
                        GetComponent<Animator>().runtimeAnimatorController = ListAnimatorController[15];
                        nameAnimatorController = ListAnimatorController[15].name;
                        Debug.Log("Random Skin : " + ListAnimatorController[15].name);
                        break;
                    case 1:
                        GetComponent<Animator>().runtimeAnimatorController = ListAnimatorController[7];
                        nameAnimatorController = ListAnimatorController[7].name;
                        Debug.Log("Random Skin : " + ListAnimatorController[7].name);
                        break;
                    case 2:
                        GetComponent<Animator>().runtimeAnimatorController = ListAnimatorController[20];
                        nameAnimatorController = ListAnimatorController[20].name;
                        Debug.Log("Random Skin : " + ListAnimatorController[20].name);
                        break;
                    case 3:
                        GetComponent<Animator>().runtimeAnimatorController = ListAnimatorController[29];
                        nameAnimatorController = ListAnimatorController[29].name;
                        Debug.Log("Random Skin : " + ListAnimatorController[29].name);
                        break;
                    case 4:
                        GetComponent<Animator>().runtimeAnimatorController = ListAnimatorController[24];
                        nameAnimatorController = ListAnimatorController[24].name;
                        Debug.Log("Random Skin : " + ListAnimatorController[24].name);
                        break;
                    case 5:
                        GetComponent<Animator>().runtimeAnimatorController = ListAnimatorController[4];
                        nameAnimatorController = ListAnimatorController[4].name;
                        Debug.Log("Random Skin : " + ListAnimatorController[4].name);
                        break;
                }
            }
            else if (rand == 100)
            {
                Debug.Log("1% prob.");
                int num = Random.Range(0, 3);
                switch (num)
                {
                    case 0:
                        GetComponent<Animator>().runtimeAnimatorController = ListAnimatorController[13];
                        nameAnimatorController = ListAnimatorController[13].name;
                        Debug.Log("Random Skin : " + ListAnimatorController[13].name);
                        break;
                    case 1:
                        GetComponent<Animator>().runtimeAnimatorController = ListAnimatorController[14];
                        nameAnimatorController = ListAnimatorController[14].name;
                        Debug.Log("Random Skin : " + ListAnimatorController[14].name);
                        break;
                    case 2:
                        GetComponent<Animator>().runtimeAnimatorController = ListAnimatorController[25];
                        nameAnimatorController = ListAnimatorController[25].name;
                        Debug.Log("Random Skin : " + ListAnimatorController[25].name);
                        break;
                }
            }
        }

        public void GetSkin()
        {
            for (var i = 0; i < ListAnimatorController.Length; i++)
            {
                if (ListAnimatorController[i].name == nameAnimatorController)
                {
                    GetComponent<Animator>().runtimeAnimatorController = ListAnimatorController[i];
                }
            }
        }
    }
}
