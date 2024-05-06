using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerInput.BasicControlsActions basicControls;
    private PlayerInput.PreviewControlsActions previewControls;
    // private PlayerInput.UIActions ui;

    private bool lockView = false;
    private bool isInPreviewMode = false;

    private PlayerMotor motor;
    private PlayerLook  look;
    private PlayerUI menu;
    
    private SpawnFurniture spawnFunction;

    void Awake()
    {
        playerInput = new PlayerInput();
        basicControls = playerInput.BasicControls;
        previewControls = playerInput.PreviewControls;
        // ui = playerInput.UI;
        // playerInput.UI.Disable();

        motor = GetComponent<PlayerMotor>();
        look = GetComponent<PlayerLook>();
        menu = GetComponent<PlayerUI>();
        spawnFunction = GetComponent<SpawnFurniture>();
 
        basicControls.Pause.performed += ctx => OnOpenMenu();
        basicControls.Catalog.performed += ctx => OnOpenCatalog();
 
        previewControls.Pause.performed += ctx => OnOpenMenu();
        previewControls.Cancel.performed += ctx => OnCancelPreview();
        previewControls.Confirm.performed += ctx => OnConfirmPlacement();
        previewControls.Rotation.performed += ctx => spawnFunction.rotateItem();
    }

    void Start(){
        basicControls.Disable();
        previewControls.Disable();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        motor.ProcessMove(basicControls.Movement.ReadValue<Vector2>());
        motor.ProcessMove(previewControls.Movement.ReadValue<Vector2>());
    }

    private void LateUpdate()
    {
        look.ProcessLook(basicControls.Look.ReadValue<Vector2>());
        look.ProcessLook(previewControls.Look.ReadValue<Vector2>());
        spawnFunction.positionUpdating();
        look.movePosWithCamera();
    }

    private void OnEnable()
    {
        basicControls.Enable();
    }

    private void OnDisable()
    {
        basicControls.Disable();
    }

    //Menu input management//////////////////////////////////////////////////////////////////

    //pause menu
     private void OnOpenMenu()
     {
         menu.ToogleIsPause();
         Cursor.lockState = CursorLockMode.None;
         basicControls.Disable();
     }

    //catalog menu
    private void OnOpenCatalog()
    {
        menu.ToogleIsCatalogOpen();
        Cursor.lockState = CursorLockMode.None;
        basicControls.Disable();
    }

    public void OnClosingMenu(){
        basicControls.Enable();
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void CloseGame(){
        Application.Quit();
    }

    public PlayerInput.BasicControlsActions getbasicControls(){
        return basicControls;
    }

    private void OnCancelPreview(){
        spawnFunction.hidePreview();
        previewControls.Disable();
        basicControls.Enable();
    }

    private void OnConfirmPlacement(){
        spawnFunction.setInPlace();
        previewControls.Disable();
        basicControls.Enable();
    }

    public void EnterPreviewMode(){
        previewControls.Enable();
        Cursor.lockState = CursorLockMode.Locked;
    }
}
