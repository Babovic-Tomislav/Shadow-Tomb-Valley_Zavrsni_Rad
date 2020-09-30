using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/New equipment item")]

public class EquipmentItem : ItemObject
{
    public enum EquipType
    {
        Head, Chest, Legs, Hands
    }
    
    public EquipType eType;

    public int dmg;
    public int hp;

    private void Awake()
    {
        type = ItemType.Equipment;
    }

    public override void Use()
    {
        base.Use();
        Inventory.Equip(this);
    }

    public void StatChange()
    {
        
        if (Inventory.equipedItems.ContainsKey(this.eType.ToString()))
            EquipmentStatDifferenceShow.equipmentStatDifferenceShow.ShowStatDifferences(this.hp, this.dmg, Inventory.equipedItems[this.eType.ToString()].hp, Inventory.equipedItems[this.eType.ToString()].dmg);
        else
            EquipmentStatDifferenceShow.equipmentStatDifferenceShow.ShowStatDifferences(this.hp, this.dmg);
    }

}
