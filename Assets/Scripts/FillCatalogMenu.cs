using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
public class FillCatalogMenu : MonoBehaviour
{

    private UIDocument catalogInventory;
    public VisualTreeAsset catalogTypeTemplate;
    public TextAsset jsonFile;

    public int currentRoom;

    public VisualTreeAsset cardTemplate;

    [Serializable]
    private class Furniture
    {
        public string size;
        public string width;
        public string length;

        public string thumbnail;
    }

    [Serializable]
     private class Category
    {
        public string category;
        public Furniture[] types;
    }

    [Serializable]
     private class Room
    {
        public string room;
        public Category[] categories;
    }

    [Serializable]
    private class CatalogInfo{
        public Room[] catalogInfo;
    }

    private void OnEnable(){
        currentRoom = 0;

        catalogInventory = GetComponent<UIDocument>();

        CatalogInfo assetList = JsonUtility.FromJson<CatalogInfo>(jsonFile.text);

        catalogInventory.rootVisualElement.Q<Label>("roomName").text = assetList.catalogInfo[currentRoom].room;

    
        foreach (Category category in assetList.catalogInfo[currentRoom].categories)
        {
            TemplateContainer catalogTypeContainer = catalogTypeTemplate.Instantiate();
            catalogTypeContainer.Q<Label>("furnitureLabel").text = category.category;

            foreach(Furniture furniture in category.types){
                TemplateContainer cardContainer = cardTemplate.Instantiate();
                cardContainer.Q<Label>("furnitureName").text = furniture.size;  
                cardContainer.Q<Label>("furnitureSize").text = "("+furniture.width+" x "+furniture.length+")";
                cardContainer.Q<VisualElement>("furnitureImage").style.backgroundImage = (StyleBackground)Resources.Load<Texture>("Thumbnails/"+furniture.thumbnail);

                catalogTypeContainer.Q("furnitureList").Add(cardContainer);
            }

            catalogInventory.rootVisualElement.Q("CatalogList").Add(catalogTypeContainer);
        }
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
