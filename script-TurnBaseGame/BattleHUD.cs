using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{

	public Text nameText;
	public Text levelText;
	public Slider hpSlider;
	public Slider mpSlider;

	public void SetHUD(Unit unit)
	{
		nameText.text = unit.unitName;
		levelText.text = "Lv " + unit.unitLevel;
		hpSlider.maxValue = unit.maxHP;
		hpSlider.value = unit.currentHP;
		mpSlider.maxValue = unit.maxMp;
		mpSlider.value = unit.currentMp;
	}

	public void SetHP(int hp)
	{
		hpSlider.value = hp;
	}
	public void SetMP(int mp)
	{
		mpSlider.value = mp;
	}

}
