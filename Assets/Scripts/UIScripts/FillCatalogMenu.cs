using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.UIElements;
public class FillCatalogMenu : MonoBehaviour
{
    public UnityEvent onClick;

    private UIDocument catalogInventory;
    public VisualTreeAsset catalogTypeTemplate;
    public TextAsset jsonFile;
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

    private SpawnFurniture spawnFunct;


    private void OnEnable(){

        catalogInventory = GetComponent<UIDocument>();
        
    }
    // Start is called before the first frame update
    void Start()
    {
        spawnFunct = GetComponent<SpawnFurniture>();
        catalogInventory.rootVisualElement.Q("exitBtn").RegisterCallback<ClickEvent>(OnClick);
    }

    public void OnClick(ClickEvent evt)
    {
        onClick.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void fillCatalog(int currentRoom){
        CatalogInfo assetList = JsonUtility.FromJson<CatalogInfo>(jsonFile.text);

        catalogInventory.rootVisualElement.Q<Label>("roomName").text = assetList.catalogInfo[currentRoom].room;
        catalogInventory.rootVisualElement.Q("CatalogList").Clear();
    
        foreach (Category category in assetList.catalogInfo[currentRoom].categories)
        {
            TemplateContainer catalogTypeContainer = catalogTypeTemplate.Instantiate();
            catalogTypeContainer.Q<Label>("furnitureLabel").text = category.category;

            foreach(Furniture furniture in category.types){
                TemplateContainer cardContainer = cardTemplate.Instantiate();
                cardContainer.Q<Label>("furnitureName").text = furniture.size;  
                cardContainer.Q<Label>("furnitureSize").text = "("+furniture.width+" x "+furniture.length+")";
                cardContainer.Q<VisualElement>("furnitureImage").style.backgroundImage = (StyleBackground)Resources.Load<Texture>("Thumbnails/"+furniture.thumbnail);
                cardContainer.RegisterCallback<ClickEvent>(ctx=>spawnItem("Cube"));
                catalogTypeContainer.Q("furnitureList").Add(cardContainer);
            }

            catalogInventory.rootVisualElement.Q("CatalogList").Add(catalogTypeContainer);
        }
    }

    private void spawnItem(string itemName){
        spawnFunct.Spawn(itemName);
        onClick.Invoke();
    }

}
