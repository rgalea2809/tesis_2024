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
    public XRGrabInteractable interactableReference = null;

    // Properties
    public float rotationAngle = 45.0f;
    private bool canRotate = false;


    private void Awake()
    {
        rotateReference.action.performed += RotateObject;
        interactableReference.hoverEntered.AddListener(EnableRotation);
        interactableReference.hoverExited.AddListener(DisableRotation);
    }

    private void OnDestroy()
    {
        rotateReference.action.performed -= RotateObject;
        interactableReference.hoverEntered.RemoveAllListeners();
        interactableReference.hoverExited.RemoveAllListeners();
    }

    private void EnableRotation(HoverEnterEventArgs arguments)
    {
        canRotate = true;
    }

    private void DisableRotation(HoverExitEventArgs arguments)
    {
        canRotate = false;
    }

    private void RotateObject(InputAction.CallbackContext context)
    {
        if (canRotate)
        {
            gameObject.transform.Rotate(0.0f, rotationAngle, 0.0f, Space.Self);
        }

    }
}