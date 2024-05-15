using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DinamicRoomSize : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI widthLabel;
    [SerializeField] private TextMeshProUGUI lengthLabel;

    [SerializeField] private Slider width;
    [SerializeField] private Slider length;

    [SerializeField] private RectTransform preview;

    [SerializeField] private GameObject leftWall;
    [SerializeField] private GameObject rigthWall;
    [SerializeField] private GameObject topWall;
    [SerializeField] private GameObject bottomWall;


    private float currentRoomWidth;
    private float currentRoomLength;

    // Start is called before the first frame update
    void Start()
    {
        currentRoomWidth = rigthWall.transform.localPosition.x;
        currentRoomLength = topWall.transform.localPosition.z;
        length.onValueChanged.AddListener(ctx => changeLabelValue(lengthLabel,length.value));
        width.onValueChanged.AddListener(ctx => changeLabelValue(widthLabel,width.value));
    }

    // Update is called once per frame
    void Update()
    {
        changeRoomSize(width.value, length.value);
    }

    private void changeLabelValue(TextMeshProUGUI label, float value){
        label.text = 3 + (value/10) + " m";
    }

    private void changeRoomSize(float xValue, float zValue){
        preview.sizeDelta = new Vector2(30 + zValue, 30 + xValue);
        rigthWall.transform.position = new Vector3(currentRoomWidth + (xValue/20), rigthWall.transform.localPosition.y, rigthWall.transform.localPosition.z);
        leftWall.transform.position = new Vector3(-currentRoomWidth - (xValue/20), leftWall.transform.localPosition.y, leftWall.transform.localPosition.z);
        topWall.transform.position = new Vector3(topWall.transform.localPosition.x, topWall.transform.localPosition.y, currentRoomLength + (zValue/20));
        bottomWall.transform.position = new Vector3(bottomWall.transform.localPosition.x, bottomWall.transform.localPosition.y, -currentRoomLength - (zValue/20));
    }
}
