using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPosTracker : MonoBehaviour
{
    [SerializeField] private Transform rayOrigin;
    [SerializeField] private LayerMask mask;

    private void Update(){
        trackRay();
    }

    private void trackRay(){
        Ray ray = new Ray(rayOrigin.position, rayOrigin.forward);
        Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red);
        RaycastHit hitInfo;
        bool isLookingGround = Physics.Raycast(ray, out hitInfo, 100f,mask);
        if(isLookingGround)
        {
            Debug.Log(((transform.localScale.y)/2+ hitInfo.point.y) + " " + transform.localPosition.y);
            transform.position = new Vector3(hitInfo.point.x,(transform.localScale.y/2)+ hitInfo.point.y,hitInfo.point.z);
        }
    }
}
