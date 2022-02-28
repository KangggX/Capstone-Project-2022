using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Raycast Settings")]
    [SerializeField] private float _raycastLength;
    private RaycastHit _raycastHit;
    private bool _canInteract;

    private GameObject _camera;

    KeyCode _interactKey = KeyCode.E;

    private void Start()
    {
        _camera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    private void Update()
    {
        PlayerInput(out _canInteract);
        Raycast();
    }

    private void PlayerInput(out bool Result)
    {
        Result = false;

        if (Input.GetKeyDown(_interactKey))
        {
            Result = true;
        }
    }

    private void Raycast()
    {
        bool raycast = Physics.Raycast(_camera.transform.position, _camera.transform.forward, out _raycastHit, _raycastLength);
        Debug.DrawRay(_camera.transform.position, _camera.transform.forward * _raycastLength, Color.green);

        if (raycast && _canInteract)
        {
            IInteractable interactable = _raycastHit.collider.GetComponent<IInteractable>();

            if (interactable != null)
            {
                interactable.TriggerInteraction();
            }
        }
    }
}
