using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public float runSpeed = 40f;
    public Transform GroundCheck;
    bool OnGround = true;
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
        if (GroundCheck.position.y < 3)
        {
            OnGround = false;
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed *0.2f;
        }
        if (GroundCheck.position.y > 0)
        {
            OnGround = true;
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        }
     //   horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
      //  animator.SetFloat("Speed",Mathf.Abs(horizontalMove));
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
          //  animator.SetBool("IsJumping", jump);
        }
    }
    void FixedUpdate() {

        Debug.Log(GroundCheck.position.y);
        Debug.Log(OnGround);
        controller.Move(horizontalMove * Time.fixedDeltaTime, OnGround, jump);
        jump = false;
     //   animator.SetBool("IsJumping", jump);
    }

}
