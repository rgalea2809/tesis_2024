using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class XRInputManager : MonoBehaviour
{
    [SerializeField] private UIXRControler uiControler;
    [SerializeField] private SpawnFurniture spawnFunc;
    [SerializeField] private XRRayInteractor rigthRay;
    // Start is called before the first frame update
    private GameObject selectedObj;
    public GameObject bedroomSpawner;
    public GameObject bedroomTrainning;
    public GameObject livingRoomSpawner;
    public GameObject livingRoomTrainning;

    private XRInputManager xrInputs;
    private PlayerInput playerInput;
    private PlayerInput.XRInputsActions basicControls;
    // Start is called before the first frame update
    void Awake()
    {
        playerInput = new PlayerInput();
        basicControls = playerInput.XRInputs;
        Debug.Log(basicControls);
        basicControls.Pause.performed += ctx => RequestPauseMenu();
        basicControls.Catalog.performed += ctx => OnOpenCatalog();
        basicControls.Cancel.performed += ctx => spawnFunc.hidePreview();
        basicControls.Confirm.performed += ctx => spawnFunc.setInPlace();
    }

    private void RequestPauseMenu()
    {
        if (uiControler.isGameStarted && !uiControler.isCatalogOpen && rigthRay.firstInteractableSelected == null)
        {
            uiControler.TooglePauseMenu(true);
        }
    }

    private void OnOpenCatalog()
    {
        if (uiControler.isGameStarted && !uiControler.isPaused && rigthRay.firstInteractableSelected == null)
        {
            uiControler.ToogleCatalogMenu(true);
            spawnFunc.hidePreview();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(rigthRay.isSelectActive && rigthRay.firstInteractableSelected != null)
        {
            Vector3 moveY = Vector3.zero;

            selectedObj =  rigthRay.firstInteractableSelected.colliders[0].gameObject;

            unfreezePosition();

            selectedObj.GetComponent<SpacingValidation>().CheckDistanseFromWalls();

            if(basicControls.Pause.IsPressed()){
                Debug.Log( selectedObj + " should go up by " + (selectedObj.transform.position.y + 0.1f) );
                selectedObj.GetComponent<Rigidbody>().MovePosition( 
                    new Vector3(selectedObj.transform.position.x,selectedObj.transform.position.y + 0.1f,selectedObj.transform.position.x));
            }
            else if(basicControls.Catalog.IsPressed()){ 
                Debug.Log( selectedObj + " should go down by " + (selectedObj.transform.position.y - 0.1f) );
                selectedObj.GetComponent<Rigidbody>().MovePosition( 
                    new Vector3(selectedObj.transform.position.x,selectedObj.transform.position.y - 0.1f,selectedObj.transform.position.x));

            }
        }
        else if(!rigthRay.isSelectActive && selectedObj != null){
            freezePosition();
        }
    }

    private void OnEnable()
    {
        basicControls.Enable();
    }

    private void OnDisable()
    {
        basicControls.Disable();
    }

    public void RequestTeleportToPlayArea()
    {
        if (uiControler.didSelectLivingRoomType)
        {
            gameObject.transform.position = livingRoomSpawner.transform.position;
            livingRoomTrainning.SetActive(!uiControler.didSelectFreeMode);
        }
        else
        {
            gameObject.transform.position = bedroomSpawner.transform.position;
            bedroomTrainning.SetActive(!uiControler.didSelectFreeMode);
        }
    }

    private void freezePosition(){
        selectedObj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    }

    private void unfreezePosition(){
        selectedObj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        selectedObj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
        selectedObj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationZ;
        selectedObj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX;
        selectedObj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationY;  
        selectedObj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
    }
}
