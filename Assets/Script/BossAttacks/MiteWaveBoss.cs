using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiteWaveBoss : MonoBehaviour
{
    public ParticleSystem miteWave;
    //public List<ParticleCollisionEvent> collisionEvents;
    //keeping a list of the events so that they can eventually hide/destroy enemies

    public Vector2 movement;

    Rigidbody2D waveBody;

    // Start is called before the first frame update
    private void Start()
    {
        miteWave = GetComponent<ParticleSystem>();
        //boss.AddComponent(miteWave);
    }

    void Update()
    {
        movement = new Vector2(movement.x, 0);
        miteWave.transform.Translate(movement);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerData.currentLifes--;
        }
    }
}
