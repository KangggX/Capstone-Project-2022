using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 3;
    [SerializeField] private float _yOffset = 0.75f;
    private Transform _playerTransform;

    private float _mouseX;
    private float _mouseY;

    private float _xRotation;
    private float _yRotation;

    private void Start()
    {
        _playerTransform = Locator.GetPlayerTransform();
    }

    private void Update()
    {
        PlayerInput();
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(_playerTransform.position.x, _playerTransform.position.y + _yOffset, _playerTransform.position.z);
        transform.rotation = Quaternion.Euler(_xRotation, _yRotation, 0);
    }

    private void PlayerInput()
    {
        _mouseX = Input.GetAxisRaw("Mouse X");
        _mouseY = Input.GetAxisRaw("Mouse Y");

        _xRotation -= _mouseY * _rotationSpeed;
        _yRotation += _mouseX * _rotationSpeed;

        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);
    }
}
