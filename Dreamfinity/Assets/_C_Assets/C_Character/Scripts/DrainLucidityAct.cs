using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrainLucidityAct : MonoBehaviour {

    public GameObject m_Cam;
    public float m_harvestReach;
    LucidityControl m_rescourseControl;



    private void Start() 
    {   
        m_Cam = GameObject.FindWithTag("MainCamera");
        m_rescourseControl = GetComponent<LucidityControl>();
    }

    public void Drain(bool drain)
    {
        if(drain)
        {
            Ray harvestRay = new Ray(new Vector3(transform.position.x, transform.position.y, transform.position.z), m_Cam.transform.forward);
            RaycastHit hitInfo;

            Debug.DrawRay(transform.position, m_Cam.transform.forward,Color.blue);

            if(Physics.Raycast(harvestRay, out hitInfo, m_harvestReach))
            {
                if (hitInfo.collider.gameObject.tag == "Well")
                {
                    float _rps = hitInfo.collider.gameObject.GetComponent<WellControl>().GetRPS();
                    hitInfo.collider.gameObject.SendMessage("DrainWell", gameObject.transform);
                }
            }
        }        
    }
}
//new Vector3(transform.position.x, transform.position.y, transform.position.z)