using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chats
{
    public class StopAction : MonoBehaviour
    {
        public GameObject ThisCat;
        public GameObject RueUI;
        public GameObject BtnAdopt;
        public GameObject FeedUI;
        public GameObject CatsName;
        public GameObject StopBtn;

        public void Update()
        {
            if (CatsName.activeSelf)
            {
                StopBtn.SetActive(false);
            }
        }

        public void StopTheAction()
        {
            if (BtnAdopt.activeSelf)
            {
                RueUI.SetActive(false);
            }
            if (FeedUI.activeSelf)
            {
                FeedUI.SetActive(false);
                BtnAdopt.SetActive(true);
                RueUI.SetActive(false);
            }
            GameManager.Instance.GameInAction = false;
            ThisCat.GetComponent<AgentScript>().isInAction = false;
            ThisCat.GetComponent<AgentScript>().isWandering = false;
        }
    }
}
