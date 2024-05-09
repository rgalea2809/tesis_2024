using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using TMPro;
using UnityEditor.ShaderKeywordFilter;
using UnityEditor.UIElements;
using UnityEngine;

public class ergonomicsLogic : MonoBehaviour
{
    public EventTags a;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerStay(Collider collider)
    {
        if(collider.gameObject.tag == "Untagged")
        Debug.Log("too close man");
    }

    // Update is called once per frame
    void OnCollisionStay(Collision collisionInfo)
    {
        Debug.Log(collisionInfo.gameObject.tag);
    }
}
