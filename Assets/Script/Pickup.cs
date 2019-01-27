using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public bool isLife = false;
    public bool isBullet = true;

    bool isDestroyed = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDestroyed)
            return;
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject, 1f);
            GetComponent<SpriteRenderer>().enabled = false;
            isDestroyed = true;

            if (isLife)
                PlayerData.currentLifes++;
            if (isBullet)
                PlayerData.currentBullets += 2;

            GetComponent<AudioSource>().Play();
        }
    }
}
