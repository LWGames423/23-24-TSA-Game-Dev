using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpriteFlipper : MonoBehaviour
{
    public Transform playerTransform;
    public List<ParticleSystem> particleSystems;

    void Start()
    {
        playerTransform = GetComponentInParent<PlayerManager>().transform;
        foreach(Transform child in transform)
        {
            if(child.GetComponent<ParticleSystem>() != null)
            {
                ParticleSystem childSystem = child.GetComponent<ParticleSystem>();
                particleSystems.Add(childSystem);
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        if(playerTransform.localScale.x < 0)
        {
            foreach(ParticleSystem p in particleSystems)
            {
                Transform particleTransform = p.transform;
                particleTransform.rotation = Quaternion.Euler(particleTransform.rotation.x, 180, particleTransform.rotation.z);
            }
        }
        if (playerTransform.localScale.x > 0)
        {
            foreach (ParticleSystem p in particleSystems)
            {
                Transform particleTransform = p.transform;
                particleTransform.rotation = Quaternion.Euler(particleTransform.rotation.x, 0, particleTransform.rotation.z);
            }
        }
    }
}
