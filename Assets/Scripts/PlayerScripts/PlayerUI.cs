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
    
    private bool isPause;
    private bool isCatalogOpen;

    private bool isRoomMenuOpen;
    // Start is called before the first frame update
    void Start()
    {
        isPause = false;
        isCatalogOpen = false;
        isRoomMenuOpen = true;
        
    }

    // Update is called once per frame
    public void UpdateText(string promptMessage){
        promptText.text = promptMessage;
    }

    private void Update(){
        pauseMenu.enabled = isPause;
        catalogMenu.rootVisualElement.Q("Panel").EnableInClassList("hide",!isCatalogOpen);
        roomSelectionMenu.rootVisualElement.Q("Panel").EnableInClassList("hide",!isRoomMenuOpen);
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


}
