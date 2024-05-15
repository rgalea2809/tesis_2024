using System.Collections;
using System.Collections.Generic;
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

    public bool isGameStarted = false;
    public bool didSelectFreeMode = false;
    public bool didSelectLivingRoomType = false;

    void Start()
    {
        mainMenu.SetActive(true);
        pauseMenu.SetActive(false);
        controlsMenu.SetActive(false);
        creditsMenu.SetActive(false);
        roomSelectionMenu.SetActive(false);
        catalogMenu.SetActive(false);
        volumeCreationMenu.SetActive(false);
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
}
