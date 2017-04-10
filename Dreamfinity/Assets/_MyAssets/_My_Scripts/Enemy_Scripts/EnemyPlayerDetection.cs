﻿using UnityEngine;


public class EnemyPlayerDetection : MonoBehaviour {

	 GameObject playerRef;
	private EnemyController m_enemyController;
	public float fieldOfViewAngle;
    public float headRotMaxAngle;
    SphereCollider col;
    public GameObject m_headObjRef;
    Transform headReff,playerTrans;
    Vector3 direction;

    public bool playerInSight;
    public int m_detectionDistance;
    
    void Start () {
        m_enemyController = gameObject.transform.GetComponent<EnemyController>();
		fieldOfViewAngle = 170;
        col = GetComponent<SphereCollider>();
        playerRef = GameObject.FindWithTag("Player");
        m_headObjRef = GameObject.FindWithTag("Head");
            
    }


    public void Update()
    {
        headReff = m_headObjRef.transform;
        playerTrans = playerRef.transform;
        direction = playerTrans.position - headReff.position;
    }


     
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.transform.tag == "Player")
        {
            float angle = Vector3.Angle(direction, headReff.forward);

            //if (angle < fieldOfViewAngle * 0.5f)
            //{
                RaycastHit hit;

                if (Physics.Raycast(headReff.position, direction, out hit))
                {
                    if (hit.collider.gameObject.transform.tag == "Player")
                    {
                       
                        m_enemyController.playerInSight = true;
                        m_enemyController.lastPlayerSighting = playerRef.transform.position;
                        PlayerInSight();
                        playerInSight = true;
                    }
                    else
                    {
                        m_enemyController.playerInSight = false;

                        playerInSight = false;
                    }

                }
                
            //}
        }

        
    }

    void PlayerInSight()
    {

        Quaternion headRot = Quaternion.LookRotation(playerRef.transform.position - headReff.transform.position, Vector3.up);
        float headRotAngle = Vector3.Angle(headReff.forward, this.transform.forward);
       
            headReff.rotation = headRot;
      
       
    }

   
}
