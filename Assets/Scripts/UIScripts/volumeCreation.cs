using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class volumeCreation : MonoBehaviour
{ 
    public UnityEvent envtGoBackBtn;
    public UnityEvent envtCreateBtn;
    private UIDocument volumeOptions;
    private float width;
    private float heigth;
    private float length;
    private SpawnFurniture spawnFunct;


    private void OnEnable(){

        volumeOptions = GetComponent<UIDocument>();

    }
    // Start is called before the first frame update
    void Start()
    {
        spawnFunct = GetComponent<SpawnFurniture>();
        volumeOptions.rootVisualElement.Q("goBackBtn").RegisterCallback<ClickEvent>(ctx =>envtGoBackBtn.Invoke());
        //createBtn
        volumeOptions.rootVisualElement.Q("createBtn").RegisterCallback<ClickEvent>(ctx => createVolume(length,heigth,width));
    }


    // Update is called once per frame
    void Update()
    {
        width =  (float) volumeOptions.rootVisualElement.Q<DoubleField>("Ancho").value;
        heigth = (float) volumeOptions.rootVisualElement.Q<DoubleField>("Alto").value;
        length = (float) volumeOptions.rootVisualElement.Q<DoubleField>("Largo").value;
    }


    private void createVolume(float length, float heigth, float width){ 
       try{
            if(heigth <= 0 || heigth > 3){
                throw new Exception("Alto debe estar entre 0 y 3 m");
            }
            if(width <= 0 || width > 1.5){
                throw new Exception("Ancho debe estar entre 0 y 1.5 m");
            }
            if(length <= 0 || length > 1.5){
                throw new Exception("Largo debe estar entre 0 y 1.5 m");
            }
            spawnFunct.Spawn(heigth,width,length);
            envtCreateBtn.Invoke();
        }catch(Exception e){
            volumeOptions.rootVisualElement.Q<Label>("errorLabel").text = e.Message;
        }
    }

}
