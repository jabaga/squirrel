using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guns : MonoBehaviour
{
    public Transform GunPoint;
    public Transform Bullet;
    public double fireRate = 0.5;
    public GameObject particlePrefab;
    public ToggleBadass toggleBadass;

    double lastShot = 0.0;

    void Update()
    {
        if (Input.GetButton("Fire1") && PlayerData.currentBullets > 0 && toggleBadass.badassMode == true) {
            Shoot();
        }
    }

    void Shoot() {
        if (Time.time > fireRate + lastShot)
        {
            lastShot = Time.time;

            Collider2D bullet = Instantiate(Bullet, GunPoint.position, GunPoint.rotation).GetComponent<Collider2D>();
            Destroy(Instantiate(particlePrefab, GunPoint.position, Quaternion.Euler(0,0,-90f)), 5f);

            Physics2D.IgnoreCollision(bullet, Main.player.GetComponent<Collider2D>());

            PlayerData.currentBullets--;

            Destroy(bullet.gameObject, 6f);
        }
    }
}
