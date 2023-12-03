using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Chats
{
    public class AgentScript : MonoBehaviour
    {

        [SerializeField] Vector3 target;

        [Header("UI du chat")]
        public GameObject UIRueCat;
        public GameObject UIBtnAdopt;
        //
        private NavMeshAgent agent;

        [Header("Distance d'arrêt du path")]
        public float pathEndThreshold = 0.1f;

        [Header("Temps d'arrêt/marche")]
        // Temps d'arrêt et de marche aléatoire dans Wander()
        public int MinWalkTime = 8;
        public int MaxWalkTime = 15;
        public int MinWaitTime = 3;
        public int MaxWaitTime = 6;
        //
        [Header("Etat du chat")]
        // STATES
        public bool isMoving = false;
        public bool isWandering = false;
        public bool isInAction = false;
        [SerializeField] private bool hasPath = false;
        public bool flip = false;
        //
        // FLIP
        private float oldPosition = 0.0f;
        //

        void Start()
        {
            // Init
            isMoving = true;
            isWandering = false;
            isInAction = false;

            // Prevent bug animations at spawn
            /*
            if (!isMoving)
            {
                gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
            }
            if (isMoving)
            {
                gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
            }
            */

            agent = GetComponent<NavMeshAgent>();
            agent.updateRotation = false;
            agent.updateUpAxis = false;

            // FLIP
            oldPosition = transform.position.x;
        }

        public void Update()
        {
            Flip();
        }

        private void FixedUpdate()
        {
            // If its not wandering and not in action
            if (!isWandering && !isInAction)
            {
                StartCoroutine(Wander());
            }

            if (isInAction && isWandering)
            {
                // In Action =>
                if (GameManager.Instance.GameInRue)
                {
                    UIRueCat.SetActive(true);
                    if (!UIBtnAdopt.activeSelf)
                    {
                        UIBtnAdopt.SetActive(true);
                    }
                    StopCoroutine(Wander());
                    isWandering = false;
                    isMoving = false;
                    MoveAndStop();
                }
            }
            // Ray of the target for each cat [DEBUG]
            Debug.DrawRay(target, transform.TransformDirection(Vector2.up) * 1, Color.red);
            // Detect if the agent is on target
            DetectPathComplete();
        }

        public void Flip() // Flip in AnimationControl.cs
        {
            if (transform.position.x > oldPosition) // he's looking right
            {
                flip = true;
            }

            if (transform.position.x < oldPosition) // he's looking left
            {
                flip = false;
            }
            oldPosition = transform.position.x; // update the variable with the new position so we can chack against it next frame
        }

        IEnumerator Wander()
        {
            int walkTime = Random.Range(MinWalkTime, MaxWalkTime);
            int waitTime = Random.Range(MinWaitTime, MaxWaitTime);

            isWandering = true;

            // === MOVE ===
            isMoving = true;
            MoveAndStop();

            yield return new WaitForSeconds(walkTime);

            // === IDLE ===
            isMoving = false;
            MoveAndStop();

            yield return new WaitForSeconds(waitTime);

            // === RELOAD ===
            isWandering = false;
        }
        private void MoveAndStop()
        {
            // MOVE
            if (isMoving)
            {
                //gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
                target = GetRandomPosition();
                agent.SetDestination(target);
            }
            // STOP
            else
            {
                //gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
                agent.ResetPath();
            }
        }

        // Detect the end of the path for the agent
        bool AtEndOfPath()
        {
            hasPath |= agent.hasPath;
            pathEndThreshold = Random.Range(0.1f, 0.5f);
            if (hasPath && agent.remainingDistance <= agent.stoppingDistance + pathEndThreshold)
            {
                // Arrived
                hasPath = false;
                return true;
            }
            return false;
        }

        // Do this after the end of the path for the agent
        private void DetectPathComplete()
        {
            if (AtEndOfPath())
            {
                // STOP AGENT
                StopCoroutine(Wander());
                isMoving = false;
                MoveAndStop();
                WaitAndGo();
            }
        }

        // Wait and reload Move
        IEnumerator WaitAndGo()
        {
            yield return new WaitForSeconds(Random.Range(3, 6));
            isWandering = false;
        }

        // Get a random position in Circle (*check size*)
        Vector3 GetRandomPosition()
        {
            // random cercle dans la zone inverse
            // Vector3 LeftPosition = new Vector3(Random.Range(-2, -10), Random.Range(-5, -2), transform.position.z);

            // Vector3 RightPosition = new Vector3(Random.Range(2, 10), Random.Range(-5, -2), transform.position.z);

            Vector3 RuePos = new Vector3(Random.Range(-38, 38), Random.Range(-5, -2), transform.position.z);

            Vector3 MaisonPos = new Vector3(Random.Range(-30, 27), Random.Range(-5, -1), transform.position.z);

            if (GameManager.Instance.GameInRue)
            {
                return RuePos;
            }
            else if (GameManager.Instance.GameInMaison)
            {
                return MaisonPos;
            }
            else
            {
                return new Vector3(0, 0, 0);
            }
        }
    } // end GetRandomPosition()
}
