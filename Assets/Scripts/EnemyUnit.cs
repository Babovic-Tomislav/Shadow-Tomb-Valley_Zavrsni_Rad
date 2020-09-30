using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : Unit
{
    public int gold;
    private void Awake()
    {
       
        
    }

    public void SetLvl(int lvl)
    {
        this.unitLvl = lvl;
    }

    public void SetStats(int lvl)
    {
        this.SetLvl(lvl);
        this.damage += Mathf.RoundToInt(1 + (this.unitLvl - 1) * 0.15f + (1 * (this.unitLvl - 1)));
        this.maxHP += Mathf.RoundToInt(1 + (this.unitLvl - 1) * 0.15f + (3 * (this.unitLvl - 1)));
        this.exp += Mathf.RoundToInt(1 + (this.unitLvl - 1) * 0.15f + (10 * (this.unitLvl - 1)));
        this.gold += Mathf.RoundToInt(1 + (this.unitLvl - 1) * 0.15f + (10 * (this.unitLvl - 1)));
        this.currentHP = maxHP;

    }
}
