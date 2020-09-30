using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LastSelectedButton : MonoBehaviour
{


    public Button button;
    public static Button item;
    public Button back;

    // Start is called before the first frame update

    public void SetLastSelectedButton(Button but)
    {
        button = but;

    }

    public void SetLastSelectedItem(Button but)
    {
        item = but;
    }

    public void SelectLastSelectedButton()
    {

        button.Select();
        button.OnSelect(null);

    }

    public void SelectLastSelectedItem()
    {
        if (item == null) item = back;
        item.Select();

    }

    public void UseLastSelectedItem()
    {
        item.GetComponent<ItemButton>().item.Use();
        item.GetComponent<ItemButton>().SetQuantity(-1);
        if (item.GetComponent<ItemButton>().quantity.text == "0")
            item = null;
        
        
        
    }

 



}
