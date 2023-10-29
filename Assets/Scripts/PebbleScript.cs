using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PebbleScript : MonoBehaviour
{
    private bool _groundHit;

    private GameObject _medea;

    private void Awake()
    {
        _medea = GameObject.FindGameObjectWithTag("Medea");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            if (!_groundHit)
            {
                LureMedea();
            }
            
            _groundHit = true;
        }
    }

    public void LureMedea()
    {
        _medea.GetComponent<PlayerController>().Lure(transform.position);
    }

    public void Grabbed()
    {
        Debug.Log("falsened");
        _groundHit = false;
    }
}
