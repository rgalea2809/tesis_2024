using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class PauseMenuFunctions : MonoBehaviour
{


    public UnityEvent onClick;

    private UIDocument pauseMenu;


    // Start is called before the first frame update
    private void OnEnable()
    {
        pauseMenu = GetComponent<UIDocument>();
    }

    void Start()
    {
        pauseMenu.rootVisualElement.Q("resumeBtn").RegisterCallback<ClickEvent>(resumeOnClick);
    }

    public void resumeOnClick(ClickEvent evt)
    {
        Debug.Log("Hola");
        onClick.Invoke();
    }   


}
