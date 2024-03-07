using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public ParticleSystem disablerParticles;

    public bool hasDisabler;

    void Update(){
        if(Input.GetKeyDown(KeyCode.X)){
            if(hasDisabler){
                disablerParticles.gameObject.SetActive(true);
                disablerParticles.Play();
            }
        }
    }


}
