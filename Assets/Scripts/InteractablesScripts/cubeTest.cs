using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class cubeTest : Interactable 
{
    
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    protected override void Interact()
    {
        Debug.Log("Interated wirh: " + gameObject.name);
    }

    protected override void Push(Vector2 pushDirection,float angle){
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = pushDirection.x;
        moveDirection.z = pushDirection.y;

        moveDirection = Quaternion.Euler(0,angle,0) * moveDirection;
        
        transform.position += moveDirection * 2f * Time.deltaTime;

        
        DistanceTest();
    }

    protected void DistanceTest(){
        Ray fowardRay = new Ray(transform.position, transform.forward);
        // Ray backwardRay = new Ray(transform.position,Quaternion.Euler(0,180,0) * transform.forward);
        // Ray leftRay = new Ray(transform.position, Quaternion.Euler(0,-90,0) *transform.forward);
        // Ray rightRay = new Ray(transform.position, Quaternion.Euler(0,90,0) *transform.forward);
        Debug.DrawRay(fowardRay.origin,fowardRay.direction* 3f,Color.red);
        // Debug.DrawRay(backwardRay.origin,backwardRay.direction* viewDistance,Color.red);
        // Debug.DrawRay(leftRay.origin,leftRay.direction* viewDistance,Color.red);
        // Debug.DrawRay(rightRay.origin,rightRay.direction* viewDistance,Color.red);
        RaycastHit hitInfo;
        Physics.Raycast(fowardRay, out hitInfo, 3f,1);
        Debug.Log(hitInfo.distance - transform.lossyScale.z/2 + " " + hitInfo.collider);
    }
}
