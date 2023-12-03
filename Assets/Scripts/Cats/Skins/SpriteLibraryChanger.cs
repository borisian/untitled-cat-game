using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chats
{
    public class SpriteLibraryChanger : MonoBehaviour
    {
        [Header("Predef. Skins")]
        public GameObject[] Skins;
        //
        private int index;

        [Header("Skin Saved")]
        [SerializeField]
        private GameObject skin;
        [SerializeField]
        public string skinName;
        //

        [Header("Sprite Label Name Saved")]
        public string body_label;
        public string oreille1_label;
        public string head_label;
        public string queue_label;
        //

        public void Start()
        {
            if (GameManager.Instance.GameInRue)
            {
                index = Random.Range(0, Skins.Length);
                skin = Skins[index];
                Instantiate(skin, gameObject.transform);
                skinName = skin.name;
                //skin.name = skinName;
            }
            if (GameManager.Instance.GameInMaison)
            {
                for (int i = 0; i < Skins.Length; i++)
                {
                    if (skinName == Skins[i].name)
                    {
                        Instantiate(Skins[i], gameObject.transform);
                    }
                }
            }
        }
    }
}
