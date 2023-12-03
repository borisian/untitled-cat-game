using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chats
{
    public class Minigames : MonoBehaviour
    {
        // FISHING
        [Header("Fishing Area")]
        [SerializeField] Transform topBounds;
        [SerializeField] Transform bottomBounds;

        [Header("Fish Settings")]
        [SerializeField] Transform fish;
        [SerializeField] float smoothMotion = 3f;
        [SerializeField] float fishTimeRandomizer = 3f;
        float fishPosition;
        float fishSpeed;
        float fishTimer;
        float fishTargetPosition;

        [Header("Hook Settings")]
        [SerializeField] Transform hook;
        [SerializeField] float hookSize = .18f;
        [SerializeField] float hookSpeed = .1f;
        [SerializeField] float hookGravity = .05f;
        float hookPosition;
        float hookPullVelocity;

        [Header("Progress Bar Settings")]
        [SerializeField] Transform progressBarContainer;
        [SerializeField] float hookPower;
        [SerializeField] float progressBarDecay;
        float catchProgress;

        //

        private void FixedUpdate()
        {
                MoveFish();
                MoveHook();
                CheckProgress();
        }

        public void OnEnable()
        {
            catchProgress = .004f;
        }

        public void OnDisable()
        {
            // Reset Game
        }

        private void CheckProgress()
        {
            Vector3 progressBarScale = progressBarContainer.localScale;
            progressBarScale.y = catchProgress;
            progressBarContainer.localScale = progressBarScale;

            float min = hookPosition - hookSize / 2;
            float max = hookPosition + hookSize / 2;

            if (min < fishPosition && fishPosition < max)
            {
                catchProgress += hookPower * Time.deltaTime;
                if (catchProgress >= 0.0156f)
                {
                    GameManager.Instance.GetComponent<AudioSource>().clip = GameManager.Instance.Win;
                    GameManager.Instance.GetComponent<AudioSource>().loop = false;
                    GameManager.Instance.GetComponent<AudioSource>().Play();
                    Debug.Log("WIN");
                    GameManager.Instance.AddMoney(1);
                    GameManager.Instance.GameInAction = false;
                    gameObject.SetActive(false);
                }
            }
            else
            {
                catchProgress -= progressBarDecay * Time.deltaTime;
                if (catchProgress <= 0)
                {
                    GameManager.Instance.GetComponent<AudioSource>().clip = GameManager.Instance.Error;
                    GameManager.Instance.GetComponent<AudioSource>().loop = false;
                    GameManager.Instance.GetComponent<AudioSource>().Play();
                    Debug.Log("LOSE");
                    GameManager.Instance.GameInAction = false;
                    gameObject.SetActive(false);
                }
            }

            catchProgress = Mathf.Clamp(catchProgress, 0, 0.0156f);
        }

        private void MoveHook()
        {
            if (Input.GetMouseButton(0))
            {
                // increase our pull velocity
                hookPullVelocity += hookSpeed * Time.deltaTime;
            }
            hookPullVelocity -= hookGravity * Time.deltaTime;
            hookPosition += hookPullVelocity;

            if (hookPosition - hookSize / 2 <= 0 && hookPullVelocity < 0)
            {
                hookPullVelocity = 0;
            }

            if (hookPosition + hookSize / 2 >= 1 && hookPullVelocity > 0)
            {
                hookPullVelocity = 0;
            }

            hookPosition = Mathf.Clamp(hookPosition, hookSize / 2, 1 - hookSize / 2);
            hook.position = Vector3.Lerp(bottomBounds.position, topBounds.position, hookPosition);
        }

        private void MoveFish()
        {
            // based on a timer, pick random position
            // move fish to that position smoothly
            fishTimer -= Time.deltaTime;
            if (fishTimer < 0)
            {
                // pick a new target position
                // and reset timer
                fishTimer = Random.value * fishTimeRandomizer;
                fishTargetPosition = Random.value;
            }
            fishPosition = Mathf.SmoothDamp(fishPosition, fishTargetPosition, ref fishSpeed, smoothMotion);
            fish.position = Vector3.Lerp(bottomBounds.position, topBounds.position, fishPosition);
        }

        public void StopFishing()
        {
            GameManager.Instance.GetComponent<AudioSource>().Pause();
            GameManager.Instance.GameInAction = false;
            gameObject.SetActive(false);
        }
    }
}
