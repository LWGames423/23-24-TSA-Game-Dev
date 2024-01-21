using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator swordAnimator, bowAnimator, staffAnimator;
    public bool isSword, isBow, isStaff, canStab;

    void OnFire()
    {
        if(isSword){
            swordAnimator.SetTrigger("Swipe");
        }
        if(isBow){

        }
        if(isStaff){

        }
    
    }

    void Update(){
        if(Input.GetMouseButtonDown(1)){
            if(isSword){
                swordAnimator.SetTrigger("Stab");
            }
            if(isBow){

            }
            if(isStaff){

            }
        }
    }


}
