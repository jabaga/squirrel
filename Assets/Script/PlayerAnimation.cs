using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Animator animator;

    Rigidbody2D body;
    
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        // the player is moving UP/DOWN
        if(Math.Round(body.velocity.y, 1) != 0)
        {
            animator.SetBool("isJumping", true);
        } else
        {
            animator.SetBool("isJumping", false);
        }

        // the player is moving LEFT/RIGHT
        if (Math.Round(body.velocity.x, 1) != 0)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }

        if(Input.GetButton("Fire1"))
        {
            animator.SetBool("isShooting", true);
        }
        else
        {
            animator.SetBool("isShooting", false);
        }
    }
}
