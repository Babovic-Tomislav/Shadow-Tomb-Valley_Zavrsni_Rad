using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public abstract class SkillObject : ScriptableObject
{
    public string skillName = "new skill";
    public int skillDmg;
    public string description;
    public Sprite img;
    public int lvlReq;
    public int manaCost;
}
