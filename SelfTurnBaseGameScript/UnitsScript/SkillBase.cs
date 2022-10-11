using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill", menuName = "BattleUnit/Create new skill")]
public class SkillBase : ScriptableObject
{
    [SerializeField] string skillName;

    [TextArea]
    [SerializeField] string skillDescription;

    [SerializeField] int power;
    [SerializeField] int usageMP;
    [SerializeField] int accuracy;
    [SerializeField] BattleBaseUnit.skType type;

    public string Name
    {
        get { return skillName; }
    }
    public string Description
    {
        get { return skillDescription; }
    }

    public int Power
    {
        get { return power; }
    }
    public int UsageMP
    {
        get { return usageMP; }
    }
    public int Accuracy
    {
        get { return accuracy; }
    }

}
