using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Chats
{
    public class Cooldown : MonoBehaviour
    {
        [SerializeField]
        private Image imageCooldown;

        public bool isCD = false;
        private float CDTime = 120f;
        private float CDTimer = 0f;

        private void Start()
        {
            imageCooldown.fillAmount = 1.0f;
        }

        private void Update()
        {
            if (isCD)
            {
                ApplyCD();
            }
        }

        private void ApplyCD()
        {
            CDTimer += Time.deltaTime;
            if (CDTimer > 120.0f)
            {
                isCD = false;
                imageCooldown.fillAmount = 1.0f;
            }
            else
            {
                imageCooldown.fillAmount = CDTimer / CDTime;
            }
        }
    }
}
