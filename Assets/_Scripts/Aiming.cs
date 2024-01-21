using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aiming : MonoBehaviour
{
    private Transform aimTransform;
    public GameObject arrow;
    public PlayerCombat playerCombat;
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
        if(playerCombat.isSword == true){
            aimTransform = transform.Find("SwordCursor");
            aimTransform.gameObject.SetActive(true);
        }
        if(playerCombat.isBow == true){
            aimTransform = transform.Find("BowCursor");
            aimTransform.gameObject.SetActive(true);
        }
        if(playerCombat.isStaff == true){
            aimTransform = transform.Find("StaffCursor");
            aimTransform.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = GetMouseWorldPosition();
        Vector3 aimDirection = (mousePosition-transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90;
        aimTransform.rotation = Quaternion.Euler(0,0,angle);

        if(Input.GetMouseButton(0)){
            if(playerCombat.isBow == true){
                aimTransform.GetComponent<Animator>().SetTrigger("Shoot");
            }   

        }
    }

    public void spawnArrow(){
        Instantiate(arrow, aimTransform.position, Quaternion.identity);
    }
}
