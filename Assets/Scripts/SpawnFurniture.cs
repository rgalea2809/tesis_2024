using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class SpawnFurniture : MonoBehaviour
{
    private static GameObject obj;
    public GameObject postionPreview;
    private static bool isInPreview = false;



    [SerializeField] public Transform spawiningPosition;

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
            spawiningPosition.position = new Vector3(spawiningPosition.position.x,obj.transform.localScale.y/2,spawiningPosition.position.z);
            postionPreview.transform.localScale = obj.transform.localScale;
            isInPreview = true;
        }
        else{      
            throw new Exception("404: No se pudo encontrar el modelo requerido");
        }
    }

    private void LateUpdate(){
        if(isInPreview){
            postionPreview.transform.position = spawiningPosition.position;
        }
    }

    public void setInPlace(){
        hidePreview();
        UnityEngine.Object.Instantiate(obj,spawiningPosition.position, Quaternion.identity);
    }

    public void hidePreview(){
        postionPreview.transform.position = new Vector3(0,-5,0);
        postionPreview.transform.rotation = Quaternion.Euler(0,0,0);
        isInPreview = false;
    }
}
