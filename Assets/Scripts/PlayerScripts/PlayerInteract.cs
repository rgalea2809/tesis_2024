using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private Camera cam;
    
    private bool isInView;
    [SerializeField]
    private float viewDistance = 3f;
    [SerializeField]
    private LayerMask mask;
    private PlayerUI playerUI;

    private InputManager inputManager;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<PlayerLook>().cam;
        playerUI = GetComponent<PlayerUI>();
        inputManager = GetComponent<InputManager>();
    }

    // Update is called once per frame
    void Update()
    {
       
        playerUI.UpdateText(string.Empty);
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(ray.origin,ray.direction* viewDistance);
        RaycastHit hitInfo;
        isInView = Physics.Raycast(ray, out hitInfo, viewDistance,mask);
        if(isInView)
        {
            if(hitInfo.collider.GetComponent<Interactable>() != null)
            {
                Interactable objectInteracted = hitInfo.collider.GetComponent<Interactable>();
                playerUI.UpdateText(objectInteracted.promptMessage);
                if(inputManager.getbasicControls().Interact.triggered) 
                {
                    objectInteracted.BaseInteract();
                }
                if(inputManager.getbasicControls().Push.IsPressed()){
                    objectInteracted.BasePush(inputManager.getbasicControls().Movement.ReadValue<Vector2>(),transform.eulerAngles.y);
                    if(inputManager.getbasicControls().Rotate.WasPressedThisFrame()){
                        objectInteracted.transform.Rotate(0,90,0);
                    }
                }
                if(inputManager.getbasicControls().Delete.IsPressed()){
                    Destroy(hitInfo.collider.gameObject);
                }
            }
        }
    }
}
