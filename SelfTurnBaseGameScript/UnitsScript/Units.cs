using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Units
{
    public BattleBaseUnit Base { get; set; }
    public int level;
    public int currentHP { get; set; }
    public int currentMP { get; set; }
    public int currentAtk { get; set; }

    public int currentDef { get; set; }

    public float exp;
    public float dfExp;
    public List<Skills> Skills { get; set; }

    public Units(BattleBaseUnit uBase, int ulevel)
    {
        Base = uBase;
        level = ulevel;
        currentHP = MaxHP;
        currentMP = MaxMp;
        currentAtk = PAttack;
        currentDef = PDefence;
        dfExp= Mathf.FloorToInt((Base.MaxMP * level) / 100f) + 5;
        Skills = new List<Skills>();
        foreach(var skill in Base.LearnableSkills)
        {
            if (skill.Level <= level)
            {
                Skills.Add(new Skills(skill.Skill));

            }
            if (Skills.Count >= 4)
            {
                break;
            }
        }
    }

    public int PAttack
    {
        get { return Mathf.FloorToInt((Base.PATK*level) / 100f)+5; }
    }
    public int MAttack
    {
        get { return Mathf.FloorToInt((Base.MATK * level) / 100f) + 5; }
    }
    public int PDefence
    {
        get { return Mathf.FloorToInt((Base.PDEF * level) / 100f) + 5; }
    }
    public int MDefence
    {
        get { return Mathf.FloorToInt((Base.MDEF * level) / 100f) + 5; }
    }
    public int MaxHP
    {
        get { return Mathf.FloorToInt((Base.MaxHP * level) / 100f) + 10; }
    }
    public int MaxMp
    {
        get { return Mathf.FloorToInt((Base.MaxMP * level) / 10f) + 5; }
    }

    public bool TakeDamageP(Skills skill,Units attacker)
    {
        float modifers = Random.Range(0.85f, 1f);
        float a = (2 * attacker.level + 10) / 250f;
        float d = a * skill.BaseSkill.Power * ((float)attacker.currentAtk / currentDef) + 2;
        int damage = Mathf.FloorToInt(d * modifers);

        currentHP -= damage;
        attacker.currentMP -= skill.usageMp;
        if (currentHP <= 0)
        {
            currentHP = 0;
            currentMP = 0;
            return true;
        }
        return false;
    }
   
    public Skills GetRandomMove()
    {
        int r = Random.Range(0, Skills.Count);
        return Skills[r];
    }
    public void GainExp(inBattleUnit player)
    {

        player.exp += dfExp;
        exp += dfExp;
    }
    public void LevelUp(inBattleUnit player)
    {
        player.level++;
        player.exp = 0;
        level++;
    }
}


