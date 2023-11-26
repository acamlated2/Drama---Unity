using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class ControlManagerScript : MonoBehaviour
{
    [SerializeField] private InputActionReference leftTrigger;
    private float _leftTriggerValue;
    [SerializeField] private InputActionReference rightTrigger;
    private float _rightTriggerValue;

    private void OnEnable()
    {
        leftTrigger.action.performed += ctx => _leftTriggerValue = ctx.ReadValue<float>();
        leftTrigger.action.canceled += ctx => _leftTriggerValue = 0;
        
        rightTrigger.action.performed += ctx => _rightTriggerValue = ctx.ReadValue<float>();
        rightTrigger.action.canceled += ctx => _rightTriggerValue = 0;
    }

    private void Update()
    {
        HandleInteractors();
    }

    private void HandleInteractors()
    {
        GameObject cameraOffset = transform.GetChild(0).gameObject;

        GameObject leftController = cameraOffset.transform.GetChild(1).gameObject;
        GameObject leftRayInteractor = leftController.transform.GetChild(0).gameObject;
        GameObject leftDirectInteractor = leftController.transform.GetChild(1).gameObject;
        
        GameObject rightController = cameraOffset.transform.GetChild(2).gameObject;
        GameObject rightRayInteractor = rightController.transform.GetChild(0).gameObject;
        GameObject rightDirectInteractor = rightController.transform.GetChild(1).gameObject;
        
        if (_leftTriggerValue > 0.5f)
        {
            leftRayInteractor.SetActive(true);
            leftDirectInteractor.SetActive(false);
        }
        else
        {
            leftRayInteractor.SetActive(false);
            leftDirectInteractor.SetActive(true);
        }
        
        if (_rightTriggerValue > 0.5f)
        {
            rightRayInteractor.SetActive(true);
            rightDirectInteractor.SetActive(false);
        }
        else
        {
            rightRayInteractor.SetActive(false);
            rightDirectInteractor.SetActive(true);
        }
    }
}
