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
            transform.position = new Vector3(hitInfo.point.x,gameObject.transform.position.y,hitInfo.point.z);
        }
    }
}