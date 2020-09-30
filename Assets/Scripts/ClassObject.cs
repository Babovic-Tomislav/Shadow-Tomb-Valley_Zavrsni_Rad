using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public abstract class ClassObject : ScriptableObject
{
    public string cName = "New Classe";
    public List<SkillObject> skills = new List<SkillObject>();
    public int changeHP;
    public int changeDMG;
    public string description;

}
