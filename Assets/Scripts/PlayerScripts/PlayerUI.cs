using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI promptText;
    [SerializeField] private Canvas pauseMenu;
    [SerializeField] private UIDocument catalogMenu;	
    
    private bool isPause;
    private bool isCatalogOpen;
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.enabled = false;
        isPause = false;
        isCatalogOpen = false;
    }

    // Update is called once per frame
    public void UpdateText(string promptMessage){
        promptText.text = promptMessage;
    }

    private void Update(){
        pauseMenu.enabled = isPause;
        catalogMenu.enabled = isCatalogOpen;
    }

    public void ToogleIsPause(){
        isPause = !isPause;
    }

    public void ToogleIsCatalogOpen(){
        isCatalogOpen  = !isCatalogOpen ;
    }

}
