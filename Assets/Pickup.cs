using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public bool isLife = false;
    public bool isBullet = true;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if(isLife)
                PlayerData.currentLifes++;
            if (isBullet)
                PlayerData.currentBullets++;

            Destroy(gameObject);
        }
    }
}
