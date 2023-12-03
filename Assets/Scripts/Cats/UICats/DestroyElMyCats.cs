using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Chats
{
    public class DestroyElMyCats : MonoBehaviour
    {
        public GameObject catname;
        public GameObject[] cats;
        /*public void DeleteThisCats()
        {
            foreach (var cat in GameManager.Instance.AllCats.ToArray())
            {
                if (cat.catName == catname.GetComponent<TextMeshProUGUI>().text)
                {
                    if (GameManager.Instance.GameInMaison)
                    {
                        cats = GameObject.FindGameObjectsWithTag("cat");
                        foreach (var c in cats)
                        {
                            if (c.GetComponent<Cat>().Name == cat.catName)
                            {
                                Destroy(c);
                            }
                        }
                    }
                    GameManager.Instance.AllCats.Remove(cat);
                    GameManager.Instance.LoseCat(1);
                    GameManager.Instance.SaveChats();
                    Destroy(gameObject);
                }
            }
        }*/

        public void GetNameOfCat()
        {
            GameManager.Instance.CatToDelete = catname;
            GameManager.Instance.PopUpDelete.SetActive(true);
        }
    }
}
