using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpacingValidation : MonoBehaviour
{
    [SerializeField] private LayerMask rayMask;

    private Vector3 lastValidPosition;
    
    private bool hasCollided;

    void OnCollisionStay(Collision collision){
        hasCollided = true;
    }

    void OnCollisionExit(Collision collision){
        hasCollided = false;
    }

    void Start()
    {
        lastValidPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CheckDistanseFromWalls(){
        Ray frontRay = new Ray(transform.position, transform.forward);
        Ray backRay = new Ray(transform.position,Quaternion.Euler(0,180,0) * transform.forward);
        Ray leftRay = new Ray(transform.position, Quaternion.Euler(0,-90,0) *transform.forward);
        Ray rightRay = new Ray(transform.position, Quaternion.Euler(0,90,0) *transform.forward);
        
        RaycastHit hitInfoTop;
        RaycastHit hitInfoBot;
        RaycastHit hitInfoLef;
        RaycastHit hitInfoRig;
        
        Physics.Raycast(frontRay, out hitInfoTop, 10f,rayMask);
        Physics.Raycast(backRay, out hitInfoBot, 10f,rayMask);
        Physics.Raycast(leftRay, out hitInfoLef, 10f,rayMask);
        Physics.Raycast(rightRay, out hitInfoRig, 10f,rayMask);

        double distanceFromTopWall =  Math.Round(hitInfoTop.distance,2) - Math.Round(transform.localScale.z/2,2);
        double distanceFromBotWall = Math.Round(hitInfoBot.distance,2) - Math.Round(transform.localScale.z/2,2);
        double distanceFromLeftWall = Math.Round(hitInfoLef.distance,2) - Math.Round(transform.localScale.x/2,2);
        double distanceFromRigthWall = Math.Round(hitInfoRig.distance,2) - Math.Round(transform.localScale.x/2,2);
        
        if(distanceFromTopWall >= 0 && distanceFromBotWall >= 0 && distanceFromLeftWall >= 0 && distanceFromRigthWall >= 0 && !hasCollided)
        {
            lastValidPosition = transform.position;
        }
        else{
            transform.position = lastValidPosition;
        }
    }
}
