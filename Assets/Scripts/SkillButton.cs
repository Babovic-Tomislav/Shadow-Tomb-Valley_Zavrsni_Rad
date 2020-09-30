using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{

    public Button button;
    new public TMP_Text name;
    public Image image;
    public string description;
    public SkillObject skill;

    public void SetName(string nname)
    {
        this.name.text = nname;
    }

  

    public void SetSprite(Sprite spr)
    {
        this.image.sprite = spr;
    }

    public void SetDescription(string descript)
    {
        description = descript;
    }

    public void SetSkill(SkillObject newSkill)
    {
        skill = newSkill;
    }

}
