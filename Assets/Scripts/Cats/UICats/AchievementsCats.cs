using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 Liste dans GameManager qui crée tout les bools correspondant au bon chat dans la liste des animations (AnimationControl).
 */

namespace Chats
{
    public class AchievementsCats : MonoBehaviour
    {
        public List<GameObject> catsUnlocked;
        public GameObject AllCatsCarnet;
        public void OnEnable()
        {
            for (var i = 0; i < GameManager.Instance.CatsUnlockedBool.Count; i++)
            {
                if (GameManager.Instance.CatsUnlockedBool[i])
                {
                    try
                    {
                        catsUnlocked[i].GetComponent<Image>().sprite = AllCatsCarnet.GetComponent<AllCatsCarnet>().portraitsCats[i];
                    }
                    catch
                    {
                        Debug.Log("Déjà découvert");
                    }
                }
            }
        }
    }
}
