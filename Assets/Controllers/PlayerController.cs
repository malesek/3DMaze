using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameInput _inputs;

    //Player
    private float _threshold = 0.001f;
    public float FallTimeout = 0.15f;
    private float _terminalVelocity = 53.0f;
    private float _verticalVelocity;
    private bool _grounded;
    private float runSpeed = 5f;
    private float gravity = -10f;

    //Camera
    public GameObject CmCamTarget;

    private float _cmTargetPitch;
    private float _rotationSpeed = 4.0f;
    private float _rotationVelocity;
    private float _minCamPitch = -90.0f;
    private float _maxCamPitch = 90.0f;

    //Others
    private float _fallTimeoutDelta;
    private CharacterController _characterController;
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _characterController = GetComponent<CharacterController>();
    }

    void Start()
    {
        Application.targetFrameRate = 144;
    }

    void Update()
    {
        CheckGround();
        Movement();
    }

    private void LateUpdate()
    {
        Look();
    }

    //Base
    private void Movement()
    {


        Vector3 moveDir = new Vector3(_inputs.GetMovementNormalized().x, 0f, _inputs.GetMovementNormalized().y).normalized;

        if (_inputs.GetMovementNormalized() != Vector2.zero)
        {
            moveDir = transform.right * _inputs.GetMovementNormalized().x + transform.forward * _inputs.GetMovementNormalized().y;
        }



        _characterController.Move(moveDir.normalized * (runSpeed * Time.deltaTime) +
                                  new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);
    }

    private void Look()
    {
        if (_inputs.GetLookVector().sqrMagnitude >= _threshold)
        {
            _cmTargetPitch += _inputs.GetLookVector().y * _rotationSpeed * 1;
            _rotationVelocity = _inputs.GetLookVector().x * _rotationSpeed * 1;

            _cmTargetPitch = ClampAngle(_cmTargetPitch, _minCamPitch, _maxCamPitch);

            CmCamTarget.transform.localRotation = Quaternion.Euler(_cmTargetPitch, 0.0f, 0.0f);
            transform.Rotate(Vector3.up * _rotationVelocity);
        }
    }

    //Calculations of movement
    private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }

    private void CheckGround()
    {
        _grounded = _characterController.isGrounded;
        if (_grounded)
        {
            //Reset the fall timeout timer
            _fallTimeoutDelta = FallTimeout;

            //Stop our velocity dropping infinitely when grounded
            if (_verticalVelocity < 0.0f)
            {
                _verticalVelocity = -2f;
            }
        }
        else
        {

            //Fall timeout
            if (_fallTimeoutDelta >= 0.0f)
            {
                _fallTimeoutDelta -= Time.deltaTime;
            }
        }

        // Apply gravity over time
        if (_verticalVelocity < _terminalVelocity)
        {
            _verticalVelocity += gravity * Time.deltaTime;
        }
    }
}