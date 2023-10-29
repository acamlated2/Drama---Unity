using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishColliderScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Medea"))
        {
            Debug.Log("Medea lured");
        }
    }
}
