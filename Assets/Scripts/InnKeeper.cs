using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class InnKeeper : NPC
{
    public int nightSpendCost;
    public GameObject yesNoPrompt;
    public Image img;
    private void Awake()
    {
        
        dialogue.Add("Hello adventurer.\n Do u want to spend the night for 30g?");
        dialogue.Add("I hope You had a great night. See You again.");
        dialogue.Add("Not enought gold.");
        dialogue.Add("Maybe next time then.");
    }

    public override void SetDialogue(int i = 0)
    {
        base.SetDialogue(i);
        if (i == 1)
        {
            if (Inventory.goldAmount < nightSpendCost)
                i = 2;
            else
            {
                StartCoroutine(Wait());
                SpendNight();
            }
        }
        dialoguePanel.GetComponentInChildren<TMP_Text>().text = dialogue[i];
        if (i > 0) this.triggerNpc = true;
        else this.triggerNpc = false;
    }

    public void SpendNight()
    {
        img.GetComponentInChildren<Animator>().SetTrigger("Trigger");
        Inventory.goldAmount -= nightSpendCost;
        PlayerUnit.HealUnit(PlayerUnit.maxHP, PlayerUnit.maxMana);
    }

    IEnumerator Wait()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        dialoguePanel.GetComponentInChildren<TMP_Text>().text = dialogue[1];
        yield return null;
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
        yesNoPrompt.SetActive(true);
        EventSystem.current.SetSelectedGameObject(dialoguePanel.GetComponentInChildren<Button>().gameObject);
        EventSystem.current.currentSelectedGameObject.GetComponent<Button>().OnSelect(null);
        while (yesNoPrompt.activeInHierarchy)
        {
            yield return new WaitForSecondsRealtime(1);
            
        }
        while (!Input.GetKeyDown(KeyCode.Space))
        {
            yield return null;
        }
        EndDialogue();
        yield return null;
    }
}
