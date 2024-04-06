using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashWall : MonoBehaviour
{
    public EdgeCollider2D leftCollider;
    public EdgeCollider2D rightCollider;

    private float _dWallSize;

    private float _dir;
    
    public PlayerMovement pm;
    
    
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _dir = pm.GetDash().x;
    }
}
