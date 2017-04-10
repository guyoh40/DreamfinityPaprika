﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GamePlayManager : MonoBehaviour {

    /*This will track the state of the game*/
    public enum GameState { Playing, Paused };


    public GameState m_gameState;
    GameObject m_DebugUI;
    GameObject m_CameraObj;

	// Use this for initialization
	void Start () {
        m_DebugUI = GameObject.FindWithTag("DebugUI");
        m_CameraObj = GameObject.FindWithTag("MainCamera");
        m_gameState = GameState.Playing;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
	
	// Update is called once per frame
	void Update ()
    {

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        //CursorHandling();
        //StateMachine();


        //if(Input.GetButtonDown("Cancel"))
        //{

        //}


    }

    //private void StateMachine()
    //{
    //    if(m_gameState == GameState.Paused)
    //    {
    //        Time.timeScale = 0.0f;
    //    }

    //    if (m_gameState == GameState.Playing)
    //    {
    //        Time.timeScale = 1.0f;
    //    }
    //}

   

    private void CursorHandling()
    {    
            if (m_DebugUI.GetComponent<Canvas>().enabled)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                //m_CameraObj.GetComponent<v3rdPersonCamera>().lockCamera = true;

            }
            else
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                //m_CameraObj.GetComponent<v3rdPersonCamera>().lockCamera = false;
            }

        
    }
}
