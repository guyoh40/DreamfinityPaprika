using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LucidityProjectile : MonoBehaviour {
    public LucidityControl m_plrResCont;
    Rigidbody m_rigBodRef;
    float timeSinceBirth; //float value to track how long this is alive
    public float lifeSpan = 3f; //length of time this object can be alive before destroying its self

    public float m_decelForc; //The amount of deceleration when the projectile collides with something;

    public void Start()
    {
        timeSinceBirth = Time.time;
        m_plrResCont = GameObject.FindWithTag("Player").GetComponent<LucidityControl>();
        m_rigBodRef = GetComponent<Rigidbody>();
    }


    public void Update()
    { 
        if (Time.time - timeSinceBirth > lifeSpan || Input.GetKey(KeyCode.F1))
        {
            Destroy(this.gameObject);   
        }
    }

    public void OnCollisionEnter(Collision other)
    {
        m_rigBodRef.useGravity = true;
        lifeSpan *= 10;

        if (other.collider.gameObject.transform.tag == ("Player"))
        {


            if(m_plrResCont.balance < m_plrResCont.limit)
            {
                m_plrResCont.Deposit(1);
                Destroy(this.gameObject);
            }
           
    }
}

}
