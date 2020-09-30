using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;


public class ChangeFontColorOnSelect : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    void ISelectHandler.OnSelect(BaseEventData eventData)
    {
        gameObject.GetComponentInChildren<TMP_Text>().color = Color.green;


    }

    void IDeselectHandler.OnDeselect(BaseEventData eventData)
    {
        gameObject.GetComponentInChildren<TMP_Text>().color = Color.white;

    }
}
