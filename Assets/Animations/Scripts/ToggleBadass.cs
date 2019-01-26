using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ToggleBadass : MonoBehaviour
{
    public GameObject[] badassObjects;
    public bool badassMode;

    bool isBadass;

    private void Start()
    {
        isBadass = badassMode;
    }

    void Update()
    {
        if (badassMode == true && isBadass == false)
        {
            isBadass = true;
            foreach (GameObject badassItem in badassObjects)
            {
                badassItem.SetActive(true);
            }
        }

        if (badassMode == false && isBadass == true)
        {
            isBadass = false;
            foreach (GameObject badassItem in badassObjects)
            {
                badassItem.SetActive(false);
            }
        }
    }
}
