using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestProblemNumGenerator : MonoBehaviour
{

    System.Random rnd = new System.Random();
    public int problemNum;

    // Start is called before the first frame update
    void Awake()
    {
        problemNum = rnd.Next(0, 6);
    }

}
