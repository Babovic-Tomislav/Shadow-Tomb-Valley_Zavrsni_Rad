using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PlayerUnit 
{
    public static float time=0;
    public static int enemiesKilled = 0;
    public static bool bossFight = false;
    [SerializeField]
    public static int expForLvlUp=46 ;
    public static bool pickUpClass = false;
    public static string unitName="Hero";
    public static int unitLvl=1;
    public static int damage=70;
    public static int maxHP=39;
    public static int currentHP=30;
    public static int exp=40;
    public static int manaAmount=15;
    public static int maxMana=15;
    


   

    public static void SetExpNeededForNextLvl()
    {
        expForLvlUp = (int)(100 * (Mathf.Pow(unitLvl+1,1.3f)) - 100 * (unitLvl+1));
    }


    public static void LvlUp()
    {
        unitLvl++;
        if (unitLvl == 2)
            pickUpClass = true;
        damage += UnityEngine.Random.Range(2, 5);
        var hp = UnityEngine.Random.Range(4, 7);
        maxHP += hp;
        currentHP += hp;
        var mana = UnityEngine.Random.Range(3, 7);
        manaAmount += mana;
        maxMana += mana;
        if (unitLvl == 2) return;
        foreach(var skill in Inventory.playerClass.skills)
        {
            if (skill.lvlReq == unitLvl)
                Inventory.AddSkill(skill);
        }
    }

    public static void SetExp(int xp)
    {
        exp += xp;
        if (exp >= expForLvlUp)
        {
            exp -= expForLvlUp;
            LvlUp();
            SetExpNeededForNextLvl();
        }

    }
    
    public static void SetMana(int newManaAmount)
    {
        manaAmount += newManaAmount;
        if (manaAmount < 0)
            manaAmount = 0;
        else if (manaAmount > maxMana)
            manaAmount = maxMana;
    }

    public static bool TakeDmg(int dmg)
    {
        currentHP -= dmg;

        if (currentHP <= 0)
            return true;
        else
            return false;
    }

    public static void HealUnit(int healAmount,int manaHeal)
    {
        currentHP += healAmount;
        if (currentHP > maxHP)
        {
            currentHP = maxHP;
        }

        manaAmount += manaHeal;
        if (manaAmount < 0)
            manaAmount = 0;
        else if (manaAmount > maxMana)
            manaAmount = maxMana;

    }

}
