using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class ControlersMenuFunctions : MonoBehaviour
{
    public UnityEvent onClick;

    private UIDocument controlsMenu;


    // Start is called before the first frame update
    private void OnEnable()
    {
        controlsMenu = GetComponent<UIDocument>();
    }

    void Start()
    {
        controlsMenu.rootVisualElement.Q("goBackBtn").RegisterCallback<ClickEvent>(resumeOnClick);
    }

    public void resumeOnClick(ClickEvent evt)
    {
        Debug.Log("Hola");
        onClick.Invoke();
    }   
}
