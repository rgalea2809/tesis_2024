using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class MainMenuFunctions : MonoBehaviour
{
    public UnityEvent evntFreeModeBtn;
    
    public UnityEvent evntTrainingBtn;
    
    public UnityEvent evntCreditsBtn;
    
    public UnityEvent evntControlsBtn;

    private UIDocument mainMenu;


    // Start is called before the first frame update
    private void OnEnable()
    {
        mainMenu = GetComponent<UIDocument>();
    }

    void Start()
    {
        mainMenu.rootVisualElement.Q("freeModeButton").RegisterCallback<ClickEvent>(startFreeMode);
        mainMenu.rootVisualElement.Q("trainingBtn").RegisterCallback<ClickEvent>(startTrainingMode);
        mainMenu.rootVisualElement.Q("controlsBtn").RegisterCallback<ClickEvent>(openControls);
        mainMenu.rootVisualElement.Q("creditsBtn").RegisterCallback<ClickEvent>(openCredits);
        mainMenu.rootVisualElement.Q("exitBtn").RegisterCallback<ClickEvent>(closeGame);
    }

    private void startFreeMode(ClickEvent evt)
    {
        evntFreeModeBtn.Invoke();
    }   

    private void startTrainingMode(ClickEvent evt)
    {
        evntTrainingBtn.Invoke();
    }   

    private void openCredits(ClickEvent evt)
    {
        evntCreditsBtn.Invoke();
    }   
    private void openControls(ClickEvent evt)
    {
        evntControlsBtn.Invoke();
    }   

    private void closeGame(ClickEvent evt)
    {
        Debug.Log("aun no implementado, losiento");
    }
}
