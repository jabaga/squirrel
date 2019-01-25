using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static int currentLifes
    {
        get { return _currentLifes; }
        set {
            if (value > _maxLifes)
                value = _maxLifes;
            if (value < 0)
                value = 0;

            _currentLifes = value;

            if(_currentLifes == 0)
            {
                // TODO die
            }
        }
    }

    public static int currentBullets
    {
        get { return _currentLifes; }
        set
        {
            if (value > _maxBullets)
                value = _maxBullets;
            if (value < 0)
                value = 0;

            _currentLifes = value;
        }
    }

    static int _maxLifes = 8;
    static int _maxBullets = 50;
    static int _currentLifes = 5;
    static int _currentBullets = 0;

}
