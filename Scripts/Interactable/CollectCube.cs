using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class CollectCube : Interactable
{
    public GameObject particle;
    protected override void Interact()
    {
        Destroy(gameObject); 
        Instantiate(particle, transform.position, UnityEngine.Quaternion.identity);
    }
}
