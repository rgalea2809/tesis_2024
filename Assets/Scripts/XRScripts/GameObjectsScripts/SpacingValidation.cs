using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class SpacingValidation : MonoBehaviour
{
    [SerializeField] private LayerMask rayMask;
    private Vector3 lastValidPosition;
    
    [SerializeField] private GameObject ValidDistanceBox;

    public bool isAVolume = false;

    public bool canGoHigher = true;
    
    private bool hasCollided;
    private static bool isOnValidDistance = true;

    private Vector3[] collisionPoints;
    private double[] distances;

    void OnTriggerStay(Collider other){
        if (!other.isTrigger)
        {
            isOnValidDistance = false;
            ValidDistanceBox.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other){
        if (!other.isTrigger)
        {
            isOnValidDistance = true;
            ValidDistanceBox.SetActive(false);
        }
    }

    void OnCollisionStay(Collision collision){
        if(collision.gameObject.layer != 7)
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
        collisionPoints = new Vector3[4];
        distances = new double[4];
        CheckYPosition();
    }

    public void moveInY(float moveDistance){
            transform.position = new Vector3(transform.position.x,transform.position.y + moveDistance, transform.position.z);
    }

    public void CheckDistanseFromWalls(){
        Vector3 fixY;
        if(isAVolume)
        {
            fixY = new Vector3(transform.position.x, transform.position.y - transform.localScale.y/2, transform.position.z);
        }
        else{
            fixY = transform.position;
        }
        Ray frontRay = new Ray(fixY, transform.forward);
        Ray backRay = new Ray(fixY,Quaternion.Euler(0,180,0) * transform.forward);
        Ray leftRay = new Ray(fixY, Quaternion.Euler(0,-90,0) *transform.forward);
        Ray rightRay = new Ray(fixY, Quaternion.Euler(0,90,0) *transform.forward);
        
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


        collisionPoints[0] = hitInfoTop.point;
        collisionPoints[1] = hitInfoBot.point;
        collisionPoints[2] = hitInfoLef.point;
        collisionPoints[3] = hitInfoRig.point;

        distances[0] = distanceFromTopWall;
        distances[1] = distanceFromBotWall;
        distances[2] = distanceFromLeftWall;
        distances[3] = distanceFromRigthWall;
        
        if(distanceFromTopWall >= 0 && distanceFromBotWall >= 0 && distanceFromLeftWall >= 0 && distanceFromRigthWall >= 0 && !hasCollided)
        {
            lastValidPosition = transform.position;
        }
        else{
            transform.position = lastValidPosition;
        }
    }

    public Vector3[] getCollisionPoints(){
        return collisionPoints;
    }

    public double[] getDistances(){
        return distances;
    }

    public bool getIsOnValidDistance(){
        return isOnValidDistance;
    }

    private void CheckYPosition(){
        if(isAVolume){
            if(transform.position.y - transform.localScale.y/2 >= 1.5){
                canGoHigher = false;
            }
            else{
                canGoHigher = true;
            }
        }
        else{
            if(transform.position.y >= 1.5){
                canGoHigher = false;
            }
            else{
                canGoHigher = true;
            }
        }
    }
}
