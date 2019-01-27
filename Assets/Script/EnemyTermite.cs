using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTermite : MonoBehaviour
{
    public Vector2 jumpForce;
    public float walkSpeed;
    public TermiteAction[] actions;
    public Animator animator;

    Rigidbody2D body;
    float time = 0;
    bool isDead = false;
    int currentAction = -1;
    float currentActionStartTime;
    TermiteAction previousAction;
    bool landedOnPlatform = true;
    bool actionJustSwitched = false;
    bool orientedLeft = true;

    public enum ACTION { WALK_LEFT, WALK_RIGHT, JUMP_LEFT, JUMP_RIGHT, STAY };

    void Start()
    {
        body = GetComponent<Rigidbody2D>();

        NextAction();
    }
    
    void Update()
    {
        if (isDead)
            return;

        time += Time.deltaTime;

        if (Math.Round(body.velocity.y, 1) != 0)
        {
            animator.SetBool("isJumping", true);
        }
        else
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

        TermiteAction action = actions[currentAction];
        if((action.action == ACTION.STAY || action.action == ACTION.WALK_LEFT || action.action == ACTION.WALK_RIGHT) &&
            time - currentActionStartTime >= action.time)
        {
            NextAction();
            //return;
        }
    }

    private void FixedUpdate()
    {
        if (actions[currentAction].action == ACTION.WALK_LEFT)
        {
            if (actionJustSwitched == true)
            {
                //body.velocity = Vector2.zero;
                body.velocity = Vector2.left * walkSpeed;

                // flip
                if (orientedLeft == false)
                {
                    transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
                    orientedLeft = true;
                }
            }

            //body.AddForce(Vector2.left * walkSpeed);
        }
        else if (actions[currentAction].action == ACTION.WALK_RIGHT)
        {
            if (actionJustSwitched == true)
            {
                //body.velocity = Vector2.zero;
                body.velocity = Vector2.right * walkSpeed;

                // flip
                if (orientedLeft == true)
                {
                    transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
                    orientedLeft = false;
                }
            }

            //body.AddForce(Vector2.right * walkSpeed);
        }
        else if (actions[currentAction].action == ACTION.STAY)
        {
            if (actionJustSwitched == true)
                body.velocity = Vector2.zero;
        }
        else if (actions[currentAction].action == ACTION.JUMP_LEFT && Math.Round(body.velocity.y, 1) == 0 && landedOnPlatform == true)
        {
            if (actionJustSwitched == true)
            {
                body.velocity = Vector2.zero;

                // flip
                if (orientedLeft == false)
                {
                    transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
                    orientedLeft = true;
                }
            }

            body.AddForce(new Vector2(-jumpForce.x, jumpForce.y), ForceMode2D.Impulse);
            landedOnPlatform = false;
        }
        else if (actions[currentAction].action == ACTION.JUMP_RIGHT && Math.Round(body.velocity.y, 1) == 0 && landedOnPlatform == true)
        {
            if (actionJustSwitched == true)
            {
                body.velocity = Vector2.zero;

                // flip
                if (orientedLeft == true)
                {
                    transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
                    orientedLeft = false;
                }
            }

            body.AddForce(new Vector2(jumpForce.x, jumpForce.y), ForceMode2D.Impulse);
            landedOnPlatform = false;
        }

        actionJustSwitched = false;
    }
    void NextAction()
    {
        currentActionStartTime = time;
        actionJustSwitched = true;
        
        currentAction++;
        if (currentAction >= actions.Length)
        {
            currentAction = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDead)
            return;

        // hit from below
        if (collision.gameObject.tag == "Player")
        {
            Die();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            landedOnPlatform = true;

            if (actions[currentAction].action == ACTION.JUMP_LEFT || actions[currentAction].action == ACTION.JUMP_RIGHT)
            {
                body.velocity = Vector2.zero;

                NextAction();
            }
        }
        else if(collision.gameObject.tag == "Bullet")
        {
            Die();
        }
        else if (collision.gameObject.tag == "Player")
        {
            PlayerData.currentLifes--;
        }
    }

    void Die()
    {
        isDead = true;
        Destroy(gameObject, 2f);

        body.velocity = Vector2.zero;

        // death animation
        body.bodyType = RigidbodyType2D.Dynamic;
        body.constraints = RigidbodyConstraints2D.None;
        body.AddTorque(300);
        body.AddForce(Vector2.up * 900f);
        body.gravityScale = 5f;
        body.mass = 1f;

        Collider2D[] colliders = GetComponents<Collider2D>();
        foreach (Collider2D col in colliders)
        {
            col.isTrigger = true;
        }
    }

    [Serializable]
    public class TermiteAction
    {
        public ACTION action;
        public float time;
    }
}
