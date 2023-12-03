using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Unity.Collections;
using UnityEngine.U2D;
using UnityEngine.U2D.Animation;

namespace Chats
{

    public class Swapper : MonoBehaviour
    {
        #region Inspector

        [SerializeField]
        private SpriteLibrary spriteLibrary = default;

        [SerializeField]
        private SpriteResolver targetResolver = default;

        [SerializeField]
        private string targetCategory = default;

        [SerializeField]
        private string label = default;

        public SpriteLibraryChanger thisCat;

        #endregion

        #region Properties

        private SpriteLibraryAsset LibraryAsset => spriteLibrary.spriteLibraryAsset;

        #endregion

        public void Start()
        {
            // Get the cat parent of this script
            thisCat = gameObject.transform.parent.GetComponent<SpriteLibraryChanger>();
            // Chose a random label/sprite for an element 
            if (GameManager.Instance.GameInRue)
            {
                SelectRandom();
               
            }
            // save or get(instantiated) this random choice
            SaveLabel();
        }

        #region Methods

        public void SelectRandom()
        {
            string[] labels =
              LibraryAsset.GetCategoryLabelNames(targetCategory).ToArray();
            int index = Random.Range(0, labels.Length);
            label = labels[index];

            targetResolver.SetCategoryAndLabel(targetCategory, label);
        }

        public void SaveLabel()
        {
            if (GameManager.Instance.GameInRue)
            {
                switch (targetCategory)
                {
                    case "Body":
                        thisCat.body_label = label;
                        break;
                    case "Oreille":
                        thisCat.oreille1_label = label;
                        break;
                    case "Head":
                        thisCat.head_label = label;
                        break;
                    case "Queue":
                        thisCat.queue_label = label;
                        break;
                }
            }
            if (GameManager.Instance.GameInMaison)
            {
                switch (targetCategory)
                {
                    case "Body":
                        label = thisCat.body_label;
                        break;
                    case "Oreille":
                        label = thisCat.oreille1_label;
                        break;
                    case "Head":
                        label = thisCat.head_label;
                        break;
                    case "Queue":
                        label = thisCat.queue_label;
                        break;
                }
                targetResolver.SetCategoryAndLabel(targetCategory, label);
            }
        }
        #endregion
    }
}
