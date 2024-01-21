using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator swordAnimator, bowAnimator, staffAnimator;
    public bool isSword, isBow, isStaff;

    void OnFire()
    {
        if(isSword){
            swordAnimator.SetTrigger("Swipe");
        }
        if(isStaff){
            staffAnimator.SetTrigger("Swipe");
        }
    
    }

    void Update(){
        if(Input.GetMouseButtonDown(1)){
            if(isSword){
                swordAnimator.SetTrigger("Stab");
            }
            if(isStaff){
                staffAnimator.SetTrigger("Beam");
            }
        }
    }


}
