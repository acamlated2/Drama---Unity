using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameControllerScript : MonoBehaviour
{
    public PlayerInput _playerInput;
    
    private InputAction _rightTrigger;

    public InputActionReference _input;

    private void Awake()
    {
        
    }

    private void OnEnable()
    {
        //_rightTrigger = _input.
    }
}
