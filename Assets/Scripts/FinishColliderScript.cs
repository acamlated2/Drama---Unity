using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishColliderScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Medea"))
        {
            SceneManager.LoadScene("Go To Cauldron", LoadSceneMode.Single);
        }
    }
}
