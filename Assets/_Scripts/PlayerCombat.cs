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
        if(isBow){

        }
        if(isStaff){

        }
    
    }

}
