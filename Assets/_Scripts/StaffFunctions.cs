using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffFunctions : MonoBehaviour
{
    public Aiming aiming;
    void SendWave()
    {
        aiming.spawnWave();
    }
}
