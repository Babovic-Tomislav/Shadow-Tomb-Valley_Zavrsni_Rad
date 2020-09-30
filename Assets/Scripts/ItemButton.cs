using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemButton : MonoBehaviour
{

    public Button button;
    public TMP_Text iName;
    public TMP_Text quantity;
    public Image image;
    public string description;
    public TMP_Text price;

    public ItemObject item;

    public void SetName(string nname)
    {
        this.iName.text = nname;
    }

    public void SetPrice(int pri)
    {
        this.price.text = pri.ToString();
    }

  

    public void SetQuantity(int q)
    {
        q = q + int.Parse( this.quantity.text.ToString());

        
            this.quantity.text = q.ToString();
        
        
    }

    public void SetSprite(Sprite spr)
    {
        this.image.sprite = spr;
    }
    public void SetDescription(string descript)
    {
        description = descript;
    }

    public void SetItem(ItemObject newItem)
    {
        
        item = newItem;
    }
    
    
    public void OnClick()
    {
        if (item.type == ItemObject.ItemType.Default)
        {
            GameObject.FindGameObjectWithTag("Equip").SetActive(false);
            GameObject.FindGameObjectWithTag("Use").GetComponentInChildren<Button>().Select();
        }
        else
        {
            GameObject.FindGameObjectWithTag("Use").SetActive(false);
            GameObject.FindGameObjectWithTag("Equip").GetComponentInChildren<Button>().Select();

        }
    }

    


}
