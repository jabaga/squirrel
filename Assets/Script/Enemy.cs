using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool isMoving = false;
    public Vector2 movement;
    public float timeToReverse = 2f;

    float time = 0;
    float timeLastReversed = 0;
    Rigidbody2D body;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();


        if (isMoving)
            body.velocity = movement;
    }

    private void Update()
    {
        time += Time.deltaTime;

        if(isMoving)
        {
            if(time - timeLastReversed >= timeToReverse)
            {
                timeLastReversed = time;
                
                movement = new Vector2(-movement.x, -movement.y);

                // flip
                Vector2 newScale = transform.localScale;
                if (movement.x != body.velocity.x)
                    newScale.x = -newScale.x;

                transform.localScale = newScale;

                body.velocity = movement;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            PlayerData.currentLifes--;
        }
    }
}
