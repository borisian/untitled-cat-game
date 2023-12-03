using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Chats
{
    public class UIScript : MonoBehaviour
    {
        public GameObject ButtonStartGame;
        public GameObject ButtonGoMaison;

        public void Update()
        {
           if (GameManager.Instance.GameInRue && !GameManager.Instance.GameStarted || GameManager.Instance.GameInRue && GameManager.Instance.GameInAction)
            {
                ButtonGoMaison.GetComponent<Button>().interactable = false;
            }
           else
            {
                ButtonGoMaison.GetComponent<Button>().interactable = true;
            }

           
        }

        public void StartGame()
        {
            ButtonStartGame.SetActive(false);
            Debug.Log("Game Started");
            GameManager.Instance.GameStarted = true;
        }

        public void Start()
        {
            if (ButtonStartGame.activeSelf && GameManager.Instance.GameStarted)
            {
                ButtonStartGame.SetActive(false);
            }
        }
    }
}
