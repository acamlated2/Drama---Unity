using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameControllerScript : MonoBehaviour
{
    [SerializeField] private InputActionReference XButton;
    private float XValue;
    [SerializeField] private InputActionReference YButton;
    private float YValue;
    [SerializeField] private InputActionReference AButton;
    private float AValue;
    [SerializeField] private InputActionReference BButton;
    private float BValue;

    private GameObject[] camPositions;
    private int _cameraCounter;

    private GameObject _player;

    private bool _switching;

    private void OnEnable()
    {
        XButton.action.performed += ScrollCameraDown;
        YButton.action.performed += ScrollCameraUp;
        AButton.action.performed += ScrollCameraDown;
        BButton.action.performed += ScrollCameraUp;
    }

    private void Awake()
    {
        camPositions = GameObject.FindGameObjectsWithTag("CameraPosition");

        _player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        
    }

    private void ScrollCameraUp(InputAction.CallbackContext ctx)
    {
        _cameraCounter += 1;

        if (_cameraCounter > camPositions.Length - 1)
        {
            _cameraCounter = 0;
        }
        
        TeleportCamera();
    }
    
    private void ScrollCameraDown(InputAction.CallbackContext ctx)
    {
        _cameraCounter -= 1;

        if (_cameraCounter < 0)
        {
            _cameraCounter = camPositions.Length - 1;
        }
        
        TeleportCamera();
    }

    private void TeleportCamera()
    {
        _player.transform.position = camPositions[_cameraCounter].transform.position;
        _player.transform.rotation = camPositions[_cameraCounter].transform.rotation;
    }
}
