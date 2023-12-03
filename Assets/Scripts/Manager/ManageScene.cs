using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Chats
{
    public class ManageScene : MonoBehaviour
    {
        [Header("Transition")]
        public GameObject Ruetransition;
        public GameObject RueEndTransition;
        
        public GameObject MaisonTransition;
        public GameObject MaisonEndTransition;

        public void Start()
        {
            if (GameManager.Instance.GameInMaison)
            {
                StartCoroutine(StartMaison());
            }
            if (GameManager.Instance.GameInRue)
            {
                StartCoroutine(StartRue());
            }
        }

        public void SceneRue()
        {
            StartCoroutine(LoadRue());
        }

        public void funcStateRue()
        {
            SceneManager.LoadScene("Rue", LoadSceneMode.Single);
            GameManager.Instance.GameInMaison = false;
            GameManager.Instance.GameInRue = true;
        }

        public void SceneMaison()
        {
            StartCoroutine(LoadMaison());

        }

        public void funcStateMaison()
        {
            SceneManager.LoadScene("Maison", LoadSceneMode.Single);
            GameManager.Instance.GameInMaison = true;
            GameManager.Instance.GameInRue = false;
        }

        IEnumerator LoadRue()
        {
            MaisonEndTransition.SetActive(true);

            yield return new WaitForSeconds(1);

            funcStateRue();
        }

        IEnumerator LoadMaison()
        {
            RueEndTransition.SetActive(true);

            yield return new WaitForSeconds(1);

            funcStateMaison();
        }

        IEnumerator StartMaison()
        {
            MaisonTransition.SetActive(true);
            yield return new WaitForSeconds(1);
            MaisonTransition.SetActive(false);
        }

        IEnumerator StartRue()
        {
            Ruetransition.SetActive(true);
            yield return new WaitForSeconds(1);
            Ruetransition.SetActive(false);
        }

    }
}
