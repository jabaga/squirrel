using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guns : MonoBehaviour
{
    public Transform GunPoint;
   
    public Transform Bullet;
    public double fireRate = 0.5;
    public double lastShot = 0.0;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {


            Shoot();
        }
    }

    void Shoot()
    {
        if (Time.time > fireRate + lastShot)
        {

            Instantiate(Bullet, GunPoint.position, GunPoint.rotation);
            lastShot = Time.time;
        }
    }
}

