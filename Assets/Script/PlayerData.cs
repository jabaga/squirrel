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

            if (value < _currentLifes)
            {
                AnimationHelper.Instance.Blink(Main.player, Color.red, 0.7f);

                SpriteRenderer sr = Camera.main.transform.GetChild(0).GetComponent<SpriteRenderer>();
                sr.color = new Color(1,0,0,0.5f);
                AnimationHelper.Instance.FadeOut(Camera.main.transform.GetChild(0).gameObject, .2f);
                
            }

            _currentLifes = value;

            if (_currentLifes == 0)
            {
                // TODO die
                Main.GameOver();
            }
            else {
                Main.UpdateUIData();
            }
        }
    }

    public static int currentBullets
    {
        get { return _currentBullets; }
        set
        {
            if (value > _maxBullets)
                value = _maxBullets;
            if (value < 0)
                value = 0;

            _currentBullets = value;
            Main.UpdateUIData();
        }

    }

    public static int _maxLifes = 20;
    public static int _maxBullets = 500;
    static int _currentLifes = 5;
    static int _currentBullets = 0;
}
