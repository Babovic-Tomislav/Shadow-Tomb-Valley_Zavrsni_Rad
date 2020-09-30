using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public abstract class ItemObject : ScriptableObject
{
    public string itemName;

    public string description;
    public enum ItemType
    {
        Default,
        Equipment
    }
    public ItemType type;
    public Sprite sprit;
    public int price;


    public virtual void Use()
    {
        int index = Inventory.playerInventory.IndexOf(this);
        
        Inventory.itemQuantity[index]--;
        if (Inventory.itemQuantity[index] == 0)
        {
            Inventory.itemQuantity.RemoveAt(index);
            Inventory.playerInventory.RemoveAt(index);
        }
    }



}
