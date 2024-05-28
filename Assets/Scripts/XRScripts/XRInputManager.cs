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

    [SerializeField] private TextMeshProUGUI xControls;
    [SerializeField] private TextMeshProUGUI yControls;
    [SerializeField] private TextMeshProUGUI aControls;
    [SerializeField] private TextMeshProUGUI bControls;
    [SerializeField] private TextMeshProUGUI ltControls;

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
        basicControls.Rotate.performed += ctx => spawnFunc.rotateItem();
    }

    private void RequestPauseMenu()
    {
        Debug.Log("Requested pause menu");
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
            Vector3 moveY = Vector3.zero;

            selectedObj = rigthRay.firstInteractableSelected.colliders[0].gameObject;

            unfreezePosition();

            selectedObj.GetComponent<SpacingValidation>().CheckDistanseFromWalls();

            if (basicControls.Pause.IsPressed())
            {
                Debug.Log(selectedObj + " should go up by " + (selectedObj.transform.position.y + 0.1f));
                selectedObj.GetComponent<SpacingValidation>().moveInY(0.1f);
            }
            else if (basicControls.Catalog.IsPressed())
            {
                Debug.Log(selectedObj + " should go down by " + (selectedObj.transform.position.y - 0.1f));
                selectedObj.GetComponent<SpacingValidation>().moveInY(-0.1f);

            }
        }
        else if (!rigthRay.isSelectActive && selectedObj != null)
        {
            yControls.text = "Abrir Pausa";
            xControls.text = "Abrir Catalogo";
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
}
