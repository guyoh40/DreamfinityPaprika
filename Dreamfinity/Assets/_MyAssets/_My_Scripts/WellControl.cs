using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]

public class WellControl : MonoBehaviour {

    public LucidityControl m_resCont;
    public LucidityControl m_plrResCont;
    ParticleSystem m_DrainEffect;
    public int rps;//"Return Per Second" when being drained
    public bool m_draining; // True when the well is being drained
    bool m_filling; // True when the well is being filled

    GameObject m_castingObj;

    public void Start()
    {

        m_DrainEffect = GetComponent<ParticleSystem>();
        m_plrResCont = GameObject.FindWithTag("Player").GetComponent<LucidityControl>();
        m_resCont = GetComponent<LucidityControl>();
        m_DrainEffect.IsAlive();

        m_castingObj = Resources.Load("pfb_RescourseCast") as GameObject;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.transform.tag == "Player")
        {
            if(Input.GetButtonDown("Drain") /*&& m_resCont.balance > 0*/)
            {
                Instantiate(m_castingObj, transform.position + Vector3.up, transform.rotation);
                
            }
        }
    }


    public void Update()
    { 
        if(!m_draining)
        {
            m_DrainEffect.Stop();  
        }
        else
        {
            Debug.Log("in!");
            m_resCont.Withdraw(rps);
            m_plrResCont.Deposit(rps);
        }
        //transform.localScale = new Vector3(m_resCont.balance, m_resCont.balance, m_resCont.balance);
        m_draining = false;
    }

    public float GetRPS()
    {
        return rps;
    }

    public void DrainWell(Transform target)
    {
        if (m_resCont.balance > 0)
        {
            Quaternion direction = Quaternion.LookRotation(target.position - transform.position, Vector3.up);
            m_DrainEffect.Play();
            m_DrainEffect.transform.rotation = direction;
            m_draining = true;

        }  
        else
        {
            m_draining = false;
        }
    }
}
