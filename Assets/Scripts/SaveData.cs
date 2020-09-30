using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
[System.Serializable]
public class SaveData
{

    public PlayerData playerData;
    public PlayerInventory playerInventory;
    public bool boxDestroy = DestroyOnLoad.box;
    public bool bossDestroy = DestroyOnLoad.boss; 
    [System.Serializable]
    public class PlayerData
        {
        public int expForLvlUp = PlayerUnit.expForLvlUp;
        public bool pickUpClass = PlayerUnit.pickUpClass;
        public string unitName = PlayerUnit.unitName;
        public int unitLvl = PlayerUnit.unitLvl;
        public int damage = PlayerUnit.damage;
        public int maxHP = PlayerUnit.maxHP;
        public int currentHP = PlayerUnit.currentHP;
        public int exp = PlayerUnit.exp;
        public int manaAmount = PlayerUnit.manaAmount;
        public int maxMana = PlayerUnit.maxMana;
        public int enemiesKilled = PlayerUnit.enemiesKilled;
        public bool bossFight = PlayerUnit.bossFight;
        public float time = Time.realtimeSinceStartup + PlayerUnit.time;
        public float playerPositionX;
        public float playerPositionY;
        public string sceneName;

        public void SetPositionAndScene(float x, float y)
        {
            this.playerPositionX = x;
            this.playerPositionY = y;
            this.sceneName = SceneManager.GetActiveScene().name;
           
        }
    }
 
    [System.Serializable]
    public class PlayerInventory
    {
        public int gold = Inventory.goldAmount;

        public List<string> playerItems=new List<string>();
        public List<string> playerSkills = new List<string>();
        public string playerClass;
        public List<string> playerEquip = new List<string>();
        public List<int> itemQuantity = new List<int>();

        public void SetUpInventorySave()
        {
            foreach (var item in Inventory.playerInventory)
            {
                this.playerItems.Add(item.itemName);
            }
            foreach(var skill in Inventory.playerSkills)
            {
                this.playerSkills.Add(skill.skillName);
            }

            this.itemQuantity = Inventory.itemQuantity;

            foreach( KeyValuePair<string,EquipmentItem> item in Inventory.equipedItems)
            {
                playerEquip.Add(item.Value.itemName);
            }
            if (Inventory.playerClass!=null)
            playerClass = Inventory.playerClass.cName;

        }




    }

    public void LoadGame()
    {
        DestroyOnLoad.box = this.boxDestroy;
        DestroyOnLoad.boss = this.bossDestroy;

        /*Load player stats*/
        PlayerUnit.expForLvlUp = this.playerData.expForLvlUp;
        PlayerUnit.pickUpClass = this.playerData.pickUpClass;
        PlayerUnit.unitName = this.playerData.unitName;
        PlayerUnit.unitLvl = this.playerData.unitLvl;
        PlayerUnit.damage = this.playerData.damage;
        PlayerUnit.maxHP = this.playerData.maxHP;
        PlayerUnit.currentHP = this.playerData.currentHP;
        PlayerUnit.exp = this.playerData.exp;
        PlayerUnit.manaAmount = this.playerData.manaAmount;
        PlayerUnit.maxMana = this.playerData.maxMana;
        PlayerUnit.enemiesKilled = this.playerData.enemiesKilled;
        PlayerUnit.time = this.playerData.time;
        PlayerUnit.bossFight = this.playerData.bossFight;


        /*Load inventory*/

        Inventory.goldAmount = this.playerInventory.gold;
        Inventory.itemQuantity = this.playerInventory.itemQuantity;

        foreach (var item in this.playerInventory.playerItems)
        {
           
            Inventory.AddItem(Resources.Load<ItemObject>("Items/" + item));
        }

        foreach(var skill in this.playerInventory.playerSkills)
        {
            Inventory.AddSkill(Resources.Load<SkillObject>("Skills/" + skill));
        }

        Inventory.AddClass(Resources.Load<ClassObject>("Classes/" + this.playerInventory.playerClass));

        foreach(var equip in this.playerInventory.playerEquip)
        {
            Inventory.Equip(Resources.Load<EquipmentItem>("Items/" + equip));
        }


       
        PlayerPrefs.SetFloat("x", this.playerData.playerPositionX);
        PlayerPrefs.SetFloat("y", this.playerData.playerPositionY);
        Time.timeScale = 1;
        PlayerPrefs.DeleteKey("_last_scene_");
        SceneManager.LoadScene(this.playerData.sceneName, LoadSceneMode.Single);
        
    }

}
