using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class fillXRCatalog : MonoBehaviour
{
    public TextAsset jsonFile;

    [System.Serializable]
    private class Furniture
    {
        public string size;
        public string width;
        public string length;

        public string thumbnail;
    }

    [System.Serializable]
     private class Category
    {
        public string category;
        public Furniture[] types;
    }

    [System.Serializable]
     private class Room
    {
        public string room;
        public Category[] categories;
    }

    [System.Serializable]
    private class CatalogInfo{
        public Room[] catalogInfo;
    }

    [SerializeField] private SpawnFurniture spawnFunct;
    [SerializeField] private TextMeshProUGUI furnitureType;
    [SerializeField] private GameObject cardContainer;

    [SerializeField] private RectTransform scrolView;
    [SerializeField] private Button card;
    [SerializeField] private GameObject catalog;

    [SerializeField] private TextMeshProUGUI menuLabel;

    private int catalogSize = 37;
    // Start is called before the first frame update
    void Start()
    {
        fillCatalogFunc(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void fillCatalogFunc(int currentRoom){
        cleanCatalog();
        CatalogInfo assetList = JsonUtility.FromJson<CatalogInfo>(jsonFile.text);
        menuLabel.text = assetList.catalogInfo[currentRoom].room;
        foreach (Category category in assetList.catalogInfo[currentRoom].categories)
        {
            TextMeshProUGUI furnitureLabel = Object.Instantiate(furnitureType,catalog.transform);
            furnitureLabel.text = category.category;
            catalogSize += 8;
            GameObject cardCointainerInstant = Object.Instantiate(cardContainer,catalog.transform);
            foreach(Furniture furniture in category.types)
            {
                Button cardInstant = Object.Instantiate(card,cardCointainerInstant.transform);
                CardProperties cardProps = cardInstant.GetComponent<CardProperties> ();
                cardProps.setThumbnail(Resources.Load<Texture2D>("Thumbnails/"+furniture.thumbnail));
                cardProps.setName(furniture.size);
                cardProps.setdimetions("("+furniture.width+" x "+furniture.length+")");
                cardInstant.onClick.AddListener(() => whenClicked("Cube"));
            }
            catalogSize += 32;
        }
        scrolView.sizeDelta = new Vector2(scrolView.sizeDelta.x, catalogSize);
        catalogSize = 37;

    }

    private void cleanCatalog(){
        while (catalog.transform.childCount > 2) {
            DestroyImmediate(catalog.transform.GetChild(2).gameObject);
        }
    }

    private void whenClicked(string itemName){
        spawnFunct.Spawn(itemName);
    }
}
