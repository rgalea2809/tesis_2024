using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public bool useEvents;
    public string promptMessage;

    public void BaseInteract()
    {
        if(useEvents)
            GetComponent<InteractionEvent>().onInteract.Invoke();
        Interact();
    }

   
    // Start is called before the first frame update
    protected virtual void Interact(){}
    
    public void BasePush(Vector2 direction, float angle){
        Push(direction, angle);
    }


    protected virtual void Push(Vector2 direction, float angle){}
}
