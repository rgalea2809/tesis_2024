using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FurnitureCanvasHandler : MonoBehaviour
{
    Camera cameraToLookAt;
    // References
    public XRSocketInteractor socketReference = null;
    public Canvas canvasReference = null;

    // Use this for initialization 
    void Start()
    {
        cameraToLookAt = Camera.main;
        socketReference.selectEntered.AddListener(OnItemSelected);
        socketReference.selectExited.AddListener(OnItemRemoved);

    }

    // Update is called once per frame 
    void LateUpdate()
    {
        transform.LookAt(cameraToLookAt.transform);
        transform.rotation = Quaternion.LookRotation(cameraToLookAt.transform.forward);
    }

    void OnItemSelected(SelectEnterEventArgs args)
    {
        canvasReference.enabled = false;
    }

    void OnItemRemoved(SelectExitEventArgs args)
    {
        canvasReference.enabled = true;
    }
}