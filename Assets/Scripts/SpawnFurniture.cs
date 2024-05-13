using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnFurniture : MonoBehaviour
{   private static GameObject obj;
    public GameObject postionPreview;
    private static bool isInPreview = false;
    [SerializeField] public Transform spawiningPosition;

    void Start(){
        isInPreview = false;
    }

    public void Spawn(string objName){
        obj = (GameObject) Resources.Load<GameObject>("Prefabs/"+objName);
        if(obj != null){
            spawiningPosition.position = new Vector3(spawiningPosition.position.x,obj.transform.localScale.y/2,spawiningPosition.position.z);
            postionPreview.transform.localScale = obj.transform.localScale;
            isInPreview = true;
        }
        else{
            Debug.Log("404: Object Not Found");
        }
    }

    public void Spawn(float heigth, float width, float length){
        obj = (GameObject) Resources.Load<GameObject>("Prefabs/Cube");
        spawiningPosition.position = new Vector3(spawiningPosition.position.x,obj.transform.localScale.y/2,spawiningPosition.position.z);
        if(obj != null){
            obj.transform.localScale = new Vector3(length,heigth,width);
            postionPreview.transform.localScale = obj.transform.localScale;
            isInPreview = true;
        }
        else{      
            throw new Exception("404: No se pudo encontrar el modelo requerido");
        }
    }


    void FixedUpdate(){
        if(isInPreview){
            Debug.Log(spawiningPosition.position.x + " "+ spawiningPosition.position.y + " " +  + spawiningPosition.position.z);
            postionPreview.transform.position = spawiningPosition.position;
        }
    }

    public void setInPlace(){
        Debug.Log(postionPreview.transform.rotation.y);
        UnityEngine.Object.Instantiate(obj,spawiningPosition.position,postionPreview.transform.localRotation);
        hidePreview();
    }

    public void hidePreview(){
        postionPreview.transform.position = new Vector3(0,-5,0);
        postionPreview.transform.localRotation = Quaternion.Euler(0,0,0);
        isInPreview = false;
    }

    public void rotateItem(){
        Debug.Log(postionPreview.transform.rotation);
        postionPreview.transform.Rotate(0,90,0);
    }

}
