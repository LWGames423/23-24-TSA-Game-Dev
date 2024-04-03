using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashWall : MonoBehaviour
{

    private BoxCollider2D bc;
    public PlayerMovement pm;

    void OnCollisionEnter(Collision c)
    {
        if(pm.pubIsDashing == true && c.gameObject.tag == "Player")
        {
            // Physics.IgnoreCollision(bc, c.gameObject, true);
            //Need to extend dash's time, can someone who did movement help here?
            // pm.dashTime
        }
    }

    void OnCollisionExit(Collision c)
    {
        if(pm.pubIsDashing == true && c.gameObject.tag == "Player")
        {
            // Physics.IgnoreCollision(bc, c.gameObject.BoxCollider2D, false);
            //More help here?
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
