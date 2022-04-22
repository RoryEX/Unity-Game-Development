using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{

	public string unitName;
	public int unitLevel;

	public int damage;
	public int mDamage;
	public int AC;
	public int mpCost;
	public int maxHP;
	public int currentHP;
	public int maxMp;
	public int currentMp;

	public void TakeDamage(int dmg)
	{
		currentHP -= dmg;
	}

	public void Heal(int amount)
	{
		currentHP += amount;
		if (currentHP > maxHP)
			currentHP = maxHP;
	}
	public void MagicCost(int mp)
    {
		currentMp -= mp;
    }
	public void Defence()
    {
		AC++;

	}
}
