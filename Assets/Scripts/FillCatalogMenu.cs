using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
public class FillCatalogMenu : MonoBehaviour
{

    private UIDocument catalogInventory;
    public VisualTreeAsset catalogTypeTemplate;

    public VisualTreeAsset cardTemplate;
    private void OnEnable(){
        catalogInventory = GetComponent<UIDocument>();

        // for(int i=0; i<2;i++){  
        //     TemplateContainer catalogTypeContainer = catalogTypeTemplate.Instantiate();
        //     TemplateContainer cardContainer = cardTemplate.Instantiate();
        //     catalogTypeContainer.Q("furnitureList").Add(cardContainer);
        //     catalogTypeContainer.Q<Label>("furnitureLabel").text = "Camas";

        //     catalogInventory.rootVisualElement.Q("CatalogList").Add(catalogTypeContainer);
        // }

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
