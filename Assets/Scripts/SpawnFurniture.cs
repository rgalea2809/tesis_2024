using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnFurniture : MonoBehaviour
{
    [SerializeField] public Transform spawiningPosition;

    public void Spawn(string objName){
        GameObject catalogObject = (GameObject) Resources.Load<GameObject>("Prefabs/"+objName);
        if(catalogObject != null){
            Object.Instantiate(catalogObject,spawiningPosition.position, Quaternion.identity);
        }
        else{
            Debug.Log("404: Object Not Found");
        }
    }
}
