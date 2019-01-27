using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiteWaveMovement : MonoBehaviour
{
    public ParticleSystem miteWave;
    //public List<ParticleCollisionEvent> collisionEvents;
    //keeping a list of the events so that they can eventually hide/destroy enemies
    public bool instantKill = true;
    public Vector2 movement;

    Rigidbody2D waveBody;

    private void Start()
    {
        miteWave = GetComponent<ParticleSystem>();
        //collisionEvents = new List<ParticleCollisionEvent>();
    }

    void Update()
    {
        movement = new Vector2(movement.x, 0);
        // do not check for minimum speed, the wave is not always preserved on screen
        miteWave.transform.Translate(movement);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (instantKill == true)
                Main.GameOver();
            else
                PlayerData.currentLifes--;
        }
    }

}
