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
