using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skills 
{
    public SkillBase BaseSkill { get; set; }
    public int usageMp { get; set; }

    public Skills(SkillBase sBase)
    {
        BaseSkill = sBase;
        usageMp = sBase.UsageMP;
        //initialize class
    }
}
