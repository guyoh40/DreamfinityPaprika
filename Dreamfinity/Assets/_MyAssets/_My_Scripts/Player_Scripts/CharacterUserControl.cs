using System;
using UnityEngine;
//using UnityStandardAssets.CrossPlatformInput;
using Invector.CharacterController;

namespace C_Utilities.C_Characters.C_ThirdPerson
{
    public class CharacterUserControl : MonoBehaviour
    {

        private v3rdPersonCamera m_camControl;   // A reference to the main camera in the scenes transform
        private Camera m_mainCam;
        private CharacterControler m_Character;

        private DrainLucidityAct m_DrainRescourse; 

        private SpendLucidityAct m_CastRescourse;

        private Transform m_Cam;

        private GameObject m_camObject;

        private Vector3 m_CamForward;    // The current forward direction of the camera

        private Vector3 m_Move;    // the world-relative desired move direction, calculated from the camForward and user input.

        private Vector2 m_MouseInput;    // Values 0 - 1 for mouse movement. 0 when the mouse is moving and 1 or -1 when the 

        private bool m_jump;    //Jumping or not

        private bool m_crouch; // Is the player Crouching?
        private bool m_drain, m_spend; // Booleans that are true when the player is draining or spending RES.
        private bool m_attack;
        private bool m_lucJump, m_lucAttack, m_lucDash;

        private void Start()
        {
            
           
            m_Character = GetComponent<CharacterControler>();
            m_camObject = GameObject.FindWithTag("MainCamera");
            m_camControl = m_camObject.GetComponent<v3rdPersonCamera>();
            m_DrainRescourse = GetComponent<DrainLucidityAct>();
            m_CastRescourse = GetComponent<SpendLucidityAct>();
            m_mainCam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();

            m_Cam = m_mainCam.transform;
        }


        public void Update()
        {
            if (!m_jump)
            {
                m_jump = Input.GetButton("Jump");
            } 

            m_spend = Input.GetButtonDown("Spend");
            m_lucJump = Input.GetButton("Jump");
            m_drain = Input.GetButton("Drain");
            m_attack = Input.GetButton("Attack");
 
            m_DrainRescourse.RayDrain(m_drain);
            m_CastRescourse.Cast(m_spend);  
        }

        private void FixedUpdate()
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            m_crouch = Input.GetButton("Crouch");
            Vector2 mouseAxis;
            mouseAxis = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

            m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
            m_Move = v*m_CamForward + h*m_Cam.right;
           
            m_Character.Move(m_Move, m_crouch, m_jump);
            m_camControl.RotateCamera(mouseAxis.x, mouseAxis.y);

            m_jump = false;
        }
    }
}
