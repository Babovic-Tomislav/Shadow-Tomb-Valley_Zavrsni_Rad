using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;




public class Vendor : NPC
{
    public GameObject buySellPrompt;
    public List<Button> sellableItems;
    public List<Button> merchandise ;
    public List<Button> merchandiseCopy;
    
    public Button itemButtonBuy;
    public Button itemButtonSell;
    Navigation navigation;
    CustomScroll scroll;
    EventSystem eventSystem;
    public Button back;
    string activeDialogue;
    public GameObject buySellPanel;

    [Header("Stats")]
    public TMP_Text name;
    public TMP_Text lvl;
    public TMP_Text hp;
    public TMP_Text mana;
    public TMP_Text dmg;
    public TMP_Text gold;
    public TMP_Text exp;
    // Start is called before the first frame update
    private void Awake()
    {
        
        dialogue.Add("Hello adventurer.\n How can i help you?");
        dialogue.Add("Anything else?");
        dialogue.Add("Thank you, come again.");
        dialogue.Add("Maybe next time then.");
        
    }
    public void OnBuyButton()
    {
        
        ChangeStats();

        SetUpMerchandise();


    }

    public void OnSellButton()
    {
        ChangeStats();
        SetUpSellableItems();
        
    }
   
    private void Start()
    {
        
        scroll = CustomScroll.scroll;
        eventSystem = EventSystem.current;
        

    }
    // Update is called once per frame
    void Update()
    {
       

        if (!eventSystem.currentSelectedGameObject) return;

        if (buySellPanel.gameObject.activeInHierarchy)
            
        if (eventSystem.currentSelectedGameObject.GetComponent<ItemButton>() != null)
        {
            if (eventSystem.currentSelectedGameObject.GetComponent<ItemButton>().item.type == ItemObject.ItemType.Equipment)
            {
                Resources.Load<EquipmentItem>("Items/" + eventSystem.currentSelectedGameObject.GetComponent<ItemButton>().item.itemName).StatChange();

            }
            else
            {
                EquipmentStatDifferenceShow.equipmentStatDifferenceShow.hpArrow.SetActive(false);
                EquipmentStatDifferenceShow.equipmentStatDifferenceShow.hpChange.gameObject.SetActive(false);
                EquipmentStatDifferenceShow.equipmentStatDifferenceShow.dmgArrow.SetActive(false);
                EquipmentStatDifferenceShow.equipmentStatDifferenceShow.dmgChange.gameObject.SetActive(false);
            }
            if (merchandise.Contains(eventSystem.currentSelectedGameObject.GetComponent<Button>()))
                scroll.Scrolling(merchandise, eventSystem.currentSelectedGameObject.GetComponentInParent<ScrollRect>());
            else if (sellableItems.Contains(eventSystem.currentSelectedGameObject.GetComponent<Button>()))
                scroll.Scrolling(sellableItems, eventSystem.currentSelectedGameObject.GetComponentInParent<ScrollRect>());
            else
                scroll.Scrolling(merchandiseCopy, eventSystem.currentSelectedGameObject.GetComponentInParent<ScrollRect>());
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

    public void SetUpMerchandise()
    {
        foreach (var item in merchandise)
        {
            Destroy(item.gameObject);
        }
        foreach(var item in merchandiseCopy)
        {
            Destroy(item.gameObject);
        }
        merchandise = new List<Button>();
        merchandiseCopy = new List<Button>();
        ItemObject[] items;
        items = Resources.LoadAll<ItemObject>("Items");
        foreach(var item in items)
        {
            Button button = Instantiate(itemButtonBuy) as Button;
            button.gameObject.SetActive(true);
            

            button.name = item.itemName;
            button.GetComponent<ItemButton>().SetName(item.itemName);
            button.GetComponent<ItemButton>().SetPrice(item.price);
            button.GetComponent<ItemButton>().SetSprite(item.sprit);
            button.GetComponent<ItemButton>().SetDescription(item.description);
            button.GetComponent<ItemButton>().SetItem(item);
            button.transform.SetParent(itemButtonBuy.transform.parent, false);
            merchandise.Add(button);

            Button button2 = button.GetComponentsInChildren<Button>()[1];
            button2.name = item.itemName+"(Clone)";
            button2.GetComponent<ItemButton>().SetName(item.itemName);
            button2.GetComponent<ItemButton>().SetPrice(item.price);
            button2.GetComponent<ItemButton>().SetSprite(item.sprit);
            button2.GetComponent<ItemButton>().SetDescription(item.description);
            button2.GetComponent<ItemButton>().SetItem(item);
            button2.gameObject.SetActive(true);
            var color = button2.colors;
            color.selectedColor = Color.gray;
            color.pressedColor = Color.gray;
            button2.colors = color;
            


            merchandiseCopy.Add(button2);


            

        }
        SetMerchandiseNavigation();
    }

    public void SetUpSellableItems()
    {
        foreach(var item in sellableItems)
        {
            Destroy(item.gameObject);
        }
        sellableItems = new List<Button>();
        int j = 0;
        foreach(var item in Inventory.playerInventory)
        {
            
            Button button = Instantiate(itemButtonSell) as Button;
            button.gameObject.SetActive(true);

            button.name = item.itemName;
            button.GetComponent<ItemButton>().SetName(item.itemName);
            button.GetComponent<ItemButton>().SetQuantity(Inventory.itemQuantity[j]);
            button.GetComponent<ItemButton>().SetSprite(item.sprit);
            button.GetComponent<ItemButton>().SetDescription(item.description);
            button.GetComponent<ItemButton>().SetItem(item);
            button.transform.SetParent(itemButtonSell.transform.parent, false);
            sellableItems.Add(button);
            j++;
            int i = sellableItems.Count;

            if (i > 1)
            {

                navigation = button.navigation;
                navigation.mode = Navigation.Mode.Explicit;
                navigation.selectOnLeft = sellableItems[i - 2];
                sellableItems[i - 1].navigation = navigation;
                navigation = sellableItems[i - 2].navigation;
                navigation.mode = Navigation.Mode.Explicit;
                navigation.selectOnRight = sellableItems[i - 1];
                sellableItems[i - 2].navigation = navigation;
                if (i > 2)
                {
                    navigation = button.navigation;
                    navigation.mode = Navigation.Mode.Explicit;
                    navigation.selectOnUp = sellableItems[i - 3];
                    sellableItems[i - 1].navigation = navigation;
                    navigation = sellableItems[i - 3].navigation;
                    navigation.mode = Navigation.Mode.Explicit;
                    navigation.selectOnDown = sellableItems[i - 1];
                    sellableItems[i - 3].navigation = navigation;
                }
            }
            if(i==sellableItems.Count)
            {
                navigation = button.navigation;
                navigation.mode = Navigation.Mode.Explicit;
                navigation.selectOnRight = back;
                sellableItems[i - 1].navigation = navigation;
            }
        }
        
    }

    public void DeactivateButtons(bool buySell)
    {
        if(buySell)
        {
            for(int i =0; i<sellableItems.Count;i++)
            {
                sellableItems[i].interactable = false;
            }
        }else
        for(int i =0; i< merchandise.Count;i++)
        {
            merchandise[i].interactable = false;
            merchandiseCopy[i].interactable = false;
        }
    }

    public void ActivateButtons(bool buySell)
    {
        if (buySell)
        {
            for (int i = 0; i < sellableItems.Count; i++)
            {
                sellableItems[i].interactable = true;
            }
        }
        else
            for (int i = 0; i < merchandise.Count; i++)
            {
                merchandise[i].interactable = true;
                merchandiseCopy[i].interactable = true;
            }
    }


    public override void SetDialogue(int i = 0)
    {

        base.SetDialogue(i);
        if (i == 1)
        {


        }
        dialoguePanel.GetComponentInChildren<TMP_Text>().text = dialogue[i];
        activeDialogue = dialogue[i];
        if (i == 0) this.triggerNpc = false;
        else this.triggerNpc = true;
    }

    public void SellItem()
    {
        Button item = LastSelectedButton.item;
        ItemObject itemItem = item.GetComponent<ItemButton>().item;
        Inventory.itemQuantity[Inventory.playerInventory.IndexOf(itemItem)]--;
        if (Inventory.itemQuantity[Inventory.playerInventory.IndexOf(itemItem)]==0)
        {
            Inventory.itemQuantity.Remove(Inventory.playerInventory.IndexOf(itemItem));
            Inventory.playerInventory.Remove(itemItem);
        }
        LastSelectedButton.item = back;
        Inventory.goldAmount += itemItem.price / 2;
        SetUpSellableItems();
        ChangeStats();
    }

    public void BuyItem()
    {
        
        Inventory.AddItem(LastSelectedButton.item.GetComponent<ItemButton>().item);
        Inventory.goldAmount -= LastSelectedButton.item.GetComponent<ItemButton>().item.price;
        SetUpSellableItems();
        SetUpMerchandise();
        ChangeStats();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            this.triggerNpc = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            this.triggerNpc = false;
        }

    }

    public void SelectFirstItem()
    {

        
        if (merchandise[0].interactable)
        {
            merchandise[0].Select();
        }
            
        else
        {
            merchandiseCopy[0].Select();
        }
    }

    public void SelectFirstSellItem()
    {
        if (sellableItems.Count != 0)
        {
            sellableItems[0].Select();
            sellableItems[0].OnSelect(null);
        }
        else
        {
            back.Select();
            back.OnSelect(null);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (this.triggerNpc && Input.GetButtonDown("Submit") && collision.CompareTag("Player"))
        {
            StartCoroutine(SetChat());




        }
    }
    IEnumerator SetChat()
    {
        yield return new WaitForEndOfFrame();
        Time.timeScale = 0;
        if (!dialoguePanel.activeInHierarchy)
        {


            SetDialogue();





        }
        yield return new WaitForSecondsRealtime(1f);
        buySellPrompt.SetActive(true);
        EventSystem.current.SetSelectedGameObject(dialoguePanel.GetComponentInChildren<Button>().gameObject);
        EventSystem.current.currentSelectedGameObject.GetComponent<Button>().OnSelect(null);
        while (buySellPrompt.activeInHierarchy)
        {
            yield return new WaitForSecondsRealtime(1);

        }

        while(activeDialogue==dialogue[1] || activeDialogue==dialogue[0])
        {

            yield return new WaitForSecondsRealtime(2f);
        }


        while (!Input.GetKeyDown(KeyCode.Space))
        {

            yield return null;
        }


        EndDialogue();
        yield return null;
    }

    public void SetMerchandiseNavigation()
    {
        
        for (int i = 0; i < merchandise.Count; i++)
        {
            merchandise[i].GetComponent<Button>().interactable = true;

            
            if (merchandise[i].GetComponent<ItemButton>().item.price > Inventory.goldAmount)
            {
                
                merchandise[i].GetComponent<Button>().interactable = false;
                merchandise[i].GetComponentsInChildren<Button>()[1].interactable = true;
                
            }
            else
                merchandise[i].GetComponentsInChildren<Button>()[1].interactable = false;
        }
        
            for (int i = 0; i < merchandise.Count; i++)
            {
                
                if (i > 0)
                {
                    if (merchandise[i].GetComponent<Button>().interactable)
                        navigation =merchandise[i].navigation;
                    else
                        navigation =merchandise[i].GetComponentsInChildren<Button>()[1].navigation;
                    navigation.mode = Navigation.Mode.Explicit;
                    if (merchandise[i-1].GetComponent<Button>().interactable)
                        navigation.selectOnLeft = merchandise[i-1];
                    else
                        navigation.selectOnLeft = merchandise[i-1].GetComponentsInChildren<Button>()[1];
                    if (merchandise[i].GetComponent<Button>().interactable)
                       merchandise[i].navigation = navigation;
                    else
                       merchandise[i].GetComponentsInChildren<Button>()[1].navigation = navigation;
                    if (merchandise[i-1].GetComponent<Button>().interactable)
                        navigation = merchandise[i-1].navigation;
                    else
                        navigation = merchandise[i-1].GetComponentsInChildren<Button>()[1].navigation;
                    navigation.mode = Navigation.Mode.Explicit;
                    if (merchandise[i].GetComponent<Button>().interactable)
                        navigation.selectOnRight =merchandise[i];
                    else
                        navigation.selectOnRight =merchandise[i].GetComponentsInChildren<Button>()[1];
                    if (merchandise[i-1].GetComponent<Button>().interactable)
                        merchandise[i-1].navigation = navigation;
                    else
                        merchandise[i-1].GetComponentsInChildren<Button>()[1].navigation = navigation;
                    if (i > 1)
                    {
                        if (merchandise[i].GetComponent<Button>().interactable)
                            navigation =merchandise[i].navigation;
                        else
                            navigation =merchandise[i].GetComponentsInChildren<Button>()[1].navigation;
                        navigation.mode = Navigation.Mode.Explicit;
                        if (merchandise[i-2].GetComponent<Button>().interactable)
                            navigation.selectOnUp = merchandise[i-2];
                        else
                            navigation.selectOnUp = merchandise[i-2].GetComponentsInChildren<Button>()[1];
                        if (merchandise[i].GetComponent<Button>().interactable)
                           merchandise[i].navigation = navigation;
                        else
                           merchandise[i].GetComponentsInChildren<Button>()[1].navigation = navigation;
                        if (merchandise[i-2].GetComponent<Button>().interactable)
                            navigation = merchandise[i-2].navigation;
                        else
                            navigation = merchandise[i-2].GetComponentsInChildren<Button>()[1].navigation;
                        navigation.mode = Navigation.Mode.Explicit;
                        if (merchandise[i].GetComponent<Button>().interactable)
                            navigation.selectOnDown =merchandise[i];
                        else
                            navigation.selectOnDown =merchandise[i].GetComponentsInChildren<Button>()[1];
                        if (merchandise[i-2].GetComponent<Button>().interactable)
                            merchandise[i-2].navigation = navigation;
                        else
                            merchandise[i-2].GetComponentsInChildren<Button>()[1].navigation = navigation;
                    }
                }
                if (i == merchandise.Count-1)
                {
                   
                    if (merchandise[i ].GetComponent<Button>().interactable)
                    {
                        
                        navigation =merchandise[i ].navigation;
                        navigation.mode = Navigation.Mode.Explicit;
                        navigation.selectOnRight = back;
                        merchandise[i ].navigation = navigation;
                    }
                    else
                    {
                        
                        navigation =merchandise[i ].GetComponentsInChildren<Button>()[1].navigation;
                        navigation.mode = Navigation.Mode.Explicit;
                        navigation.selectOnRight = back;
                        merchandise[i ].GetComponentsInChildren<Button>()[1].navigation = navigation;
                    }
                }
            
        }
    }

   

}
