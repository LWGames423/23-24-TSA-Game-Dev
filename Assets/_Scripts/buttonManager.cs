using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class buttonManager : MonoBehaviour
{
    public GameObject door;
    [FormerlySerializedAs("toggled")] public bool closed = true;

    private void Update()
    {
        door.SetActive(closed);
    }

    public void Toggle()
    {
        closed = !closed;
    }
}
