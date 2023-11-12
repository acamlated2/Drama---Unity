using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class ControlManagerScript : MonoBehaviour
{
    [SerializeField] private InputActionReference rightTrigger;
    private float _rightTriggerValue;

    private void OnEnable()
    {
        rightTrigger.action.performed += ctx => _rightTriggerValue = ctx.ReadValue<float>();
        rightTrigger.action.canceled += ctx => _rightTriggerValue = 0;
    }

    private void Update()
    {
        GameObject cameraOffset = transform.GetChild(0).gameObject;
        GameObject rightController = cameraOffset.transform.GetChild(2).gameObject;
        GameObject rayInteractor = rightController.transform.GetChild(0).gameObject;
        GameObject directInteractor = rightController.transform.GetChild(1).gameObject;
        
        if (_rightTriggerValue > 0.5f)
        {
            rayInteractor.SetActive(true);
            directInteractor.SetActive(false);
        }
        else
        {
            rayInteractor.SetActive(false);
            directInteractor.SetActive(true);
        }
    }
}
