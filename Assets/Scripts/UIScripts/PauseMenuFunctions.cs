using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class PauseMenuFunctions : MonoBehaviour
{
    public UnityEvent evntResumeBtn;
    
    public UnityEvent evntControlsBtn;
    
    public UnityEvent evntExitBtn;

    private UIDocument pauseMenu;


    // Start is called before the first frame update
    private void OnEnable()
    {
        pauseMenu = GetComponent<UIDocument>();
    }

    void Start()
    {
        pauseMenu.rootVisualElement.Q("resumeBtn").RegisterCallback<ClickEvent>(resumeOnClick);
        pauseMenu.rootVisualElement.Q("controlesBtn").RegisterCallback<ClickEvent>(controlsOnClick);
        pauseMenu.rootVisualElement.Q("salirBtn").RegisterCallback<ClickEvent>(exitOnClick);
    }

    public void resumeOnClick(ClickEvent evt)
    {
        evntResumeBtn.Invoke();
    }   

     public void controlsOnClick(ClickEvent evt)
    {
        evntControlsBtn.Invoke();
    }   

     public void exitOnClick(ClickEvent evt)
    {
        evntExitBtn.Invoke();
    }   


}
