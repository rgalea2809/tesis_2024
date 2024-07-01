using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class previewPosChecker : MonoBehaviour
{
    public Material validSpawnPos;
    public Material invalidSpawnPos;

    public bool isSpawnPosValid = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerStay(Collider other){
        if(!other.isTrigger)
        {
            transform.gameObject.GetComponent<Renderer>().material = invalidSpawnPos;
            isSpawnPosValid = false;
        }
    }

    void OnTriggerExit(Collider other){
        if(!other.isTrigger)
        {
            transform.gameObject.GetComponent<Renderer>().material = validSpawnPos;
            isSpawnPosValid = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
