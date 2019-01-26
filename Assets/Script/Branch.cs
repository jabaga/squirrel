using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Branch : MonoBehaviour
{
    public bool isTemporary = false;
    public float temporaryTime = 1f;
    public bool isSwitchable = false;
    public float switchTime = 3f;

    float time = 0;
    float timePlayerLanded = -1;
    float timeSwitched = 0;
    bool isDetached = false;
    Rigidbody2D body;
    new Collider2D collider;
    SpriteRenderer spriteRenderer;
    bool isActive = true;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    void Update()
    {
        if (isDetached == true)
            return;

        time += Time.deltaTime;

        if(isTemporary)
        {
            if (timePlayerLanded > 0 && time - timePlayerLanded >= temporaryTime)
            {
                Detach();
            }
        }

        if(isSwitchable)
        {
            if(time - timeSwitched >= switchTime)
            {
                timeSwitched = time;
                isActive = !isActive;

                collider.enabled = isActive;
                spriteRenderer.enabled = isActive;
            }
        }
    }

    void Detach()
    {
        isDetached = true;

        body.bodyType = RigidbodyType2D.Dynamic;
        collider.isTrigger = true;

        Destroy(gameObject, 5f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player" && isTemporary == true)
        {
            timePlayerLanded = time;
        }
    }
}
