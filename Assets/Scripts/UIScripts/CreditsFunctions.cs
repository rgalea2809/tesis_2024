using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class CreditsFunctions : MonoBehaviour
{
     public UnityEvent onClick;

    private UIDocument creditsMenu;


    // Start is called before the first frame update
    private void OnEnable()
    {
        creditsMenu = GetComponent<UIDocument>();
    }

    void Start()
    {
        creditsMenu.rootVisualElement.Q("goBackBtn").RegisterCallback<ClickEvent>(resumeOnClick);
    }

    public void resumeOnClick(ClickEvent evt)
    {
        Debug.Log("Hola");
        onClick.Invoke();
    }   

}
