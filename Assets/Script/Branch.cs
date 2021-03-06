﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Branch : MonoBehaviour
{
    [Header("Temporary")]
    public bool isTemporary = false;
    public float temporaryTime = 1f;
    public ParticleSystem temporaryParticle;
    [Header("Switchable")]
    public bool isSwitchable = false;
    public float switchTime = 3f;
    public bool reversed = false;
    public ParticleSystem switchableParticle;

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

        if (isTemporary)
            temporaryParticle.Play();
        if (isSwitchable)
        {
            switchableParticle.Play();
            if(reversed)
            {
                isActive = false;
                collider.enabled = isActive;
                spriteRenderer.enabled = isActive;
            }

        }
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
                GetComponent<AudioSource>().Play();
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

        temporaryParticle.Stop();

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
