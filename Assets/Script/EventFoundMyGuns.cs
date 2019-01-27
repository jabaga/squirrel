using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventFoundMyGuns : MonoBehaviour
{
    public GameObject gunsHolder;
    public ToggleBadass toggleBadass;
    public GameObject particleToSpawn;

    bool activated = false;

    void Start()
    {
        
    }
    
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (activated)
            return;

        if(collision.gameObject.tag == "Player")
        {
            activated = true;

            Camera.main.GetComponent<CameraManager>().SetZoom(7, 0.7f);

            Main.Instance.player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

            Invoke("Badass", 1f);
            Invoke("Badass2", 4.2f);
            Invoke("Badass3", 4.5f);
        }
    }

    void Badass()
    {
        Destroy(gunsHolder);
        toggleBadass.badassMode = true;

        Destroy(Instantiate(particleToSpawn, Main.Instance.player.transform.position, Quaternion.identity), 3f);
    }

    void Badass2()
    {
        Camera.main.GetComponent<CameraManager>().SetZoom(20, 1);
    }

    void Badass3()
    {
        Main.Instance.player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }
}
