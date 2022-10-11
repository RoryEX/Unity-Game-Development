using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BattleUnit", menuName = "BattleUnit/Create new BattleUnit")]
public class BattleBaseUnit : ScriptableObject
{
    [SerializeField] string name;
    [TextArea]
    [SerializeField] string descritpion;
    [SerializeField] Sprite playerSprite;
    [SerializeField] Sprite enemySprite;
    //[SerializeField] Sprite backSprite;

    [SerializeField] int maxHP;
    [SerializeField] int pAtk;
    [SerializeField] int mAtk;
    [SerializeField] int pDef;
    [SerializeField] int mDef;
    [SerializeField] int maxMP;
    [SerializeField] skType type1;
    [SerializeField] skType type2;
    [SerializeField] List<LearnableSkill> learnableSkills;
    //get properties
    public string Name
    {
        get { return name; }
    }
    public string Description
    {
        get { return descritpion; }
    }
    public Sprite PlayerSprite
    {
        get { return playerSprite; }
    }
    public Sprite EnemySprite
    {
        get { return enemySprite; }
    }
    public int MaxHP
    {
        get { return maxHP; }
    }
    public int PATK
    {
        get { return pAtk; }
    }
    public int MATK
    {
        get { return mAtk; }
    }
    public int PDEF
    {
        get { return pDef; }
    }
    public int MDEF
    {
        get { return mDef; }
    }
    public int MaxMP
    {
        get { return maxMP; }
    }
    public skType Tyep1
    {
        get{ return type1; }
    }
    public skType Tyep2
    {
        get { return type2; }
    }
    public enum skType 
    {
        None,
        Temp1,Temp2,Temp3
    }

    [System.Serializable]
    public class LearnableSkill
    {
        [SerializeField] SkillBase skillbase;
        [SerializeField] int level;

        public SkillBase Skill
        {
            get { return skillbase; }
        }
        public int Level
        {
            get { return level; }
        }
    }
    public List<LearnableSkill> LearnableSkills 
    {
        get { return learnableSkills; }
    }

   
}
