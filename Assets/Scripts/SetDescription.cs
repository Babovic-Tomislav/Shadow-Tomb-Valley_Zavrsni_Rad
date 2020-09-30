using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;



public class SetDescription : MonoBehaviour
{
    public TMP_Text description;


    EventSystem eventSystem;

    private void Start()
    {
        eventSystem = EventSystem.current;
    }

    private void Update()
    {
        if (!eventSystem.currentSelectedGameObject)
        {
            
            return;
        }
        if (eventSystem.currentSelectedGameObject.GetComponent<ItemButton>() != null)
            description.text = eventSystem.currentSelectedGameObject.GetComponent<ItemButton>().description;
        else if (eventSystem.currentSelectedGameObject.GetComponent<SkillButton>() != null)
            description.text = eventSystem.currentSelectedGameObject.GetComponent<SkillButton>().description;
        else
        {
            description.text = "";
        }
    }

}
