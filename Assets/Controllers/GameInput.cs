using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    Inputs playerInputs;
    
    // Start is called before the first frame update
    // Awake is called before Start
    void Awake()
    {
        playerInputs = new();
        playerInputs.Enable();
    }

    public Vector2 GetLookVector()
    {
        Vector2 inputVector = playerInputs.Player.Camera.ReadValue<Vector2>();

        return inputVector;
    }

    public Vector2 GetMovementNormalized()
    {
        Vector2 inputVector = playerInputs.Player.Movement.ReadValue<Vector2>();

        inputVector = inputVector.normalized;

        return inputVector;
    }
}
