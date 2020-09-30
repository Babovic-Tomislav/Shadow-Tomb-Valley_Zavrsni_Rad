using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BossNpc : NPC
{
    private void Awake()
    {
        if (DestroyOnLoad.boss)
            Destroy(gameObject);
        dialogue.Add("Hello adventurer.");
        dialogue.Add("Do you realy think you can defeat me?");
        dialogue.Add("me, a shadow king.");
        dialogue.Add("Lets see");
    }

    public override void OnCollisionStay2D(Collision2D collision)
    {
        base.OnCollisionStay2D(collision);
    }

    public override void SetDialogue(int i = 0)
    {
        base.SetDialogue(i);
        dialoguePanel.GetComponentInChildren<TMP_Text>().text = dialogue[i];
    }

   

}
