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
        // basicControls.Catalog.performed += ctx => OnOpenCatalog();
        
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

     private void OnOpenMenu()
     {
         menu.ToogleIsPause();
         Cursor.lockState = CursorLockMode.None;
         basicControls.Disable();
     }

     public void OnCloseMenu()
     {
         basicControls.Enable();
         menu.ToogleIsPause();
         Cursor.lockState = CursorLockMode.Locked;
     }

    // private void OnOpenCatalog()
    // {
    //     playerInput.UI.Enable();
    //     menu.ToogleIsCatalogOpen();
    //     Cursor.lockState = CursorLockMode.None;
    //     playerInput.basicControls.Disable();
    // }

    // public void OnCloseCatalog()
    // {
    //     playerInput.basicControls.Enable();
    //     menu.ToogleIsCatalogOpen();
    //     Cursor.lockState = CursorLockMode.Locked;
    //     playerInput.UI.Disable();
    // }

    public void CloseGame(){
        Application.Quit();
    }

    public PlayerInput.BasicControlsActions getbasicControls(){
        return basicControls;
    }
}
