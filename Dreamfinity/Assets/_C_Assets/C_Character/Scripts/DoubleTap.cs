using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DoubleTap : MonoBehaviour {

    bool m_isValid; // is the double press process valid?
    float m_initCoolDown, m_secondPressCooldDown; //cooldowns for both the first press and second 
    Input m_targetInput; //the input which is being dblePressed
    KeyCode[] m_validInputList; //array of valid input button codes

    /*UI*/
    Text DebugPannel;
    float m_DebugTextCoolDown;

    /*Examplle Script*/
    public float ButtonCooler; // Half a second before reset
    int ButtonCount = 0; 
    string m_inputDP;

    /*Empowered Dash Variables*/
    public float m_dashSpeed = .5f;
    public bool m_isDashing;

    float timer;
    CharacterControler m_charContRef;
    Rigidbody m_rigBodRef;
    GameObject m_camObjRef;
    float m_dashDir;

    public float m_dashForce;

    /*Debug UI */
    GameObject m_debugCanvasRef;
    GameObject m_debugTextField_01;



    private void Start()
    {
        if(ButtonCooler == 0)
        {
            ButtonCooler = 0.3f;  //if this value is not initalized in the editor it is set to a default
        }
        

        if(m_dashForce == 0)
        {
            m_dashForce = 1000f;
        }

        m_rigBodRef = GetComponent<Rigidbody>();
        m_camObjRef = GameObject.FindWithTag("MainCamera");

        /*Empowered Dash Variable Initialization*/
        m_charContRef = GetComponent<CharacterControler>();

        /*Debug UI init*/
        m_debugCanvasRef = GameObject.FindWithTag("DebugUI");
        m_debugTextField_01 = GameObject.FindWithTag("DebugUIText01");

    }

    
    void Update()
    {


        if (Input.GetButtonDown("Horizontal"))
        {             
            if (ButtonCooler > 0 && ButtonCount == 1/*Number of Taps you want Minus One*/)
            {
                m_isDashing = true;
                m_dashDir = Input.GetAxisRaw("Horizontal");
                timer = Time.time;   
            }
            else
            {
                ButtonCooler = 0.2f;
                ButtonCount += 1;
            }   
        }

        if (ButtonCooler > 0)
        {
            ButtonCooler -= 1 * Time.deltaTime;
        }
        else
        {
            ButtonCount = 0; 
        }  
    }

    private void FixedUpdate()
    {
        if (m_isDashing)
        {

            if (Time.time - timer <= m_dashSpeed)
            {
                m_charContRef.m_dashing = true;
                m_rigBodRef.AddForce(m_camObjRef.transform.right * m_dashDir * m_dashForce, ForceMode.Impulse);

            }
            else
            {
                m_charContRef.m_dashing = false;
            }

        }
    }

    void Dash(Rigidbody _rigBod, Vector3 dashDir)
    {
        _rigBod.AddForce(dashDir);

    }
}
