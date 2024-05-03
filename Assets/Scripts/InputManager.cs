using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private PlayerInput.BasicControlsActions basicControls;
    // private PlayerInput.UIActions ui;

    private bool lockView = false;

    private PlayerMotor motor;
    private PlayerLook  look;
    private PlayerUI menu;
    
    
    void Awake()
    {
        playerInput = new PlayerInput();
        basicControls = playerInput.BasicControls;
        // ui = playerInput.UI;
        // playerInput.UI.Disable();

        motor = GetComponent<PlayerMotor>();
        look = GetComponent<PlayerLook>();
        menu = GetComponent<PlayerUI>();
 
        basicControls.Pause.performed += ctx => OnOpenMenu();
        basicControls.Catalog.performed += ctx => OnOpenCatalog();
        
    }

    void Start(){
        basicControls.Disable();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        motor.ProcessMove(basicControls.Movement.ReadValue<Vector2>());
    }

    private void LateUpdate()
    {
        if(!lockView)
            look.ProcessLook(basicControls.Look.ReadValue<Vector2>());
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
}
