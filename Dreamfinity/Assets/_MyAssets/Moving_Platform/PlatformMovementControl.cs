using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


namespace C_Contetnt.C_Puzzles
{

    [RequireComponent(typeof(LucidityControl))]
    [ExecuteInEditMode]


   

    public class PlatformMovementControl : MonoBehaviour
    {
        CharacterIKControl ikController;
        public LucidityControl lucContRef;
        Rigidbody rigBodRef;
        public GameObject wayPointPrntObj;
        public GameObject[] wayPoints;
        public Vector3 posFrom, posTo;

        public int currentWayPointIndex, nextWayPointIndex;
        public float speed;
        public float lucBalanceF;
        bool activated;

        float time;

        

        private void Start()
        {

            rigBodRef = GetComponent<Rigidbody>();
            activated = false;
            lucContRef = GetComponent<LucidityControl>();
            lucContRef.limit = 1; 
            lucContRef.balance = 0;
            currentWayPointIndex = 0;
            nextWayPointIndex = 1;
            WayPointArrayAllocation();
            posFrom = wayPointPrntObj.transform.GetChild(currentWayPointIndex).transform.position;
            posTo = wayPointPrntObj.transform.GetChild(nextWayPointIndex).transform.position;


        }

       


        private void OnCollisionEnter(Collision other)
        {       
            /* The activation function */
            if (other.collider.gameObject.transform.tag == "Casting" && !activated)
            {
                time = Time.time;
                posFrom = rigBodRef.position;
                activated = true;
                GameObject.Destroy(other.gameObject);
            }

            if(other.collider.gameObject.transform.tag == "Player")
            {
                other.collider.gameObject.transform.parent = this.transform;
            }
        }

        private void OnCollisionExit(Collision other)
        {
            if (other.collider.gameObject.transform.tag == "Player")
            {
                other.collider.gameObject.transform.parent = null;
            }
        }

        private void WayPointArrayAllocation()
        {
            if (wayPointPrntObj == null)
            {
                Debug.LogError(gameObject.name + " does not have a waypoint parent obj");
            }
            else       //Dynamcialy addjusting the array at the beging of the game;
            {
                wayPoints = new GameObject[wayPointPrntObj.transform.childCount];

                for (int i = 0; i < wayPointPrntObj.transform.childCount; i++)
                {
                    wayPoints[i] = wayPointPrntObj.transform.GetChild(i).gameObject;
                }
            }
        }

        public void FixedUpdate()
        {
            if (activated)
            {
                TransPos();
            }
           
        }

        void WayPointIndexIncrease()
        {
            currentWayPointIndex++;
            if (currentWayPointIndex > wayPointPrntObj.transform.childCount - 1)
            {
                currentWayPointIndex = 0;
            }

            nextWayPointIndex++;
            if (nextWayPointIndex > wayPointPrntObj.transform.childCount - 1)
            {
                nextWayPointIndex = 0;
            }
        }

        

        void TransPos()
        {
            float distance = Vector3.Distance(rigBodRef.position, posTo);
            
            transform.position = Vector3.Lerp(posFrom ,posTo, Time.time - time);
            if(distance < 0.1f)
            {
                activated = false; 
                posFrom = transform.position;
                WayPointIndexIncrease();
                posTo = wayPointPrntObj.transform.GetChild(nextWayPointIndex).transform.position;
                
            }


            
        }
    }
}

//Vector3.Lerp(rigBodRef.position, posTo, Mathf.Pow(Mathf.Sin(Time.time * speed), 2))
