using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SocketHandler : MonoBehaviour
{
    // References
    public XRSocketInteractor socketReference = null;
    public MeshRenderer meshReference = null;

    // Start is called before the first frame update
    void Start()
    {
        socketReference.selectEntered.AddListener(OnItemSelected);
        socketReference.selectExited.AddListener(OnItemRemoved);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnItemSelected(SelectEnterEventArgs args)
    {
        meshReference.enabled = false;
    }

    void OnItemRemoved(SelectExitEventArgs args)
    {
        meshReference.enabled = true;
    }
}
