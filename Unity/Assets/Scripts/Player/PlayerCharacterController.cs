using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerCharacterController : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField] private float _movementSpeed = 20;
    [SerializeField] private float _jumpStrength = 2;
    [SerializeField] private float _rbDrag = 1;
    [SerializeField] private KeyCode _jumpKey = KeyCode.Space;
    private float _horizontalMovement;
    private float _verticalMovement;

    [Header("Player Rotation")]
    [SerializeField] private float _rotationSpeed = 3;
    private float _mouseX;
    private float _yRotation;

    [Header("Ground Detection")]
    [SerializeField] private float _groundDetectionRadius = 0.1f;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _layerMask;
    private RaycastHit _hitGround;
    private bool _isGrounded;

    private Vector3 _moveDirection;
    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;
    }

    private void Update()
    {
        PlayerInput();
        ControlDrag();
        GroundCheck();

        // If player is grounded and is pressing/holding the Jump Key
        if (_isGrounded && Input.GetKey(_jumpKey))
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
        RotatePlayer();
    }

    // Player Keyboard and Mouse input
    private void PlayerInput()
    {
        // Keyboard Inputs
        _horizontalMovement = Input.GetAxisRaw("Horizontal");
        _verticalMovement = Input.GetAxisRaw("Vertical");
        _moveDirection = (transform.forward * _verticalMovement) + (transform.right * _horizontalMovement);

        // Mouse Inputs
        _mouseX = Input.GetAxisRaw("Mouse X");
        _yRotation += _mouseX * _rotationSpeed;
    }

    // Control the Rigidbody Drag, the higher the value, the least likely the player will slide
    private void ControlDrag()
    {
        _rb.drag = _rbDrag;
    }

    // Check if player is colliding with the ground or not
    private void GroundCheck()
    {
        _isGrounded = Physics.CheckSphere(_groundCheck.position, _groundDetectionRadius, _layerMask);
    }

    // Move player towards WASD input
    private void MovePlayer()
    {
        _rb.AddForce(_moveDirection.normalized * _movementSpeed, ForceMode.Acceleration);
    }

    // Rotate player Y-axis based on horizontal mouse movement
    private void RotatePlayer()
    {
        transform.rotation = Quaternion.Euler(0, _yRotation, 0);
    }

    // Jump!!!
    private void Jump()
    {
        _rb.AddForce(transform.up * _jumpStrength, ForceMode.Impulse);
    }

    // Gizmos for Scene View illustration
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(_groundCheck.position, _groundDetectionRadius);
    }
}
