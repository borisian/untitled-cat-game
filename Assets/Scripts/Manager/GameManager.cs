using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

/*
 Gestion des PlayerPrefs/JSON (Saves), States et les Chats appr
 */

namespace Chats
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager instance = null;
        public static GameManager Instance => instance;

        [Header("Liste des chats adopt�s")]
        public List<data.CatData> AllCats = new List<data.CatData>();
        public List<bool> CatsUnlockedBool = new List<bool>();

        [Header("States")]
        public bool GameInMaison;
        public bool GameInRue;
        public bool GameStarted;
        public bool GameInAction;
        public bool CanTouch;
        public bool CanPlayFishing;

        [Header("Values")]
        public int FirstTime;
        public int TutoUp;
        public int Money;
        public int NbCats;
        public int NbCatsAdopted;

        [Header("UI")]
        public TextMeshProUGUI textNbCatsAdopted;
        public TextMeshProUGUI textMoney;
        public TextMeshProUGUI NameTextArea;
        public Image CatPortraitGiveNamePanel;
        public GameObject PanelName;
        public GameObject Tutorial;
        public GameObject InvalidName;
        public GameObject ErrorFood;
        public GameObject ErrorCatLimit;
        public GameObject PopUpDelete;

        [Header("FX")]
        public GameObject Vanish;

        [Header("For Adopt/Name")]
        public GameObject ThisCat;
        public GameObject ObjectMyCatsForPortrait;
        public GameObject ObjectCarnetAchievements;
        public GameObject CatToDelete;

        [Header("Audio")]
        public AudioClip fishing;
        public AudioClip Win;
        public AudioClip Error;
        public AudioClip GoButtonSound;
        public AudioClip OpenCarnetSound;
        public AudioClip TurnPage;
        public AudioClip StartSound;
        public AudioClip CloseCarnet;
        public bool AudioOnOff;

        #region Start
        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this.gameObject);
                return;
            }
            else
            {
                instance = this;
            }
            DontDestroyOnLoad(this.gameObject);
            Init();
        }

        public void FirstTimeFunc()
        {
            for (var i = 0; i < 32; i++)
            {
                CatsUnlockedBool.Add(false);
            }
            AllCats.Add(new data.CatData("Base", 0, "base"));
            SaveChats();
            FirstTime += 1;
            PlayerPrefs.SetInt("FirstTime", FirstTime);
            Money = 3;
            PlayerPrefs.SetInt("Money", Money);
            // ACTIVE TUTORIAL
            // Tutorial.SetActive(true);
        }

        public void Init()
        {
            // La ligne AllCats a servis � cr�e le d�part de la liste AllCats, sans �a, la liste renvoy�e null.
            FirstTime = PlayerPrefs.GetInt("FirstTime", FirstTime);
            // Pour tester, commenter cette ligne lors de la production
            // A REFAIRE : Check seulement la liste si elle est vide, y rajouter un �l�ment si oui ou juste {};
            // FirstTime = 1;
            // Pour la premi�re fois les �l�ments qui comptent : Tutoriel, Unlocked Cats & List Cats, Resources, Tuto, Carnet).

            if (FirstTime == 0)
            {
                FirstTimeFunc();
            }
            //

            // Etats
            Debug.Log("INITIALIZATION");
            GameInMaison = false;
            GameInRue = false;
            GameStarted = false;
            GameInAction = false;
            CanPlayFishing = true;
            CanTouch = true;
            AudioOnOff = true;

            SceneManager.LoadScene("Rue", LoadSceneMode.Single);
            GameInRue = true;

            GetChats(); // get cats saved

            // Valeurs
            Money = PlayerPrefs.GetInt("Money", Money);
            NbCatsAdopted = PlayerPrefs.GetInt("NbCatsAdopted", NbCatsAdopted);
            TutoUp = PlayerPrefs.GetInt("TutoUp", TutoUp);

            // CHEATCODE
            // Money = 500;

            // Gestion UI
            textNbCatsAdopted.text = NbCatsAdopted.ToString();
            textMoney.text = Money.ToString();
        }
        #endregion

        public void Update()
        {
            // Prevent negative value for Money
            if (Money < 0)
            {
                Money = 0;
            }
        }

        #region Values
        public void AddMoney(int num)
        {
            Money += num;
            PlayerPrefs.SetInt("Money", Money);
            textMoney.text = Money.ToString();
            Debug.Log("Add " + num + " Money");
            // Change UI...
        }
        public void LoseMoney(int num)
        {
            Money -= num;
            PlayerPrefs.SetInt("Money", Money);
            textMoney.text = Money.ToString();
            Debug.Log("Lose " + num + " Money");
            // Change UI...
        }

        public void AddCat(int num)
        {
            NbCatsAdopted += num;
            PlayerPrefs.SetInt("NbCatsAdopted", NbCatsAdopted);
            textNbCatsAdopted.text = NbCatsAdopted.ToString();
            Debug.Log("Add " + num + " NbCatsAdopted");
            // Change UI...
        }
        public void LoseCat(int num)
        {
            NbCatsAdopted -= num;
            PlayerPrefs.SetInt("NbCatsAdopted", NbCatsAdopted);
            textNbCatsAdopted.text = NbCatsAdopted.ToString();
            Debug.Log("Lose " + num + " NbCatsAdopted");
            // Change UI...
        }
        #endregion

        #region JSON
        // ATTENTION : streamingAssetPath � changer en production en persistentPath
        public void SaveChats()
        {
            string path = Application.persistentDataPath + "/" + "data.json";
            string pathForUnlocked = Application.persistentDataPath + "/" + "unlocked.json";

            //string path = Application.streamingAssetsPath + "/" + "data.json";
            //string pathForUnlocked = Application.streamingAssetsPath + "/" + "unlocked.json";
            JsonHelper.WriteToJsonFile(path, AllCats);
            JsonHelper.WriteToJsonFile(pathForUnlocked, CatsUnlockedBool);
        }

        public void GetChats()
        {
            string path = Application.persistentDataPath + "/" + "data.json";
            string pathForUnlocked = Application.persistentDataPath + "/" + "unlocked.json";

            //string path = Application.streamingAssetsPath + "/" + "data.json";
            //string pathForUnlocked = Application.streamingAssetsPath + "/" + "unlocked.json";
            CatsUnlockedBool = JsonHelper.ReadFromJsonFile<List<bool>>(pathForUnlocked);
            AllCats = JsonHelper.ReadFromJsonFile<List<data.CatData>>(path);
        }
        #endregion

        public IEnumerator CanPlayFish()
        {
            yield return new WaitForSeconds(2);
            GetComponent<AudioSource>().clip = fishing;
            GetComponent<AudioSource>().loop = true;
            GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(120);
            CanPlayFishing = true;
        }

        public IEnumerator ErrorCoroutine(GameObject err)
        {
            err.SetActive(true);
            yield return new WaitForSeconds(3);
            err.SetActive(false);
        }

        public void GiveCatsName()
        {
            if (NameTextArea.text == "" || AllCats.Count >= 32 || NameTextArea.text.Length >= 10 || string.IsNullOrWhiteSpace(NameTextArea.text))
            {
                // Show message error input field is empty, a cat need a name
                StartCoroutine(ErrorCoroutine(InvalidName));
                Debug.Log("[ERROR] INPUT FIELD IS EMPTY OR CAT LIMIT REACHED");
            }
            else
            {
                // Check if name already taken by another cat in list
                var taken = Instance.AllCats.Find(item => item.catName == NameTextArea.text);
                if (taken == null)
                {
                    ThisCat.GetComponent<Cat>().Name = NameTextArea.text;

                   
                    // Debloque le bon portrait dans les chats "unlocked/locked"
                    for (var i = 0; i < ThisCat.GetComponent<AnimationControl>().ListAnimatorController.Length; i++)
                    {
                        if (ThisCat.GetComponent<AnimationControl>().nameAnimatorController == ThisCat.GetComponent<AnimationControl>().ListAnimatorController[i].name)
                        {
                            if (Instance.CatsUnlockedBool[i] == false)
                            {
                                Instance.CatsUnlockedBool[i] = true;
                            }
                        }
                    }

                    //
                    Instance.AddCat(1);
                    ThisCat.GetComponent<SaveCat>().Save();
                    PanelName.SetActive(false);
                    Instance.GameInAction = false;
                    Vector3 pos = ThisCat.transform.position;
                    // Cat leave
                    GameObject v = Instantiate(Vanish, pos, Quaternion.identity);
                    v.SetActive(true);
                    StartCoroutine(Vanishing(v));
                    Destroy(ThisCat);
                }
                else
                {
                    Debug.Log("Name already taken, retry");
                    return;
                }
            }
        }

        IEnumerator Vanishing(GameObject v)
        {
            yield return new WaitForSeconds(0.5f);
            Destroy(v);
        }

        public void DeleteCat()
        {
            foreach (var cat in Instance.AllCats.ToArray())
            {
                if (cat.catName == CatToDelete.GetComponent<TextMeshProUGUI>().text)
                {
                    if (Instance.GameInMaison)
                    {
                        GameObject[] cats = GameObject.FindGameObjectsWithTag("cat");
                        
                        foreach (var c in cats)
                        {
                            if (c.GetComponent<Cat>().Name == cat.catName)
                            {
                                Destroy(c);
                            }
                        }
                    }
                    Instance.AllCats.Remove(cat);
                    Instance.LoseCat(1);
                    Instance.SaveChats();
                    Destroy(CatToDelete.transform.parent.gameObject);
                }
            }
        }

        // Audio & UI
        public void TurnPageSoundFunc()
        {
            GetComponent<AudioSource>().clip = GameManager.Instance.TurnPage;
            GetComponent<AudioSource>().loop = false;
            GetComponent<AudioSource>().Play();
        }

        public void OpenCarnet()
        {
            if (ObjectCarnetAchievements.activeInHierarchy || ObjectMyCatsForPortrait.activeInHierarchy)
            {
                GameInAction = false;
                GetComponent<AudioSource>().clip = GameManager.Instance.CloseCarnet;
                GetComponent<AudioSource>().loop = false;
                GetComponent<AudioSource>().Play();
                try
                {
                    ObjectCarnetAchievements.SetActive(false);
                    ObjectMyCatsForPortrait.SetActive(false);
                }
                catch
                {
                    //...
                }
            }
            else
            {
                GameInAction = true;
                ObjectCarnetAchievements.SetActive(true);
                GetComponent<AudioSource>().clip = GameManager.Instance.OpenCarnetSound;
                GetComponent<AudioSource>().loop = false;
                GetComponent<AudioSource>().Play();
            }
        }

        public void CloseCarnetBoolFalse()
        {
            GameInAction = false;
        }
    }
}
