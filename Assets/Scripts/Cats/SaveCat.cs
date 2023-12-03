using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chats
{
    public class SaveCat : MonoBehaviour
    {
        data.CatData catData;
        public Cat thisCat;
        public AnimationControl catSkin;

        public void Start()
        {
            thisCat = gameObject.GetComponent<Cat>();
            catSkin = gameObject.GetComponent<AnimationControl>();
        }

        public void Save()
        {
            catData = new data.CatData(thisCat.Name, thisCat.Age, catSkin.nameAnimatorController);

            Debug.Log("Save : " + catData.catName);
            Debug.Log("AllCats : " + GameManager.Instance.AllCats);

            GameManager.Instance.AllCats.Add(catData);
            GameManager.Instance.SaveChats();
        }
    }
}
