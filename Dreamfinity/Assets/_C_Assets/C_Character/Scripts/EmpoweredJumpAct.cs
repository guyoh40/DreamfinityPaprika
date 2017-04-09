using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmpoweredJumpAct : MonoBehaviour {

    GameObject playerRef;
    CharacterControler charContRef;
    LucidityControl playerLucContRef;
    Rigidbody rigBodRef;

    public ParticleSystem particleRef;

    bool doubleJumpFlag;
    bool isDblJumping;
    bool isJumping;

    public float jumpDeration;
    float timer;
    public float doubleJumpForce;

    private void Start()
    {
        jumpDeration = 1;
        doubleJumpForce = 30;

        playerRef = GameObject.FindWithTag("Player");
        charContRef = playerRef.GetComponent<CharacterControler>();
        playerLucContRef = playerRef.GetComponent<LucidityControl>();

        rigBodRef = playerRef.GetComponent<Rigidbody>();
        doubleJumpFlag = false;
        isDblJumping = false;


    }

    private void Update()
    {
        if (charContRef.m_isGrounded && Input.GetButtonDown("Jump"))
        {
            isJumping = true;
        }

        if (isJumping && Input.GetButtonUp("Jump"))
        {
            isJumping = false;
            doubleJumpFlag = true;
        }

        if (doubleJumpFlag)
        {
            if (Input.GetButtonDown("Jump") && playerLucContRef.balance >= 1 && doubleJumpFlag)
            {
                isDblJumping = true;
                timer = Time.time;
                playerLucContRef.Withdraw(1);

                particleRef.Play();
            }

            if (Input.GetButton("Jump") && isDblJumping)
            {
                if (Time.time - timer < jumpDeration)
                {
                    //rigBodRef.useGravity = false;
                    playerRef.GetComponent<Rigidbody>().AddForce(Vector3.up * doubleJumpForce, ForceMode.Impulse);
                }
                else
                {
                    isJumping = false;
                    isDblJumping = false;
                    doubleJumpFlag = false;

                    particleRef.Stop();
                }
            }

            if (Input.GetButtonUp("Jump") && isDblJumping)
            {
                isJumping = false;
                isDblJumping = false;
                doubleJumpFlag = false;
                particleRef.Stop();

            }

            if (!doubleJumpFlag)
            {
                //rigBodRef.useGravity = true;
            }

            if (charContRef.m_isGrounded)
            {
                isJumping = false;
                isDblJumping = false;
                doubleJumpFlag = false;
                particleRef.Stop();

            }
        }
    }

}
