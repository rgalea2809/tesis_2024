using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnFurniture : MonoBehaviour
{
    private UIXRControler uicontroler;

    private List<GameObject> furnitureList;

    [SerializeField] private GameObject BedRoomDoor;
    [SerializeField] private GameObject BedRoomWindow;
    [SerializeField] private GameObject LivingRoomDoor;
    [SerializeField] private GameObject LivingRoomWindow;


    private static int count = 0;


    private static GameObject obj;
    public GameObject postionPreview;
    private static bool isInPreview = false;
    private bool isVolume = false;
    [SerializeField] public Transform spawiningPosition;

    [SerializeField] private GameObject furnitureContainer;

    void Start()
    {
        isInPreview = false;
        uicontroler = GetComponent<UIXRControler>();
        furnitureList = new List<GameObject>();
    }

    public void Spawn(string objName)
    {
        obj = (GameObject)Resources.Load<GameObject>("Prefabs/" + objName);
        if (obj != null)
        {
            isVolume = false;
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
            if(postionPreview.GetComponent<previewPosChecker>().isSpawnPosValid && (spawiningPosition.position.y - spawiningPosition.localScale.y/2 ) <= 1.5)
            {
                Vector3 newObjectPos = new(spawiningPosition.position.x, spawiningPosition.position.y - spawiningPosition.localScale.y/2, spawiningPosition.position.z);
                if(isVolume)
                {
                    obj.GetComponent<SpacingValidation>().isAVolume = true;
                    obj = UnityEngine.Object.Instantiate(obj, postionPreview.transform.position, postionPreview.transform.localRotation,furnitureContainer.transform);
                    
                }
                else
                    obj = UnityEngine.Object.Instantiate(obj, newObjectPos, postionPreview.transform.localRotation,furnitureContainer.transform);
                furnitureList.Add(obj);
                hidePreview();
            }
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

    public void DespawnAllFurniture() {
        // while (furnitureContainer.transform.childCount > 0) {
        //     DestroyImmediate(furnitureContainer.transform.GetChild(0).gameObject);
        // }
        Debug.Log("aqui" + furnitureList.Count);
        furnitureList.ForEach(furniture => {
            Debug.Log(furniture);
            if(furniture != null)
                DestroyImmediate(furniture,true);
        });

        furnitureList.Clear();
    }

    public void DeactivateAllValidDistancesBox(){
        furnitureList.ForEach(furniture =>{
            if(furniture != null)
                furniture.GetComponent<SpacingValidation>().DeactivateValidDistanceBox();
        });
        BedRoomDoor.GetComponent<SpacingValidation>().DeactivateValidDistanceBox();
        BedRoomWindow.GetComponent<SpacingValidation>().DeactivateValidDistanceBox();
        LivingRoomDoor.GetComponent<SpacingValidation>().DeactivateValidDistanceBox();
        LivingRoomWindow.GetComponent<SpacingValidation>().DeactivateValidDistanceBox();
    }

}
