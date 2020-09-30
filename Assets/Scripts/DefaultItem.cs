using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/New default item")]

public class DefaultItem : ItemObject
{
    public int heal;
    public int mana;
    private void Awake()
    {
        type = ItemType.Default;   
    }

    public override void Use()
    {
        base.Use();
        PlayerUnit.HealUnit(this.heal,this.mana);
        

    }

}
