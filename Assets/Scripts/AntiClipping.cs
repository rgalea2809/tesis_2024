using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class AntiClipping : MonoBehaviour
{
    [SerializeField]
    private XRRayInteractor rigthRay;
    // Start is called before the first frame update
    private GameObject selectedObj;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(rigthRay.isSelectActive && rigthRay.firstInteractableSelected != null)
        {
            selectedObj =  rigthRay.firstInteractableSelected.colliders[0].gameObject;
            Debug.Log(selectedObj.transform.position.x + " " + selectedObj.transform.position.y + " " + selectedObj.transform.position.z);
            selectedObj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            selectedObj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
            selectedObj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationZ;
            selectedObj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX;
            selectedObj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationY;
            selectedObj.transform.localPosition = new Vector3(rigthRay.transform.position.x,rigthRay.transform.position.y,rigthRay.transform.position.z);
                
        }
        else if(!rigthRay.isSelectActive && selectedObj != null){
            selectedObj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
    }
}
