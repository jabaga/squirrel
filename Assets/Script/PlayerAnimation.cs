using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Animator animator;
    public ToggleBadass toggleBadass;

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

        if(Input.GetButton("Fire1") && PlayerData.currentBullets > 0 && toggleBadass.badassMode == true)
        {
            animator.SetBool("isShooting", true);
        }
        else
        {
            animator.SetBool("isShooting", false);
        }



        // CHEAT CODES
        frame++;

        timeFromLastKeyPress += Time.deltaTime;

        // a key was pressed
        if (Input.inputString.Length > 0)
        {
            timeFromLastKeyPress = 0;
            typedString += Input.inputString;
        }

        // run every X frames
        if (frame == 30)
        {
            frame = 0;
            return;
        }

        if (timeFromLastKeyPress >= timeToClear && typedString.Length > 0)
        {
            typedString = "";
        }

        // check for cheat code
        if (typedString.Length > 0)
        {
            if (typedString == "fuck")
            {
                CheatLifes();
                typedString = "";
            }
        }
    }

    void CheatLifes()
    {
        PlayerData.currentLifes += 10;
        PlayerData.currentBullets += 10;
    }

    float timeFromLastKeyPress = 0;
    string typedString = "";
    float timeToClear = 1;
    int frame = 0;

}
