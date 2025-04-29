using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KS_UI_MarkableProgressBar_MarkCreater : MonoBehaviour
{
    public KS_UI_MarkableProgressBar_Controller controller;
    public GameObject markerPrefab;
    public Transform markerParent;
    public bool createOnStart = true;

    [Header("Number config")]
    public int perMarkIncrement = 1000;

    private void Awake() 
    {
        controller.onPreSetProgress += CreateMarks;
    }

    [ContextMenu("CreateMarks")]
    public void CreateMarks()
    {
        float totalDistance = controller.maxValue;
        float perMarkDistance = perMarkIncrement;

        int numMarks = Mathf.CeilToInt(totalDistance / perMarkDistance);

        for (int i = 0; i < numMarks - 1; i++)
        {
            GameObject newMarker = Instantiate(markerPrefab, markerParent);
            KS_UI_MarkableProgressBar_Marker marker = newMarker.GetComponent<KS_UI_MarkableProgressBar_Marker>();
            marker.value = perMarkDistance * (i + 1);
            marker.controller = controller;
            marker.gameObject.SetActive(true);
        }
    }
}
