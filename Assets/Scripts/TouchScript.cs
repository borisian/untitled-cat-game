using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Chats
{
    public class TouchScript : MonoBehaviour
    {
        Vector3 touchStart;
        Vector3 oldPos;
        Vector3 catPos;
        GameObject catTouched;
        [Header("Fishing Game On/Off")]
        public GameObject fishingZone;
        public GameObject fishingGame;
        public AudioSource fishingSound;
        public AudioClip startFishing;
        public AudioClip inFishingAction;
        public GameObject Cooldown;

        [Header("Zoom On/Off")]
        public bool zoomFish;
        public bool dezoomFish;
        public bool zoomActive;
        public bool dezoom;
        [Header("Limite Caméra")]
        public float zoomOutMin = 3;
        public float zoomOutMax = 5;
        public float limitLeft = -3f;
        public float limitRight = 3f;
        public float limitUp = 0.5f;
        public float limitDown = -1f;

        [Header("Audio")]
        public AudioClip[] meowSound;

        public void Start()
        {
            if (GameManager.Instance.GameInRue)
            {
                fishingSound = fishingZone.GetComponent<AudioSource>();
            }
            zoomActive = false;
            dezoom = false;
            zoomFish = false;
            dezoomFish = false;
        }

        void Update()
        {
            if (GameManager.Instance.GameStarted && GameManager.Instance.CanTouch)
            {
                TouchFunction();
            }
        }

        public void TouchFunction()
        {
            if (Input.GetMouseButtonDown(0))
            {
                touchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
            if (Input.touchCount == 2)
            {
                Touch touchZero = Input.GetTouch(0);
                Touch touchOne = Input.GetTouch(1);

                Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

                float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

                float difference = currentMagnitude - prevMagnitude;

                zoom(difference * 0.003f);
            }
            else if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject() && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                // MOVE CAMERA
                #region MoveCamera
                if (!GameManager.Instance.GameInAction)
                {
                    Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
                    float speed = 0.4f;
                    Camera.main.transform.Translate(-touchDeltaPosition.x * speed * Time.deltaTime, -touchDeltaPosition.y * 0 * Time.deltaTime, 0);

                    //Vector3 direction = touchStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    //Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, direction, Time.deltaTime * 50);
                    // LIMIT MOVE
                    Camera.main.transform.position = new Vector3(Mathf.Clamp(Camera.main.transform.position.x, limitLeft, limitRight), Mathf.Clamp(Camera.main.transform.position.y, limitDown, limitUp), -10);
                }
                #endregion

                // TOUCH CAT
                RaycastHit2D hit = Physics2D.Raycast(touchStart, Camera.main.transform.forward);

                if (hit.collider != null)
                {

                    if (GameManager.Instance.GameInMaison && !GameManager.Instance.GameInAction)
                    {
                        if (hit.collider.gameObject.tag == "cat")
                        {
                            catTouched = hit.collider.gameObject;
                            // Sound on click
                            int n = Random.Range(0, meowSound.Length);
                            catTouched.GetComponent<AudioSource>().clip = meowSound[n];
                            catTouched.GetComponent<AudioSource>().Play();
                            // Coeur on cat
                            StartCoroutine(CoeurOnCat(catTouched.GetComponent<Cat>().Coeur));
                        }
                        if (hit.collider.gameObject.tag == "scenery")
                        {
                            GameObject obj = hit.collider.gameObject;
                            obj.GetComponent<Animator>().ResetTrigger("clicked");
                            obj.GetComponent<Animator>().SetTrigger("clicked");
                            try
                            {
                                obj.GetComponent<AudioSource>().Play();
                            }
                            catch
                            {
                                Debug.Log("This Object don't have audio source");
                                return;
                            }
                        }
                    }
                    if (GameManager.Instance.GameInRue && !GameManager.Instance.GameInAction)
                    {
                        if (hit.collider.gameObject.tag == "cat")
                        {
                            catTouched = hit.collider.gameObject;
                            catTouched.GetComponent<AgentScript>().isInAction = true;
                            catPos = catTouched.transform.position;
                            GameManager.Instance.GameInAction = true;
                            oldPos = Camera.main.transform.position;
                            zoomActive = true;

                            // Sound on click
                            int n = Random.Range(0, meowSound.Length);
                            catTouched.GetComponent<AudioSource>().clip = meowSound[n];
                            catTouched.GetComponent<AudioSource>().Play();
                        }
                        if (hit.collider.gameObject.tag == "fishing")
                        {
                            if (GameManager.Instance.CanPlayFishing)
                            {
                                fishingSound.clip = startFishing;
                                fishingSound.Play();
                                GameManager.Instance.GameInAction = true;
                                oldPos = Camera.main.transform.position;
                                zoomFish = true;
                                fishingGame.SetActive(true);
                                GameManager.Instance.CanPlayFishing = false;
                                StartCoroutine(GameManager.Instance.CanPlayFish());
                                Cooldown.GetComponent<Cooldown>().isCD = true;
                            }
                        }
                    }
                }
            }
            zoom(Input.GetAxis("Mouse ScrollWheel"));
        }

        IEnumerator CoeurOnCat(GameObject c)
        {
            c.SetActive(true);
            yield return new WaitForSeconds(3);
            c.SetActive(false);
        }

        public void LateUpdate()
        {
            // Zoom On Cat
            if (zoomActive)
            {
                float speed = 0.02f;
                Vector3 pos = new Vector3(catPos.x, catPos.y, -10);
                Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, pos, speed);
                Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, 3, speed);
                if (!GameManager.Instance.GameInAction)
                {
                    dezoom = true;
                    zoomActive = false;
                }
            }
            if (dezoom)
            {
                Camera.main.transform.position = oldPos;
                Camera.main.orthographicSize = 5;
                dezoom = false;
            }

            // Zoom On Fish
            if (zoomFish)
            {
                float speed = 0.02f;
                Vector3 pos = new Vector3(fishingZone.transform.position.x, fishingZone.transform.position.y, -10);
                Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, pos, speed);
                Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, 3, speed);
                if (!GameManager.Instance.GameInAction)
                {
                    dezoomFish = true;
                    zoomFish = false;
                }
            }
            if (dezoomFish)
            {
                Camera.main.transform.position = oldPos;
                Camera.main.orthographicSize = 5;
                dezoomFish = false;
            }
        }

        public void zoom(float increment)
        {
            Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - increment, zoomOutMin, zoomOutMax);
        }
    }
}