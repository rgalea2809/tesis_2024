using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class volumeCreation : MonoBehaviour
{
    [SerializeField] private Slider length;
    [SerializeField] private Slider width;
    [SerializeField] private Slider height;
    [SerializeField] private TextMeshProUGUI lengthLabel;
    [SerializeField] private TextMeshProUGUI widthLabel;
    [SerializeField] private TextMeshProUGUI heigthLabel;
    [SerializeField] private Button createBtn;

    [SerializeField] private SpawnFurniture spawnFunc;

    // Start is called before the first frame update
    void Start()
    {
        length.onValueChanged.AddListener(ctx => changeLabelValue(lengthLabel,length.value));
        width.onValueChanged.AddListener(ctx => changeLabelValue(widthLabel,width.value));
        height.onValueChanged.AddListener(ctx => changeLabelValue(heigthLabel,height.value));
        createBtn.onClick.AddListener(() => spawnFunc.Spawn(height.value/10, width.value/10,length.value/10));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void changeLabelValue(TextMeshProUGUI label, float value){
        label.text = value/10 + " m";
    }
}
