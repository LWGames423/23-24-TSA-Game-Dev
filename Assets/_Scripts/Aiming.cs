using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aiming : MonoBehaviour
{
    private Transform aimTransform;
    public SpriteRenderer bowRenderer;

    public static Vector3 GetMouseWorldPosition(){
        Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        return vec;
    }
    public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera){
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;

    }

    // Start is called before the first frame update
    void Awake()
    {
        aimTransform = transform.Find("ArrowCursor");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = GetMouseWorldPosition();
        Vector3 aimDirection = (mousePosition-transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90;
        aimTransform.eulerAngles = new Vector3(0,0,angle);
        if(Input.GetKey(KeyCode.B)){
            bowRenderer.enabled = true;
        }
        if(!Input.GetKey(KeyCode.B)){
            bowRenderer.enabled = false;
        }
    }
}
