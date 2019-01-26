using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guns : MonoBehaviour
{
    public Transform GunPoint;
    public Transform Bullet;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) {

            Shoot();
        }
    }

    void Shoot() {
        Instantiate(Bullet, GunPoint.position, GunPoint.rotation);
    }
}
