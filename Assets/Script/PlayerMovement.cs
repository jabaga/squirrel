﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public float runSpeed = 40f;
   // public Animator animator;
    float horizontalMove = 0f;
    bool jump = false;
    // Start is called before the first frame update
    void Start()
    {
        
      //  if ()
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
      //  animator.SetFloat("Speed",Mathf.Abs(horizontalMove));
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
          //  animator.SetBool("IsJumping", jump);
        }
    }
    void FixedUpdate() {

        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
     //   animator.SetBool("IsJumping", jump);
    }

}
