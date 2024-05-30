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
    private bool isVolume = false;
    [SerializeField] public Transform spawiningPosition;

    void Start()
    {
        isInPreview = false;
    }

    public void Spawn(string objName)
    {
        obj = (GameObject)Resources.Load<GameObject>("Prefabs/" + objName);
        if (obj != null)
        {
            isVolume = false;
            spawiningPosition.position = new Vector3(spawiningPosition.position.x, obj.transform.localScale.y / 2, spawiningPosition.position.z);
            spawiningPosition.localScale = new(spawiningPosition.localScale.x, obj.transform.localScale.y, spawiningPosition.localScale.z);
            postionPreview.transform.localScale = obj.transform.localScale;
            isInPreview = true;
        }
        else
        {
            Debug.Log("404: Object Not Found");
        }
    }

    public void Spawn(float heigth, float width, float length)
    {
        obj = (GameObject)Resources.Load<GameObject>("Prefabs/Cube");
        spawiningPosition.position = new Vector3(spawiningPosition.position.x, heigth / 2, spawiningPosition.position.z);
        spawiningPosition.localScale = new(spawiningPosition.localScale.x, heigth, spawiningPosition.localScale.z);
        if (obj != null)
        {
            isVolume = true;
            obj.transform.localScale = new Vector3(length, heigth, width);
            postionPreview.transform.localScale = obj.transform.localScale;
            isInPreview = true;
        }
        else
        {
            throw new Exception("404: No se pudo encontrar el modelo requerido");
        }
    }


    void FixedUpdate()
    {
        if (isInPreview)
        {
            postionPreview.transform.position = spawiningPosition.position;
        }
    }

    public void setInPlace()
    {
        if (isInPreview)
        {
            Vector3 newObjectPos = new(spawiningPosition.position.x, spawiningPosition.position.y - spawiningPosition.localScale.y/2 + 0.001f, spawiningPosition.position.z);
            if(isVolume)
                UnityEngine.Object.Instantiate(obj, postionPreview.transform.position, postionPreview.transform.localRotation);
            else
                UnityEngine.Object.Instantiate(obj, newObjectPos, postionPreview.transform.localRotation);
            hidePreview();
        }
    }

    public void hidePreview()
    {
        if (isInPreview)
        {
            postionPreview.transform.position = new Vector3(0, -5, 0);
            postionPreview.transform.localRotation = Quaternion.Euler(0, 0, 0);
            isInPreview = false;
        }
    }

    public void rotateItem()
    {
        if (isInPreview)
        {
            postionPreview.transform.Rotate(0, 90, 0);
        }
    }

    public bool getIsInPreview(){
        return isInPreview;
    }

}
