using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Chats
{

    public class AllCatsCarnet : MonoBehaviour
    {

        public Sprite[] portraitsCats;
        public GameObject ContentMyCats;
        public GameObject ElementMyCats;

        public void OnEnable()
        {
            InstantiateCatElementUI();
        }

        public void InstantiateCatElementUI()
        {
            // Reset List
            foreach (Transform child in ContentMyCats.transform)
            {
                Destroy(child.gameObject);
            }

            for (var i = 1; i < GameManager.Instance.AllCats.Count; i++)
            {
                // Portrait for my cats
                GameObject el = Instantiate(ElementMyCats, ContentMyCats.transform);
                el.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = GameManager.Instance.AllCats[i].catName;
                switch (GameManager.Instance.AllCats[i].nameAnimatorController)
                {
                    case "Abyssin":
                        el.transform.GetChild(1).GetComponent<Image>().sprite = portraitsCats[0];
                        break;
                    case "AmericanCurl":
                        el.transform.GetChild(1).GetComponent<Image>().sprite = portraitsCats[1];
                        break;
                    case "Balinese":
                        el.transform.GetChild(1).GetComponent<Image>().sprite = portraitsCats[2];
                        break;
                    case "Batard":
                        el.transform.GetChild(1).GetComponent<Image>().sprite = portraitsCats[3];
                        break;
                    case "Birman":
                        el.transform.GetChild(1).GetComponent<Image>().sprite = portraitsCats[4];
                        break;
                    case "Bombay":
                        el.transform.GetChild(1).GetComponent<Image>().sprite = portraitsCats[5];
                        break;
                    case "Burmese":
                        el.transform.GetChild(1).GetComponent<Image>().sprite = portraitsCats[6];
                        break;
                    case "Chartreux":
                        el.transform.GetChild(1).GetComponent<Image>().sprite = portraitsCats[7];
                        break;
                    case "japonese":
                        el.transform.GetChild(1).GetComponent<Image>().sprite = portraitsCats[8];
                        break;
                    case "Khao":
                        el.transform.GetChild(1).GetComponent<Image>().sprite = portraitsCats[9];
                        break;
                    case "Angora":
                        el.transform.GetChild(1).GetComponent<Image>().sprite = portraitsCats[10];
                        break;
                    case "Blue":
                        el.transform.GetChild(1).GetComponent<Image>().sprite = portraitsCats[11];
                        break;
                    case "Kouli":
                        el.transform.GetChild(1).GetComponent<Image>().sprite = portraitsCats[12];
                        break;
                    case "Lykoi":
                        el.transform.GetChild(1).GetComponent<Image>().sprite = portraitsCats[13];
                        break;
                    case "MaineCoon":
                        el.transform.GetChild(1).GetComponent<Image>().sprite = portraitsCats[14];
                        break;
                    case "Manx":
                        el.transform.GetChild(1).GetComponent<Image>().sprite = portraitsCats[15];
                        break;
                    case "Minuet":
                        el.transform.GetChild(1).GetComponent<Image>().sprite = portraitsCats[16];
                        break;
                    case "NoirBlanc":
                        el.transform.GetChild(1).GetComponent<Image>().sprite = portraitsCats[29];
                        break;
                    case "Norvegian":
                        el.transform.GetChild(1).GetComponent<Image>().sprite = portraitsCats[17];
                        break;
                    case "Occidental":
                        el.transform.GetChild(1).GetComponent<Image>().sprite = portraitsCats[18];
                        break;
                    case "Oriental":
                        el.transform.GetChild(1).GetComponent<Image>().sprite = portraitsCats[19];
                        break;
                    case "Persian":
                        el.transform.GetChild(1).GetComponent<Image>().sprite = portraitsCats[20];
                        break;
                    case "Ragdoll":
                        el.transform.GetChild(1).GetComponent<Image>().sprite = portraitsCats[21];
                        break;
                    case "Russian":
                        el.transform.GetChild(1).GetComponent<Image>().sprite = portraitsCats[22];
                        break;
                    case "ScottishFold":
                        el.transform.GetChild(1).GetComponent<Image>().sprite = portraitsCats[23];
                        break;
                    case "Siamois":
                        el.transform.GetChild(1).GetComponent<Image>().sprite = portraitsCats[24];
                        break;
                    case "Siberian":
                        el.transform.GetChild(1).GetComponent<Image>().sprite = portraitsCats[25];
                        break;
                    case "Sokoke":
                        el.transform.GetChild(1).GetComponent<Image>().sprite = portraitsCats[26];
                        break;
                    case "Sphynx":
                        el.transform.GetChild(1).GetComponent<Image>().sprite = portraitsCats[27];
                        break;
                    case "Turkish":
                        el.transform.GetChild(1).GetComponent<Image>().sprite = portraitsCats[28];
                        break;
                }
            }
        }
    }
}
