using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpacingValidation : MonoBehaviour
{
    [SerializeField] private LayerMask mask;

    private Ray frontRay;
    private Ray backRay;
    private Ray leftRay;
    private Ray rightRay;

    private Vector3 lastValidPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        lastValidPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        CheckDistanseFromWalls();
    }

    private void CheckDistanseFromWalls(){
        frontRay = new Ray(transform.position, transform.forward);
        backRay = new Ray(transform.position,Quaternion.Euler(0,180,0) * transform.forward);
        leftRay = new Ray(transform.position, Quaternion.Euler(0,-90,0) *transform.forward);
        rightRay = new Ray(transform.position, Quaternion.Euler(0,90,0) *transform.forward);
        
        RaycastHit hitInfoTop;
        RaycastHit hitInfoBot;
        RaycastHit hitInfoLef;
        RaycastHit hitInfoRig;
        
        Physics.Raycast(frontRay, out hitInfoTop, 10f,mask);
        Physics.Raycast(backRay, out hitInfoBot, 10f,mask);
        Physics.Raycast(leftRay, out hitInfoLef, 10f,mask);
        Physics.Raycast(rightRay, out hitInfoRig, 10f,mask);

        double distanceFromTopWall =  Math.Round(hitInfoTop.distance,2) - Math.Round(transform.localScale.z/2,2);
        double distanceFromBotWall = Math.Round(hitInfoBot.distance,2) - Math.Round(transform.localScale.z/2,2);
        double distanceFromLeftWall = Math.Round(hitInfoLef.distance,2) - Math.Round(transform.localScale.x/2,2);
        double distanceFromRigthWall = Math.Round(hitInfoRig.distance,2) - Math.Round(transform.localScale.x/2,2);

        

        if(distanceFromTopWall >= 0 && distanceFromBotWall >= 0 && distanceFromLeftWall >= 0 && distanceFromRigthWall >= 0)
        {
            lastValidPosition = transform.position;
        }
        else{
            transform.position = lastValidPosition;
        }
    }
}
