using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Chats
{

    public class UIScriptMaison : MonoBehaviour
    {
        public GameObject ButtonGoRue;

        void Update()
        {
            if (GameManager.Instance.GameInMaison && GameManager.Instance.GameInAction)
            {
                ButtonGoRue.GetComponent<Button>().interactable = false;
            }
            else
            {
                ButtonGoRue.GetComponent<Button>().interactable = true;
            }
        }
    }
}
