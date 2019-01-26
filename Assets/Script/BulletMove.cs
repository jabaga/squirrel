using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
 
    // Start is called before the first frame update

    void Start()

    {
        Vector2 target = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        target = Camera.main.ScreenToWorldPoint(target);
        Vector2 myPos = new Vector2(rb.position.x, rb.position.y + 1);
        Vector2 direction = target - myPos;
        direction.Normalize();
        rb.velocity = direction * speed;
    }


}
