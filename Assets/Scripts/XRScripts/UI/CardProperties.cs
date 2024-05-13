using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardProperties : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI cardName;
    [SerializeField] private TextMeshProUGUI dimentions;
    [SerializeField] private RawImage thumbnail;
    // Start is called before the first frame update
    
    public void setName(string newName){
        this.cardName.text = newName;
    }

    public void setdimetions(string newDimentions){
        this.dimentions.text = newDimentions;
    }

    public void setThumbnail(Texture2D newThumbnail){
        this.thumbnail.texture = newThumbnail;
    }
}
