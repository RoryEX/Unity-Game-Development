using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{

	public GameObject playerPrefab;
	public GameObject enemyPrefab;

	public Transform playerBattleStation;
	public Transform enemyBattleStation;

	Unit playerUnit;
	Unit enemyUnit;

	public Text dialogueText;

	public BattleHUD playerHUD;
	public BattleHUD enemyHUD;

	public BattleState state;
	public bool isdead;

	// Start is called before the first frame update
	void Start()
    {
		state = BattleState.START;
		StartCoroutine(SetupBattle());
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
			Application.Quit();
        }
    }

    IEnumerator SetupBattle()
	{
		GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
		playerUnit = playerGO.GetComponent<Unit>();

		GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
		enemyUnit = enemyGO.GetComponent<Unit>();

		dialogueText.text = "A wild " + enemyUnit.unitName + " approaches...";

		playerHUD.SetHUD(playerUnit);
		enemyHUD.SetHUD(enemyUnit);

		yield return new WaitForSeconds(2f);

		state = BattleState.PLAYERTURN;
		PlayerTurn();
	}

	IEnumerator PlayerAttack()
	{
		
        if (enemyUnit.currentHP <= 0)
        {
			isdead = true;
        }
        else
        {
			isdead = false;

			if (enemyUnit.AC <= 0)
			{
				enemyUnit.TakeDamage(playerUnit.damage);
				enemyHUD.SetHP(enemyUnit.currentHP);
				dialogueText.text = "You take "+ playerUnit.damage+" danage to the enmey!";
			}
			else
			{
				
				dialogueText.text = "The Enemy denfense your attack!";
				enemyUnit.AC--;
			}
		}
		yield return new WaitForSeconds(2f);

		if(isdead)
		{
			state = BattleState.WON;
			EndBattle();
		} else
		{
			state = BattleState.ENEMYTURN;
			StartCoroutine(EnemyTurn());
		}
	}
	IEnumerator MagicAttack()
	{
		if (enemyUnit.currentHP <= 0)
		{
			isdead = true;
		}
		else
		{
			isdead = false;

			if (playerUnit.currentMp <= 0)
			{
				dialogueText.text = "Not enough MP to use Fire Ball!" ;
			}
			else 
			{
				enemyUnit.TakeDamage(playerUnit.mDamage);
				playerUnit.MagicCost(playerUnit.mpCost);
				enemyHUD.SetHP(enemyUnit.currentHP);
				playerHUD.SetMP(playerUnit.currentMp);
				dialogueText.text = "Fire ball takes "+ playerUnit.mDamage+" damage to the enemy!";
				
			}
		}
		yield return new WaitForSeconds(2f);

		if (isdead)
		{
			state = BattleState.WON;
			EndBattle();
		}
		else
		{
			state = BattleState.ENEMYTURN;
			StartCoroutine(EnemyTurn());
		}
	}
	IEnumerator PlayerDefence()
    {
		playerUnit.AC++;
		dialogueText.text =  "Your AC is Up!";
		yield return new WaitForSeconds(2f);

	}
	IEnumerator EnemyTurn()
	{
		if (playerUnit.currentHP <= 0)
		{
			isdead = true;
		}
		else
		{
			isdead = false;
			if (enemyUnit.currentHP >= 80)
			{
				if (playerUnit.AC <= 0)
				{
					dialogueText.text = enemyUnit.unitName + " attacks! Takes " + enemyUnit.damage + " damage!";
					playerUnit.TakeDamage(enemyUnit.damage);
					playerHUD.SetHP(playerUnit.currentHP);

				}
				else
				{
					dialogueText.text = "You avoid one attack!";

				}
			}
			else if (enemyUnit.currentHP < 60 && enemyUnit.currentMp > 30)
			{
				dialogueText.text = enemyUnit.unitName + " using Fire Ball! Takes " + enemyUnit.mDamage + " damages!";
				playerUnit.TakeDamage(enemyUnit.mDamage);
				enemyUnit.MagicCost(enemyUnit.mpCost);
				playerHUD.SetHP(playerUnit.currentHP);
				enemyHUD.SetMP(enemyUnit.currentMp);
			}
			else if (enemyUnit.currentHP <= 30 && enemyUnit.currentMp <= 30)
			{
				enemyUnit.AC++;
				dialogueText.text = enemyUnit.unitName + " is denfending! ";
			}
            else
            {
				int RandomState = Random.Range(0, 2);
                if (RandomState == 0)
                {
					dialogueText.text = enemyUnit.unitName + " attacks! Takes " + enemyUnit.damage + " damage!";
					playerUnit.TakeDamage(enemyUnit.damage);
					playerHUD.SetHP(playerUnit.currentHP);
				}
				else if (RandomState == 1)
                {
					dialogueText.text = enemyUnit.unitName + " using Fire Ball! Takes " + enemyUnit.mDamage + " damages!";
					playerUnit.TakeDamage(enemyUnit.mDamage);
					enemyUnit.MagicCost(enemyUnit.mpCost);
					playerHUD.SetHP(playerUnit.currentHP);
					enemyHUD.SetMP(enemyUnit.currentMp);
				}
				else if (RandomState == 2)
                {
					enemyUnit.AC++;
					dialogueText.text = enemyUnit.unitName + " is denfending! ";
				}
            }
		}	
		yield return new WaitForSeconds(1f);
		if(isdead)
		{
			state = BattleState.LOST;
			EndBattle();
		} else
		{
			state = BattleState.PLAYERTURN;
			PlayerTurn();
		}

	}

	void EndBattle()
	{
		if(state == BattleState.WON)
		{
			dialogueText.text = "You won the battle!";
		} else if (state == BattleState.LOST)
		{
			dialogueText.text = "You were defeated.";
		}
	}

	void PlayerTurn()
	{
		dialogueText.text = "Choose an action:";
	}

	IEnumerator PlayerHeal()
	{
		playerUnit.Heal(5);

		playerHUD.SetHP(playerUnit.currentHP);
		dialogueText.text = "You feel renewed strength!";

		yield return new WaitForSeconds(2f);

		state = BattleState.ENEMYTURN;
		StartCoroutine(EnemyTurn());
	}
	IEnumerator PlayerItemAC()
	{
		enemyUnit.AC--;
		dialogueText.text = "The enemy's AC is down!";

		yield return new WaitForSeconds(2f);

		state = BattleState.ENEMYTURN;
		StartCoroutine(EnemyTurn());
	}
	IEnumerator ChaosAtt()
	{
		if (enemyUnit.currentHP <= 0)
		{
			isdead = true;
		}
		else
		{
			isdead = false;
			if (playerUnit.currentMp >= 50)
            {
				isdead = false;
				playerUnit.currentMp -= 50;
				enemyUnit.currentHP -= 30;
				enemyUnit.currentMp -= 10;
				enemyUnit.AC+=2;
				enemyHUD.SetHP(enemyUnit.currentHP);
				enemyHUD.SetMP(enemyUnit.currentMp);
				playerHUD.SetMP(playerUnit.currentMp);
				dialogueText.text = "The enemy is in chaos! He attack himself!But his AC is higher";

            }
            else
            {
				dialogueText.text = "Mot enought MP!";
			}
			
		}

		yield return new WaitForSeconds(2f);

		if (isdead)
		{
			state = BattleState.WON;
			EndBattle();
		}
		else
		{
			state = BattleState.ENEMYTURN;
			StartCoroutine(EnemyTurn());
		}
	}

	public void OnAttackButton()
	{
		if (state != BattleState.PLAYERTURN)
			return;

		StartCoroutine(PlayerAttack());
	}
	public void OnMagicButton()
	{
		if (state != BattleState.PLAYERTURN)
			return;

		StartCoroutine(MagicAttack());
	}
	public void OnItemButton()
	{
		if (state != BattleState.PLAYERTURN)
			return;

		StartCoroutine(PlayerItemAC());
	}
	public void OnChaosButton()
	{
		if (state != BattleState.PLAYERTURN)
			return;

		StartCoroutine(ChaosAtt());
	}
	public void OnDenfenceButton()
	{
		if (state != BattleState.PLAYERTURN)
			return;

		StartCoroutine(PlayerDefence());
	}
	public void OnHealButton()
	{
		if (state != BattleState.PLAYERTURN)
			return;

		StartCoroutine(PlayerHeal());
	}

}
