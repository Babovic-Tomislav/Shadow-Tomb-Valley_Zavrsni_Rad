using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string unitName;
    public int unitLvl;
    public int damage;
    public int maxHP;
    public int currentHP;
    public int exp;




    public bool TakeDmg(int dmg)
    {
        this.currentHP -= dmg;

        if (this.currentHP <= 0)
            return true;
        else
            return false;
    }

    public void HealUnit(int healAmount)
    {
        this.currentHP += healAmount;
        if (this.currentHP>this.maxHP)
        {
            this.currentHP = this.maxHP;
        }
    }


    

}
