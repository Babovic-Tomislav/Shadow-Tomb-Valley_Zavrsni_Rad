using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
[System.Serializable]
public class PlayerOptionsControl : MonoBehaviour
{

    [SerializeField] private Button itemButton;
    [SerializeField] private Button skillButton;

    public EventSystem eventSystem;
    public List<Button> inventoryButtons;
    public List<Button> skillButtons;
    Navigation navigation;
    CustomScroll scroll;
    public Button back;

    [Header("Stats")]
    public TMP_Text name;
    public TMP_Text lvl;
    public TMP_Text hp;
    public TMP_Text mana;
    public TMP_Text dmg;
    public TMP_Text gold;
    public TMP_Text exp;

    [Header("Equipment")]
    public TMP_Text hands;
    public TMP_Text head;
    public TMP_Text chest;
    public TMP_Text legs;

    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(gameObject.GetComponentInChildren<Button>().gameObject);
        EventSystem.current.currentSelectedGameObject.GetComponent<Button>().OnSelect(null);
        skillButtons = new List<Button>();
        SetUpItems();
        foreach (var skill in Inventory.playerSkills)
        {
            NewSkillButton(skill);
        }
        ChangeStats();
        OnEquip();
        ResetNavigation(inventoryButtons);
    }
    public void SetUpItems()
    {
        foreach (var item in inventoryButtons)
        {
            Destroy(item.gameObject);
        }
        int i = 0;
        inventoryButtons = new List<Button>();

        foreach (var item in Inventory.playerInventory)
        {
            NewItemButton(item, Inventory.itemQuantity[i]);
            i++;
        }
        ResetNavigation(inventoryButtons);
    }
    private void OnDisable()
    {
 
        
        foreach (var skill in skillButtons)
        {
            Destroy(skill.gameObject);
        }
        
    }

    void Start()
    {
        scroll = CustomScroll.scroll;
        eventSystem = EventSystem.current;
        

        
        

    }

    private void Update()
    {
        if (eventSystem != null)
        {
            if (!eventSystem.currentSelectedGameObject) return;
            if (eventSystem.currentSelectedGameObject.GetComponent<ItemButton>() != null)
            {
                if (eventSystem.currentSelectedGameObject.GetComponent<ItemButton>().item.type == ItemObject.ItemType.Equipment)
                {
                    Resources.Load<EquipmentItem>("Items/" + eventSystem.currentSelectedGameObject.
                    GetComponent<ItemButton>().item.itemName).StatChange();

                }
                else
                {
                    EquipmentStatDifferenceShow.equipmentStatDifferenceShow.hpArrow.SetActive(false);
                    EquipmentStatDifferenceShow.equipmentStatDifferenceShow.hpChange.gameObject.SetActive(false);
                    EquipmentStatDifferenceShow.equipmentStatDifferenceShow.dmgArrow.SetActive(false);
                    EquipmentStatDifferenceShow.equipmentStatDifferenceShow.dmgChange.gameObject.SetActive(false);
                }
                scroll.Scrolling(inventoryButtons, eventSystem.currentSelectedGameObject.GetComponentInParent<ScrollRect>());
            }
            else if (eventSystem.currentSelectedGameObject.GetComponent<SkillButton>())
                scroll.Scrolling(skillButtons, eventSystem.currentSelectedGameObject.GetComponentInParent<ScrollRect>());
        }
    }

    public void ChangeStats()
    {
        name.text = PlayerUnit.unitName;
        lvl.text = PlayerUnit.unitLvl.ToString();
        hp.text = PlayerUnit.currentHP.ToString() + "/" + PlayerUnit.maxHP.ToString();
        mana.text = PlayerUnit.manaAmount.ToString() + "/" + PlayerUnit.maxMana.ToString();
        dmg.text = PlayerUnit.damage.ToString();
        exp.text = PlayerUnit.exp.ToString() + "/" + PlayerUnit.expForLvlUp.ToString();
        gold.text = Inventory.goldAmount.ToString();
        
    }
    public void NewItemButton(ItemObject newItem, int amount)
    {
        
        Button button = Instantiate(itemButton) as Button;
        button.gameObject.SetActive(true);
        
        button.name = newItem.itemName;
        button.GetComponent<ItemButton>().SetName(newItem.itemName);
        button.GetComponent<ItemButton>().SetQuantity(amount);
        button.GetComponent<ItemButton>().SetSprite(newItem.sprit);
        button.GetComponent<ItemButton>().SetDescription(newItem.description);
        button.GetComponent<ItemButton>().SetItem(newItem);
        button.transform.SetParent(itemButton.transform.parent, false);
        inventoryButtons.Add(button);

        

    }

    public void NewSkillButton(SkillObject newSkill)
    {
        Button button = Instantiate(skillButton) as Button;
        button.gameObject.SetActive(true);

        button.GetComponent<SkillButton>().SetName(newSkill.skillName);

        button.GetComponent<SkillButton>().SetSprite(newSkill.img);
        button.GetComponent<SkillButton>().SetDescription(newSkill.description);
        button.GetComponent<SkillButton>().SetSkill(newSkill);
        button.transform.SetParent(skillButton.transform.parent, false);
        skillButtons.Add(button);

        

    }

    

    public void SelectFirstItem()
    {
        if (inventoryButtons.Count != 0)
            eventSystem.SetSelectedGameObject(inventoryButtons[0].gameObject);
        else
            eventSystem.SetSelectedGameObject(back.gameObject);
        eventSystem.currentSelectedGameObject.GetComponent<Button>().OnSelect(null);
    }

    public void SelectFirstSkill()
    {
        if (skillButtons.Count != 0)
        {
            eventSystem.SetSelectedGameObject(skillButtons[0].gameObject);
            eventSystem.currentSelectedGameObject.GetComponent<Button>().OnSelect(null);
        }
        else
            back.Select();
    }
   
    public void DeactivateButtons()
    {
        for(int i=0; i<inventoryButtons.Count;i++)
        {
            inventoryButtons[i].interactable = false;
        }
    }

    public void ActivateButtons()
    {
        for (int i = 0; i < inventoryButtons.Count; i++)
        {
            inventoryButtons[i].interactable = true;
        }
    }

    public Button ReturnFirstItemButton()
    {

        if (inventoryButtons.Count == 0)
            return null;
        return inventoryButtons[0];
    }

    public void ResetNavigation(List<Button> items)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (i > 0)
            {
                
                navigation = items[i ].GetComponent<Button>().navigation;
                navigation.mode = Navigation.Mode.Explicit;
                navigation.selectOnLeft = items[i - 1];
                items[i ].navigation = navigation;
                navigation = items[i - 1].navigation;
                navigation.mode = Navigation.Mode.Explicit;
                navigation.selectOnRight = items[i ];
                items[i - 1].navigation = navigation;
                if (i > 1)
                {
                    navigation = items[i ].GetComponent<Button>().navigation;
                    navigation.mode = Navigation.Mode.Explicit;
                    navigation.selectOnUp = items[i - 2];
                    items[i ].navigation = navigation;
                    navigation = items[i - 2].navigation;
                    navigation.mode = Navigation.Mode.Explicit;
                    navigation.selectOnDown = items[i ];
                    items[i - 2].navigation = navigation;
                }
            }
            if(i==items.Count-1)
            {
                
                navigation = items[i ].GetComponent<Button>().navigation;
                navigation.mode = Navigation.Mode.Explicit;
                navigation.selectOnRight = back;
                items[i].GetComponent<Button>().navigation = navigation;
            }
        }
    }

    public void OnItemUse()
    {
        SetUpItems();
        ChangeStats();
    }

    public void OnEquip()
    {
        if(Inventory.equipedItems.ContainsKey("Hands"))
        hands.text = Inventory.equipedItems["Hands"].itemName;
        if (Inventory.equipedItems.ContainsKey("Head"))
            head.text = Inventory.equipedItems["Head"].itemName;
        if (Inventory.equipedItems.ContainsKey("Chest"))
            chest.text = Inventory.equipedItems["Chest"].itemName;
        if (Inventory.equipedItems.ContainsKey("Legs"))
            legs.text = Inventory.equipedItems["Legs"].itemName;
        ChangeStats();
        SetUpItems();
    }

}