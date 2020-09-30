using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public  class Inventory 
{
    public static List<ItemObject> playerInventory = new List<ItemObject>();
    public static List<SkillObject> playerSkills = new List<SkillObject>();
    public static ClassObject playerClass;
    public static Dictionary<string, EquipmentItem> equipedItems = new Dictionary<string, EquipmentItem>();

    public static int goldAmount=120;
    public static List<int> itemQuantity = new List<int>();

    
    

 

    public static void AddItem(ItemObject item, int amount = 1)
    {
        
        if(playerInventory.Contains(item))
        {
            
            itemQuantity[playerInventory.IndexOf(item)]++;
        }
        else
        {
            
            playerInventory.Add(item);
            itemQuantity.Add(amount);
        }
        
    }

    public static void AddSkill(SkillObject skill)
    {
        if(!playerSkills.Contains(skill))
        playerSkills.Add(skill);
        
    }
    
    public static void Equip(EquipmentItem newEquip)
    {
 
            
                
            if (equipedItems.ContainsKey(newEquip.eType.ToString()))
            {
                PlayerUnit.damage -= equipedItems[newEquip.eType.ToString()].dmg;
                PlayerUnit.maxHP -= equipedItems[newEquip.eType.ToString()].hp;
                PlayerUnit.currentHP -= equipedItems[newEquip.eType.ToString()].hp;
                AddItem(equipedItems[newEquip.eType.ToString()]);
                equipedItems.Remove(newEquip.eType.ToString());
                
            }
            equipedItems.Add(newEquip.eType.ToString(), newEquip);
            PlayerUnit.damage += equipedItems[newEquip.eType.ToString()].dmg;
            PlayerUnit.maxHP += equipedItems[newEquip.eType.ToString()].hp;
            PlayerUnit.currentHP += equipedItems[newEquip.eType.ToString()].hp;
        

        
        
    }

    public static void AddClass(ClassObject classObject)
    {
        playerClass = classObject;
        PlayerUnit.pickUpClass = false;
    }

   
    
}
