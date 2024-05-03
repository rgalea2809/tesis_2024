using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI promptText;
    [SerializeField] private UIDocument pauseMenu;
    [SerializeField] private UIDocument catalogMenu;	
    [SerializeField] private UIDocument roomSelectionMenu;
    [SerializeField] private UIDocument mainMenu;
    [SerializeField] private UIDocument creditsMenu;
    [SerializeField] private UIDocument controlsMenu;
    
    private bool isPause;
    private bool isCatalogOpen;

    private bool isInMainMenu;
    private bool isCreditsOpen;
    private bool isControlsOpen;

    private bool isRoomMenuOpen;

    private bool isGameStarted;
    // Start is called before the first frame update
    void Start()
    {
        isPause = false;
        isCatalogOpen = false;
        isRoomMenuOpen = false;
        isInMainMenu =true;
        isCreditsOpen = false;
        isControlsOpen =false;
        isGameStarted =false;
    }

    // Update is called once per frame
    public void UpdateText(string promptMessage){
        promptText.text = promptMessage;
    }

    private void Update(){
        pauseMenu.rootVisualElement.Q("Panel").EnableInClassList("hide",!isPause);
        catalogMenu.rootVisualElement.Q("Panel").EnableInClassList("hide",!isCatalogOpen);
        roomSelectionMenu.rootVisualElement.Q("Panel").EnableInClassList("hide",!isRoomMenuOpen);
        mainMenu.rootVisualElement.Q("Panel").EnableInClassList("hide",!isInMainMenu);
        creditsMenu.rootVisualElement.Q("Panel").EnableInClassList("hide",!isCreditsOpen);
        controlsMenu.rootVisualElement.Q("Panel").EnableInClassList("hide",!isControlsOpen);
    }

    public void ToogleIsPause(){
        isPause = !isPause;
    }

    public void ToogleIsCatalogOpen(){
        isCatalogOpen  = !isCatalogOpen ;
    }

    public void ToogleIsRoomMenuOpen(){
        isRoomMenuOpen = !isRoomMenuOpen;
    }

    public void ToogleIsInMainMenu(){
        isInMainMenu = !isInMainMenu;
    }

    public void ToogleIsCreditsOpen(){
        isCreditsOpen = !isCreditsOpen;
    }

    public void ToogleIsControlsOpen(){
        isControlsOpen = !isControlsOpen;
    }

    public void ToogleIsGameStarted(){
        isGameStarted =!isGameStarted;
    }

    public void ToogleFromControlsMenu(){
        if(!isGameStarted)
            isInMainMenu = !isInMainMenu;
        else
            isPause = !isPause;
    }


}
