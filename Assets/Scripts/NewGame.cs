using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class NewGame : MonoBehaviour
{

    public void SetResolution(TMP_Text text)
    {
       
        if (text.text=="1920x1080")
        {
            Screen.SetResolution(1920, 1080, true);
        }
        else if ( text.text =="1600x900")
        {
            Screen.SetResolution(1600, 900, true);
        }
        else
        {
            Screen.SetResolution(960,540, true);
        }
    }
    // Start is called before the first frame update
   
    public void Quit()
    {
        Application.Quit();
    }

    public void SetUpGame()
    {
        PlayerUnit.expForLvlUp = 46;
        PlayerUnit.pickUpClass = false;
        PlayerUnit.unitName = "Hero";
        PlayerUnit.unitLvl = 1;
        PlayerUnit.damage = 12;
        PlayerUnit.maxHP = 50;
        PlayerUnit.currentHP = 50;
        PlayerUnit.exp = 0;
        PlayerUnit.manaAmount = 20;
        PlayerUnit.maxMana = 20;
        PlayerUnit.time = 0f ;
        PlayerUnit.enemiesKilled = 0;
        PlayerUnit.bossFight = false;
        Time.timeScale = 1;


        Inventory.playerInventory = new List<ItemObject>();
        Inventory.playerSkills = new List<SkillObject>();
        Inventory.playerClass=null;
        Inventory.equipedItems = new Dictionary<string, EquipmentItem>();

        Inventory.goldAmount = 0;
        Inventory.itemQuantity = new List<int>();
        DestroyOnLoad.boss = false;
        DestroyOnLoad.box = false;

        PlayerPrefs.SetString("_last_scene_", SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("PlayerHouse", LoadSceneMode.Single);
    }

}
