using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Unity.VisualScripting;

public class RotateFocusedObject : MonoBehaviour
{
    // References
    public InputActionReference rotateReference = null;
    public InputActionReference destroyActionReference = null;
    public XRGrabInteractable interactableReference = null;

    // Properties
    public float rotationAngle = 45.0f;
    private bool canInteract = false;


    private void Awake()
    {
        rotateReference.action.performed += RotateObject;
        destroyActionReference.action.performed += DestroyObject;
        interactableReference.hoverEntered.AddListener(EnableInteraction);
        interactableReference.hoverExited.AddListener(DisableInteraction);
    }

    private void OnDestroy()
    {
        rotateReference.action.performed -= RotateObject;
        destroyActionReference.action.performed -= DestroyObject;
        interactableReference.hoverEntered.RemoveAllListeners();
        interactableReference.hoverExited.RemoveAllListeners();
    }

    private void EnableInteraction(HoverEnterEventArgs arguments)
    {
        canInteract = true;
    }

    private void DisableInteraction(HoverExitEventArgs arguments)
    {
        canInteract = false;
    }

    private void RotateObject(InputAction.CallbackContext context)
    {
        if (canInteract)
        {
            gameObject.transform.Rotate(0.0f, rotationAngle, 0.0f, Space.Self);
        }

    }

    private void DestroyObject(InputAction.CallbackContext context)
    {
        if (canInteract)
        {
            Destroy(gameObject);
        }

    }
}