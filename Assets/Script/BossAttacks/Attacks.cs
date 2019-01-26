using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacks : MonoBehaviour
{
    public ParticleSystem system;
    // Start is called before the first frame update

    private int currentAttackCounter;

    private int[] AttacksArray; 
    /*= {
        //1) platform 1 -> 3 Lasers;
        //2) ground mite wave;
        //3) platform 2 -> 4 Lasers;
        //4) deflect;
        //5) platform 3 -> 1 Lasers;
        //6) ground mite wave;
        //7) platform 4 -> 2 Lasers;
        //8) deflect;
    }*/

    void Start()
    {
        currentAttackCounter = 1;
    }

    void MiteWaveAttack()
    {
        var mwave = new MiteWaveBoss();
        mwave.transform.Rotate(0, 90, 0);

        currentAttackCounter++;
        //wait for 3 seconds and go to the next attack
    }

    void Deflect()
    {
        //...
        currentAttackCounter++;
    }

    void LaserEyesAttack(int startPoint, int endPoint)
    {
        //...
        currentAttackCounter++;
    }

    void switchAttack()
    {
        switch (currentAttackCounter)
        {
            case 1:
                LaserEyesAttack(1, 3);
                break;
            case 2:
                MiteWaveAttack();
                break;
            case 3:
                LaserEyesAttack(2, 4);
                break;
            case 4:
                Deflect();
                break;
            case 5:
                LaserEyesAttack(3, 1);
                break;
            case 6:
                MiteWaveAttack();
                break;
            case 7:
                LaserEyesAttack(4, 2);
                break;
            case 8:
                Deflect();
                break;
        }

        InvokeRepeating("switchAttack", 3.0f, 3.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
