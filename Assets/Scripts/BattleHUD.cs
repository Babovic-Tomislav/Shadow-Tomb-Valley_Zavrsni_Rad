using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleHUD : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text levelText;
    public Slider hp;
    public Slider mana;

    public void SetEnemyHud(Unit unit)
    {
        nameText.gameObject.SetActive(true);
        levelText.gameObject.SetActive(true);
        hp.gameObject.SetActive(true);
        nameText.text = unit.unitName;
        levelText.text = "Lvl " + unit.unitLvl;
        hp.maxValue = unit.maxHP;
        hp.value = unit.currentHP;
        hp.GetComponentInChildren<TMP_Text>().text = hp.value.ToString() + "/" + hp.maxValue.ToString();
        
    }

    public void SetHUD()
    {
        nameText.gameObject.SetActive(true);
        levelText.gameObject.SetActive(true);
        hp.gameObject.SetActive(true);
        nameText.text = PlayerUnit.unitName;
        levelText.text = "Lvl " + PlayerUnit.unitLvl;
        hp.maxValue = PlayerUnit.maxHP;
        hp.value = PlayerUnit.currentHP;
        hp.GetComponentInChildren<TMP_Text>().text = hp.value.ToString() + "/" + hp.maxValue.ToString();
        if (mana!=null)
        {
            mana.gameObject.SetActive(true);
            mana.maxValue = PlayerUnit.maxMana;
            mana.value = PlayerUnit.manaAmount;
            mana.GetComponentInChildren<TMP_Text>().text = PlayerUnit.manaAmount.ToString() + "/" + PlayerUnit.maxMana.ToString();
        }

    }

    public void SetHP(int newHP)
    {
        hp.value = newHP;
        hp.GetComponentInChildren<TMP_Text>().text = hp.value.ToString() + "/" + hp.maxValue.ToString();
    }

    public void SetMana(int newMana)
    {
        mana.value = newMana;
        mana.GetComponentInChildren<TMP_Text>().text = newMana.ToString() + "/" + PlayerUnit.maxMana.ToString();
    }

    public void DestroyHud()
    {
        Destroy(nameText.gameObject);
        Destroy(levelText.gameObject);
        Destroy(hp.gameObject);
    }
}
