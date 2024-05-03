using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class RoomSelection : MonoBehaviour
{
    private UIDocument roomList;

    public FillCatalogMenu fillFunc;
    public VisualTreeAsset roomBtn;
    public UnityEvent onClick;
    public UnityEvent goBack;

    private enum roomNames {
        Dormitorio,
        Sala,

    }

    // Start is called before the first frame update
    void Start()
    {
        roomList = GetComponent<UIDocument>();
        //goBackBtn
        roomList.rootVisualElement.Q("goBackBtn").RegisterCallback<ClickEvent>(ctx=>goBack.Invoke());
        fillRoomList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     private void fillRoomList(){
        for(int i=0;i<2;i++){
            int currentIndex = i;
            TemplateContainer roomCard = roomBtn.Instantiate();
            roomCard.Q<Label>("roomCardLabel").text = Enum.GetName(typeof(roomNames),i);
            roomCard.Q("roomBtn").RegisterCallback<ClickEvent>(ctx=>fillCatalogOfSelectedRoom(currentIndex));
            roomList.rootVisualElement.Q("menuBody").Add(roomCard);
        }
    }

    private void fillCatalogOfSelectedRoom(int index){
        fillFunc.fillCatalog(index);
        onClick.Invoke();
    }
}
