using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Camera cam;
    private float xRotation = 0f;
    public LayerMask mask;
    public Transform spawnPos;
    public float xSensitivity = 100f;
    public float ySensitivity = 100f;
    // Start is called before the first frame update
    void Start()
    {
    }
    

    public void ProcessLook(Vector2 input)
    {
        float mouseX = input.x;
        float mouseY = input.y;

        //Calculating camera rotation for looking around ( up and down )
        xRotation -= mouseY * Time.deltaTime * ySensitivity;
        xRotation = Math.Clamp(xRotation, -80f, 80f);

        //apply to camera transform

        cam.transform.localRotation = Quaternion.Euler(xRotation,0,0);

        //rotaring character
        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensitivity);
    }

    
    public void movePosWithCamera(){
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hitInfo;
        bool isInView = Physics.Raycast(ray, out hitInfo, 5f ,mask);
        if(isInView)
        {
            spawnPos.position = new Vector3(hitInfo.point.x,spawnPos.position.y,hitInfo.point.z);
        }
    }

}
