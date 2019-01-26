using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
 
    void Start()
    {
        Vector3 target = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z);
        target = Camera.main.ScreenToWorldPoint(target);
        Vector3 myPos = new Vector2(rb.position.x, rb.position.y + 1);
        Vector3 direction = target - myPos;
        direction.z = 0;
        direction.Normalize();
        rb.velocity = direction * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
