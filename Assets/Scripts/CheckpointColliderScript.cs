using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointColliderScript : MonoBehaviour
{
    public bool collided;
    
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Medea"))
        {
            collided = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Medea"))
        {
            collided = false;
        }
    }
}
