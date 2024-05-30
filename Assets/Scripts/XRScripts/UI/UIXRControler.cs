using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class UIXRControler : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject controlsMenu;
    [SerializeField] private GameObject creditsMenu;
    [SerializeField] private GameObject roomSelectionMenu;
    [SerializeField] private GameObject catalogMenu;
    [SerializeField] private GameObject volumeCreationMenu;
    [SerializeField] private GameObject gameEndMenu;
    [SerializeField] private GameObject socketsNotFilledErrorPrompt;

    [SerializeField] private GameObject LeftControllerUI;
    [SerializeField] private GameObject RightControllerUI;

    [SerializeField] private GameObject DistanceUI;
    [SerializeField] private TextMeshProUGUI topDistance;
    [SerializeField] private TextMeshProUGUI bottomDistance;
    [SerializeField] private TextMeshProUGUI leftDistance;
    [SerializeField] private TextMeshProUGUI rigthDistance;

    private float lastOneEightyRotation = 0;

    public bool isGameStarted = false;
    public bool didSelectFreeMode = false;
    public bool didSelectLivingRoomType = false;

    public bool isPaused = false;

    public bool isCatalogOpen = false;

    private bool isControlerHelpActive = true;

    void Start()
    {
        mainMenu.SetActive(true);
        pauseMenu.SetActive(false);
        controlsMenu.SetActive(false);
        creditsMenu.SetActive(false);
        roomSelectionMenu.SetActive(false);
        catalogMenu.SetActive(false);
        volumeCreationMenu.SetActive(false);
        gameEndMenu.SetActive(false);
    }


    // Actions Handlers
    public void SelectFreeMode()
    {
        ToogleMainMenu(false);
        ToogleRoomSelectionMenu(true);
        didSelectFreeMode = true;
    }

    public void SelectTrainning()
    {
        ToogleMainMenu(false);
        ToogleRoomSelectionMenu(true);
        didSelectFreeMode = false;
    }

    public void SelectLivingRoom()
    {
        ToogleRoomSelectionMenu(false);
        ToogleIsGameStarted();
        didSelectLivingRoomType = true;
    }

    public void SelectBedroom()
    {
        ToogleRoomSelectionMenu(false);
        ToogleIsGameStarted();
        didSelectLivingRoomType = false;
    }

    // UI Togglers
    public void ToogleMainMenu(bool flag)
    {
        mainMenu.SetActive(flag);
    }
    public void TooglePauseMenu(bool flag)
    {
        pauseMenu.SetActive(flag);
        isPaused = flag;
    }
    public void ToogleControlsMenu(bool flag)
    {
        controlsMenu.SetActive(flag);
    }
    public void ToogleCreditsMenu(bool flag)
    {
        creditsMenu.SetActive(flag);
    }
    public void ToogleRoomSelectionMenu(bool flag)
    {
        roomSelectionMenu.SetActive(flag);
    }
    public void ToogleCatalogMenu(bool flag)
    {
        catalogMenu.SetActive(flag);
        isCatalogOpen = flag;
    }
    public void ToogleVolumeCreationMenu(bool flag)
    {
        volumeCreationMenu.SetActive(flag);
    }
    public void GoBackFromControlsMenu()
    {
        if (isGameStarted)
        {
            pauseMenu.SetActive(true);
        }
        else
        {
            mainMenu.SetActive(true);
        }
    }

    public void ToogleIsGameStarted()
    {
        isGameStarted = !isGameStarted;
    }

    public void ToogleControlerUI(){
        isControlerHelpActive = !isControlerHelpActive;
        LeftControllerUI.SetActive(isControlerHelpActive);
        RightControllerUI.SetActive(isControlerHelpActive);
    }

    public void ToogleDistanceUI(bool flag){
        DistanceUI.SetActive(flag);
    }

    public void setDistance(double tDistance,double bDistance,double lDistance,double rDistance,float rotation){
        Debug.Log(rotation);
        if(rotation == 0f)
        {
            lastOneEightyRotation = 0f;
            topDistance.text = tDistance + " m";
            bottomDistance.text = bDistance + " m";
            leftDistance.text = lDistance + " m";
            rigthDistance.text = rDistance + " m";
        }
        else if((rotation == 0.7071068f || rotation == -0.7071068f) && lastOneEightyRotation ==0f)
        {
            topDistance.text = lDistance + " m";
            bottomDistance.text = rDistance + " m";
            leftDistance.text = bDistance + " m";
            rigthDistance.text = tDistance + " m";
        }
        else if(rotation == 1f || rotation == -1f)
        {
            lastOneEightyRotation = 1f;
            topDistance.text = bDistance + " m";
            bottomDistance.text = tDistance + " m";
            leftDistance.text = rDistance + " m";
            rigthDistance.text = lDistance + " m";
        }
        else if((rotation == 0.7071068f || rotation == -0.7071068f) && lastOneEightyRotation == 1f)
        {   
            topDistance.text = rDistance + " m";
            bottomDistance.text = lDistance + " m";
            leftDistance.text = tDistance + " m";
            rigthDistance.text = bDistance + " m";
        }
    }

    private void HideAllMenus()
    {
        mainMenu.SetActive(false);
        pauseMenu.SetActive(false);
        controlsMenu.SetActive(false);
        creditsMenu.SetActive(false);
        roomSelectionMenu.SetActive(false);
        catalogMenu.SetActive(false);
        volumeCreationMenu.SetActive(false);
    }

    public void ToggleGameEndMenu(bool flag)
    {
        gameEndMenu.SetActive(flag);
    }

    public void ToggleSocketsNotFilled(bool flag)
    {
        socketsNotFilledErrorPrompt.SetActive(flag);
    }
}
