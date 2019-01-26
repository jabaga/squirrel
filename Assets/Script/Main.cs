using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : Singleton<Main>
{
    public Canvas canvas;
    public GameObject player;

    // (Optional) Prevent non-singleton constructor use.
    protected Main() { }

}
