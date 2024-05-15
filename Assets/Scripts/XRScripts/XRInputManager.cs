using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class XRInputManager : MonoBehaviour
{
    [SerializeField] private UIXRControler uiControler;
    [SerializeField] private SpawnFurniture spawnFunc;
    private XRInputManager xrInputs;
    private PlayerInput playerInput;
    private PlayerInput.XRInputsActions basicControls;
    // Start is called before the first frame update
    void Awake()
    {
        playerInput = new PlayerInput();
        basicControls = playerInput.XRInputs;
        Debug.Log(basicControls);
        basicControls.Pause.performed += ctx => uiControler.TooglePauseMenu(true);
        basicControls.Catalog.performed += ctx => OnOpenCatalog();
        basicControls.Cancel.performed += ctx => spawnFunc.hidePreview();
        basicControls.Confirm.performed += ctx => spawnFunc.setInPlace();
    }

    private void OnOpenCatalog(){
        uiControler.ToogleCatalogMenu(true);
        spawnFunc.hidePreview();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void debugMessage(){
        Debug.Log("Hola");
    }

    private void OnEnable()
    {
        basicControls.Enable();
    }

    private void OnDisable()
    {
        basicControls.Disable();
    }
}
