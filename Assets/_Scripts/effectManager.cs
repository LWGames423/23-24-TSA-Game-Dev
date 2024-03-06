using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effectManager : MonoBehaviour
{
    public PlayerManager pm;

    private float _health = 0f;
    // Start is called before the first frame update
    void Start()
    {
        _health = pm.health;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
