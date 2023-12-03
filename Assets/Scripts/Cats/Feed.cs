using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;
using TMPro;

namespace Chats
{
    public class Feed : MonoBehaviour
    {
        string LikedFoodCat;
        public GameObject ThisCat;
        private bool CanAdopt = true;
        [Header("Bouton du jeu d'adoption")]
        public GameObject milkBtn;
        public GameObject catfoodBtn;
        public GameObject fishBtn;
        [Header("UI Elements")]
        public GameObject RueUI;
        public GameObject FeedUI;
        public GameObject CatsName;
        public TextMeshProUGUI NameTextArea;
        [Header("Audio")]
        public AudioClip[] meowSound;
        public AudioClip Error;
        public AudioClip Win;

        public void Start()
        {
            LikedFoodCat = ThisCat.GetComponent<Cat>().LikedFood;
        }

        public void Update()
        {
            if (GameManager.Instance.AllCats.Count >= 32)
            {
                CanAdopt = false;
            }
        }

        public void ActiveFeed()
        {
            if (GameManager.Instance.Money > 0 && CanAdopt)
            {
                FeedUI.SetActive(true);
            }
            else
            {
                // Erreur limite !
                Debug.Log("Tu n'as pas assez de monnaie ou la limite de chat adopté a été atteinte");
                if (GameManager.Instance.AllCats.Count >= 32)
                {
                    StartCoroutine(GameManager.Instance.ErrorCoroutine(GameManager.Instance.ErrorCatLimit));
                }
                if (GameManager.Instance.Money <= 0)
                {
                    StartCoroutine(GameManager.Instance.ErrorCoroutine(GameManager.Instance.ErrorFood));
                }
                ThisCat.GetComponent<AgentScript>().isInAction = false;
                GameManager.Instance.GameInAction = false;
            }
        }

        public void FeedFunction()
        {
            if (GameManager.Instance.GameInRue)
            {
                if (GameManager.Instance.Money > 0)
                {
                    if (LikedFoodCat == EventSystem.current.currentSelectedGameObject.name)
                    {
                        // Réussi, adopté
                        if (!ThisCat.GetComponent<Cat>().Adopted)
                        {
                            // Audio
                            ThisCat.GetComponent<AudioSource>().clip = Win;
                            ThisCat.GetComponent<AudioSource>().Play();
                            //
                            Debug.Log("ACTION : CAT ADOPTED");
                            // Lose money
                            if (GameManager.Instance.Money > 0)
                            {
                                GameManager.Instance.LoseMoney(1);
                            }
                            ThisCat.GetComponent<Cat>().Adopted = true;
                            // Desactive les bons UI
                            FeedUI.SetActive(false);
                            GameManager.Instance.PanelName.SetActive(true);
                            GameManager.Instance.ThisCat = ThisCat;

                            // dans la liste de tout les skins, si le nom de l'anim (skin) de ce chat est égal à celui d'un de la liste, on check le bool équivalent (nécessite une symétrie entre les listes).
                            for (var i = 0; i < ThisCat.GetComponent<AnimationControl>().ListAnimatorController.Length; i++)
                            {
                                Debug.Log("Get portrait of cat unlocked");

                                if (ThisCat.GetComponent<AnimationControl>().nameAnimatorController == ThisCat.GetComponent<AnimationControl>().ListAnimatorController[i].name)
                                {
                                    GameManager.Instance.CatPortraitGiveNamePanel.sprite = GameManager.Instance.ObjectMyCatsForPortrait.GetComponent<AllCatsCarnet>().portraitsCats[i];
                                }
                            }
                        }
                    }
                    else
                    {
                        ThisCat.GetComponent<AudioSource>().clip = Error;
                        ThisCat.GetComponent<AudioSource>().Play();
                        // Check quel btn a été appuyé et donc lequel mettre une animation d'erreur dessus
                        Debug.Log("ACTION : WRONG FOOD CHOICE");
                        if (GameManager.Instance.Money > 0)
                        {
                            GameManager.Instance.LoseMoney(1);
                        }
                        switch (EventSystem.current.currentSelectedGameObject.name)
                        {
                            case "milk":
                                StartCoroutine(AnimBtnMilk());
                                break;
                            case "fish":
                                StartCoroutine(AnimBtnFish());
                                break;
                            case "catfood":
                                StartCoroutine(AnimBtnCatFood());
                                break;
                        }
                    }
                }
                else
                {
                    Debug.Log("Don't have enough money");
                }
            }
        }

        // Anim des btns en cas d'erreurs
        IEnumerator AnimBtnMilk()
        {
            Color clr = milkBtn.GetComponent<Image>().color;
            milkBtn.GetComponent<Image>().color = Color.red;
            yield return new WaitForSeconds(0.5f);
            milkBtn.GetComponent<Image>().color = clr;
        }

        IEnumerator AnimBtnFish()
        {
            Color clr = fishBtn.GetComponent<Image>().color;
            fishBtn.GetComponent<Image>().color = Color.red;
            yield return new WaitForSeconds(0.5f);
            fishBtn.GetComponent<Image>().color = clr;
        }

        IEnumerator AnimBtnCatFood()
        {
            Color clr = catfoodBtn.GetComponent<Image>().color;
            catfoodBtn.GetComponent<Image>().color = Color.red;
            yield return new WaitForSeconds(0.5f);
            catfoodBtn.GetComponent<Image>().color = clr;
        }
    }
}
