using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class XRInputManager : MonoBehaviour
{
    [SerializeField] private UIXRControler uiControler;
    [SerializeField] private SpawnFurniture spawnFunc;
    [SerializeField] private XRRayInteractor rigthRay;

    [SerializeField] private Rigidbody unFreezeConstraints;

    [SerializeField] private TextMeshProUGUI xControls;
    [SerializeField] private TextMeshProUGUI yControls;
    [SerializeField] private TextMeshProUGUI aControls;
    [SerializeField] private TextMeshProUGUI bControls;
    [SerializeField] private TextMeshProUGUI ltControls;

    [SerializeField] private LineRenderer TLine;
    [SerializeField] private LineRenderer BLine;
    [SerializeField] private LineRenderer LLine;
    [SerializeField] private LineRenderer RLine;

    // Start is called before the first frame update
    private GameObject selectedObj;
    public GameObject lobbySpawner;
    public GameObject bedroomSpawner;
    public GameObject bedroomTrainning;
    public GameObject livingRoomSpawner;
    public GameObject livingRoomTrainning;

    private bool isDistanceUIActive = true;
    private PlayerInput playerInput;
    private PlayerInput.XRInputsActions basicControls;
    // Start is called before the first frame update
    void Awake()
    {
        playerInput = new PlayerInput();
        basicControls = playerInput.XRInputs;
        basicControls.Pause.performed += ctx => RequestPauseMenu();
        basicControls.Catalog.performed += ctx => OnOpenCatalog();
        basicControls.Cancel.performed += ctx => spawnFunc.hidePreview();
        basicControls.Confirm.performed += ctx => spawnFunc.setInPlace();
        basicControls.Rotate.performed += ctx => spawnFunc.rotateItem();
        basicControls.ToogleDistances.performed += ctx => isDistanceUIActive = !isDistanceUIActive;
    }

    private void RequestPauseMenu()
    {
        if (uiControler.isGameStarted && !uiControler.isCatalogOpen && rigthRay.firstInteractableSelected == null)
        {
            uiControler.TooglePauseMenu(true);
            uiControler.ToogleIsPaused(true);
        }
    }

    private void OnOpenCatalog()
    {
        if (uiControler.isGameStarted && !uiControler.isPaused && rigthRay.firstInteractableSelected == null)
        {
            uiControler.ToogleCatalogMenu(true);
            uiControler.ToogleIsCatalogOpen(true);
            spawnFunc.hidePreview();
        }
    }

    // Update is called once per frame
    void Update()
    {
        uiControler.ToogleDistanceUI(rigthRay.firstInteractableSelected != null && isDistanceUIActive);
        if(spawnFunc.getIsInPreview()){
            bControls.text = "Cancelar";
            ltControls.text = "Colocar mueble";
        }
        else{
            bControls.text = "Eliminar mueble";
            ltControls.text = "Interactuar con menus";
        }

        if (selectedObj != null)
            selectedObj.GetComponent<SpacingValidation>().CheckDistanseFromWalls();
            
        if (rigthRay.isSelectActive && rigthRay.firstInteractableSelected != null)
        {

            yControls.text = "Mover mueble arriba";
            xControls.text = "Mover mueble abajo";

            selectedObj = rigthRay.firstInteractableSelected.colliders[0].gameObject;

            selectedObj.GetComponent<Rigidbody>().mass = 1;
            selectedObj.GetComponent<Rigidbody>().constraints = unFreezeConstraints.constraints;
            
            SpacingValidation sv =  selectedObj.GetComponent<SpacingValidation>();

            sv.CheckDistanseFromWalls();
            sv.isBeingGrabed = true;

            if(isDistanceUIActive)
                showDistance(selectedObj.transform.position,sv.getCollisionPoints(),sv.getDistances(),selectedObj.transform.rotation.y,sv.isAVolume,selectedObj.transform.localScale.y);

            if (basicControls.Pause.WasPerformedThisFrame() && sv.canGoHigher)
            {
                sv.moveInY(0.1f);
            }
            else if (basicControls.Catalog.WasPerformedThisFrame())
            {
                sv.moveInY(-0.1f);
            }
        }
        else if (!rigthRay.isSelectActive && selectedObj != null)
        {
            
            selectedObj.GetComponent<Rigidbody>().mass = 10000;
            selectedObj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            yControls.text = "Abrir Pausa";
            xControls.text = "Abrir Catalogo";
            // freezePosition();
            selectedObj.GetComponent<SpacingValidation>().isBeingGrabed = false;
            
            hideDistance();
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
    
    public void goBackToLobby(){
        gameObject.transform.position = lobbySpawner.transform.position;
    }

    private void freezePosition()
    {
        selectedObj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
    }

    private void unfreezePosition()
    {
        selectedObj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        selectedObj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
        selectedObj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationZ;
        selectedObj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX;
        selectedObj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationY;
        selectedObj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
    }

    private void showDistance(Vector3 origin, Vector3[] hitPoints, double[] distances, float rotation, bool isAVolume, float heigth){
        if(isAVolume){
            origin = new Vector3(origin.x, origin.y - heigth/2,origin.z);
        }
        
        TLine.positionCount = 2;
        TLine.SetPosition(0, origin);
        TLine.SetPosition(1, hitPoints[0]);
        
        BLine.positionCount = 2;
        BLine.SetPosition(0, origin);
        BLine.SetPosition(1, hitPoints[1]);
        
        LLine.positionCount = 2;
        LLine.SetPosition(0, origin);
        LLine.SetPosition(1, hitPoints[2]);
        
        RLine.positionCount = 2;
        RLine.SetPosition(0, origin);
        RLine.SetPosition(1, hitPoints[3]);

        uiControler.setDistance(distances[0],distances[1],distances[2],distances[3],rotation);
    }

    private void hideDistance(){
         
        TLine.positionCount = 2;
        TLine.SetPosition(0, Vector3.zero);
        TLine.SetPosition(1, Vector3.zero);
        
        BLine.positionCount = 2;
        BLine.SetPosition(0, Vector3.zero);
        BLine.SetPosition(1, Vector3.zero);
        
        LLine.positionCount = 2;
        LLine.SetPosition(0, Vector3.zero);
        LLine.SetPosition(1, Vector3.zero);
        
        RLine.positionCount = 2;
        RLine.SetPosition(0, Vector3.zero);
        RLine.SetPosition(1, Vector3.zero);
    }
}
