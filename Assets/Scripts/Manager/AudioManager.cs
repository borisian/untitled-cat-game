using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chats
{

    public class AudioManager : MonoBehaviour
    {
        private AudioSource AudioGm;

        public void Start()
        {
            AudioGm = GameManager.Instance.GetComponent<AudioSource>();
            // AudioListener.volume = 1f;
        }

        public void OnOff()
        {
            if (GameManager.Instance.AudioOnOff)
            {
                AudioListener.volume = 0f;
                GameManager.Instance.AudioOnOff = false;
            }
            else
            {
                AudioListener.volume = 1f;
                GameManager.Instance.AudioOnOff = true;
            }

        }

        public void Go()
        {
            AudioGm.clip = GameManager.Instance.GoButtonSound;
            AudioGm.loop = false;
            AudioGm.Play();
        }

        public void StartSoundFunc()
        {
            AudioGm.clip = GameManager.Instance.StartSound;
            AudioGm.loop = false;
            AudioGm.Play();
        }
    }
}
