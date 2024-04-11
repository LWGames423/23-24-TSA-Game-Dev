using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class MovingPlatformScript : MonoBehaviour
{
    public Transform[] waypoints;
    public float minRadius = 0.003f;
    public float movementSpeed = 0.05f;
    public bool automatic = false;

    private int currentID = 0;
    private int nextID = 1;
    private bool reversed = false;

    private Vector3 distance;       

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame  
    void FixedUpdate()
    {
        distance = waypoints[currentID].position - transform.position;

        if (automatic)
        {
            transform.position += distance.normalized * movementSpeed;
        }

        if (Mathf.Sqrt(Mathf.Pow(waypoints[currentID].position.x - transform.position.x, 2.0f) + Mathf.Pow(waypoints[currentID].position.y - transform.position.y, 2.0f)) <= movementSpeed)
        {
            transform.position = waypoints[currentID].position;
            if (!reversed)
            {
                if (currentID < waypoints.Length - 1)
                {
                    currentID++;
                }
                else if (currentID == waypoints.Length - 1)
                {
                    reversed = true;
                    currentID--;
                }
            }
            else if (reversed)
            {
                if (currentID > 0)
                {
                    currentID--;
                }
                else if (currentID == 0)
                {
                    reversed = false;
                    currentID++;
                }
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.gameObject.tag == "Player" && !automatic)
        {
            transform.position += distance.normalized * movementSpeed;
        }
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.root.gameObject.tag == "Player")
        {
            collision.transform.root.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.SetParent(null);
        }
    }
}
