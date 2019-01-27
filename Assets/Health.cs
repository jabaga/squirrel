using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Slider myHealthBar;
    // Start is called before the first frame update
    void Start()
    {
        myHealthBar.value = 10;
    }

    void BossHit()
    {
        myHealthBar.value -= 1;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
