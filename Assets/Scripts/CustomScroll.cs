using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEditor;



public class CustomScroll : MonoBehaviour
{
    public static CustomScroll scroll;

    private void Awake()
    {
        scroll = this;
    }

 


    GameObject selected;


    public void Scrolling(List<Button> listOfButtons, ScrollRect scrollRect)
    {
        selected = EventSystem.current.currentSelectedGameObject;

    
        float scrollChange = (float) ((listOfButtons.IndexOf(selected.GetComponent<Button>()) / 2)) / Mathf.CeilToInt((float)((listOfButtons.Count - 6) / 2f));

        if (1 - scrollChange + (1f / ((int)((listOfButtons.Count - 4) / 2))) < 0)
            scrollRect.verticalScrollbar.value = 0;
        else if (1 - scrollChange + (1f / ((int)((listOfButtons.Count - 4) / 2))) > 1)
            scrollRect.verticalScrollbar.value = 1;
        else
            scrollRect.verticalScrollbar.value = 1 - scrollChange  +(1f / ((int)((listOfButtons.Count - 4) / 2)));


    }

}
