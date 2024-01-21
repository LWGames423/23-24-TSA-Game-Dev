using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowFunctions : MonoBehaviour
{
    // Start is called before the first frame update
    public Aiming aiming;
    void Shoot()
    {
        aiming.spawnArrow();
    }
}
