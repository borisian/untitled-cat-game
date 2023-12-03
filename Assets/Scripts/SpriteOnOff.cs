using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Chats
{

    public class SpriteOnOff : MonoBehaviour
    {
        public Sprite OnSprite;
        public Sprite OffSprite;

        public void Awake()
        {
            if (GameManager.Instance.AudioOnOff)
            {
                GetComponent<Image>().sprite = OnSprite;
            }
            else
            {
                GetComponent<Image>().sprite = OffSprite;
            }
        }

        public void ChangeOnOffSprite()
        {
            if (GameManager.Instance.AudioOnOff)
            {
                GetComponent<Image>().sprite = OnSprite;
            }
            else
            {
                GetComponent<Image>().sprite = OffSprite;
            }
        }

        /*public void ChangeSpriteOnOff()
        {
            GameObject AudioManager = GameObject.FindGameObjectWithTag("manager");
            if (AudioManager.GetComponent<AudioManager>().SoundOn)
            {
                GetComponent<Image>().sprite = OnSprite;
            }
            else
            {
                GetComponent<Image>().sprite = OffSprite;
            }
        }*/
    }
}
