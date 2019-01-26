using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public bool isLife = false;
    public bool isBullet = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print(collision.gameObject.name);
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);

            if (isLife)
                PlayerData.currentLifes++;
            if (isBullet)
                PlayerData.currentBullets++;
        }
    }
}
