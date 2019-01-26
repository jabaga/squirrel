using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserEyesBoss : MonoBehaviour
{
    public ParticleSystem laserBeam;
    //public List<ParticleCollisionEvent> collisionEvents;
    //keeping a list of the events so that they can eventually hide/destroy enemies

    public Vector2 movement;

    // Start is called before the first frame update
    private void Start()
    {
        laserBeam = GetComponent<ParticleSystem>();
        //boss.AddComponent(miteWave);
    }

    void Update()
    {
        movement = new Vector2(movement.x, 0);
        laserBeam.transform.Translate(movement);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerData.currentLifes--;
        }
    }
}
