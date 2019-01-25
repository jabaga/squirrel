using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Branch : MonoBehaviour
{
    public bool isTemporary = false;
    public bool isSwitchable = false;
    public float temporaryTime = 1f;
    public float switchTime = 3f;

    float time = 0;
    float timePlayerLanded = -1;
    float timeSwitched = 0;
    bool isDetached = false;
    Rigidbody2D body;
    new Collider2D collider;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
    }
    
    void Update()
    {
        if (isDetached == true)
            return;

        time += Time.deltaTime;

        if(isTemporary)
        {
            if (time - timePlayerLanded >= temporaryTime)
            {
                Detach();
            }
        }

        if(isSwitchable)
        {
            if(time - timeSwitched >= switchTime)
            {
                switchTime = time;
                gameObject.SetActive(!gameObject.activeSelf);
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
