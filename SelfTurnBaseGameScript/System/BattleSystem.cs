using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class BattleSystem : MonoBehaviour
{
    [SerializeField] inBattleUnit playerUnit;
    [SerializeField] BattleHUD playerHUD;
    [SerializeField] inBattleUnit enemyUnit;
    [SerializeField] BattleHUD enemyHUD;
    [SerializeField] BattleDialogBox dialogBox;
    [SerializeField] List<AudioSource> audios;
    int random;

    public event Action<bool> onBattleOver;
    public enum Battlestates { Start,ActionSelection,PlayerMove,EnemyMove,PerformingMove,Result,LvUp,BattleOver,PlayerBusy };

    Battlestates state;
    int currentSkill;
    public void StartBattle()
    {
        random = UnityEngine.Random.Range(0, 4);
        audios[random].Play();
        StartCoroutine(SetBattle());
        
    }
    public IEnumerator SetBattle()
    {
        playerUnit.SetUp();
        enemyUnit.SetUp();
        playerHUD.setdata(playerUnit.BattleUnit);
        enemyHUD.setdata(enemyUnit.BattleUnit);
        dialogBox.SetSkillNames(playerUnit.BattleUnit.Skills);

        yield return dialogBox.TypeDialog($"There is {enemyUnit.BattleUnit.Base.Name} encounter!");
        yield return new WaitForSeconds(1);
        PlayerActionSelection();
    }
    void PlayerActionSelection()
    {
        state = Battlestates.ActionSelection;
        StartCoroutine(dialogBox.TypeDialog("What you want do?"));
        dialogBox.EnableSelector(true);
        
    }
    public void HandleUpdate()
    {
       
    }
    public void OpenSkillPanel()
    {
        state = Battlestates.PlayerMove;
        dialogBox.EnableSkillSelector(true);
        dialogBox.EnableItemBox(false);
    }
    public void OpenItemPanel()
    {
        dialogBox.EnableSkillSelector(false);
        dialogBox.EnableItemBox(true);
    }
    public void OnSkillOne()
    {
        dialogBox.EnableSkillSelector(false);
        dialogBox.EnableItemBox(false);
        currentSkill = 0;
        StartCoroutine(PlayerMoveTurn());
    }
    public void OnSkillTwo()
    {
        dialogBox.EnableSkillSelector(false);
        dialogBox.EnableItemBox(false);
        currentSkill = 1;
        StartCoroutine(PlayerMoveTurn());
    }
    public void OnSkillThree()
    {
        dialogBox.EnableSkillSelector(false);
        dialogBox.EnableItemBox(false);
        currentSkill = 2;
        StartCoroutine(PlayerMoveTurn());
    }
    public void OnSkillFour()
    {
        dialogBox.EnableSkillSelector(false);
        dialogBox.EnableItemBox(false);
        currentSkill = 3;
        StartCoroutine(PlayerMoveTurn());
    }
    public void OnItemHeal()
    {
        dialogBox.EnableSkillSelector(false);
        dialogBox.EnableItemBox(false);
        playerUnit.BattleUnit.currentHP += 20;
        playerHUD.UpdateHP();
        StartCoroutine(dialogBox.TypeDialog("Healing!"));
       
        StartCoroutine(PlayerItemTurn());
    }
    public void OnItemBooster()
    {
        dialogBox.EnableSkillSelector(false);
        dialogBox.EnableItemBox(false);
        playerUnit.BattleUnit.currentAtk += 10;
        StartCoroutine(dialogBox.TypeDialog("Atk Booster!"));
        
        StartCoroutine(PlayerItemTurn());
    }
    public void OnItemDebuff()
    {
        dialogBox.EnableSkillSelector(false);
        dialogBox.EnableItemBox(false);
        enemyUnit.BattleUnit.currentDef -= 10;
        StartCoroutine(dialogBox.TypeDialog("Enemy defence buff!"));
       
        StartCoroutine(PlayerItemTurn());
    }
    public void OnDefence()
    {
        dialogBox.EnableSkillSelector(false);
        dialogBox.EnableItemBox(false);
        playerUnit.BattleUnit.currentDef += 10;
        StartCoroutine(dialogBox.TypeDialog("Defence up!"));
        

        StartCoroutine(PlayerItemTurn());
    }
    public void OnItemSound()
    {
        dialogBox.EnableSkillSelector(false);
        dialogBox.EnableItemBox(false);
        audios[random].Stop();
        audios[UnityEngine.Random.Range(0, 4)].Play();
        enemyUnit.BattleUnit.currentDef -= 10;
        enemyUnit.BattleUnit.currentAtk -= 10;
        StartCoroutine(dialogBox.TypeDialog("BGM Change! Enemy is confused!"));
        
        StartCoroutine(PlayerItemTurn());
    }
    IEnumerator PlayerItemTurn()
    {
        state = Battlestates.PlayerBusy;
        yield return new WaitForSeconds(2);
        StartCoroutine(EnemyMove());
    }
    IEnumerator PlayerMoveTurn()
    {
        state = Battlestates.PerformingMove;

        var skill = playerUnit.BattleUnit.Skills[currentSkill];
        yield return dialogBox.TypeDialog($"{playerUnit.BattleUnit.Base.Name} used {skill.BaseSkill.Name}");

        yield return new WaitForSeconds(1);
        bool isFainted = false;
        bool turnOver = false;
        if (playerUnit.BattleUnit.currentMP >= 0)
        {
            isFainted  = enemyUnit.BattleUnit.TakeDamageP(skill, playerUnit.BattleUnit);
            if(skill.BaseSkill.name== "JusticePanishment" && playerUnit.BattleUnit.currentHP<= playerUnit.BattleUnit.MaxHP)
            {
                playerUnit.BattleUnit.currentHP += 10;
                playerHUD.UpdateHP();
                yield return dialogBox.TypeDialog("Heal in battle! You absorb enemy's life!");
            }
            else if(skill.BaseSkill.name == "BeyondSlash")
            {
                playerUnit.BattleUnit.currentHP -= 20;
                playerHUD.UpdateHP();
                yield return dialogBox.TypeDialog("Nice attack, but heart your self!");
            }
            turnOver = true;
        }
        else
        {
            yield return dialogBox.TypeDialog("Not enough Mana,choose another action.");
            state = Battlestates.ActionSelection;
            turnOver = false;

        }
        enemyHUD.UpdateHP();
        playerHUD.UpadteMP();
        if (isFainted)
        {

            yield return dialogBox.TypeDialog($"{enemyUnit.BattleUnit.Base.Name} Fainted");
            yield return new WaitForSeconds(2f);
            StartCoroutine(Result());
        }
        else if(!isFainted && turnOver)
        {
            StartCoroutine(EnemyMove());
        }
    }

    IEnumerator EnemyMove()
    {
        state = Battlestates.EnemyMove;
        var move = enemyUnit.BattleUnit.GetRandomMove();

        yield return dialogBox.TypeDialog($"{enemyUnit.BattleUnit.Base.Name} used {move.BaseSkill.Name}");

        yield return new WaitForSeconds(1);
        bool isFainted = playerUnit.BattleUnit.TakeDamageP(move, enemyUnit.BattleUnit);
        playerHUD.UpdateHP();
        enemyHUD.UpadteMP();
        if (isFainted)
        {
            yield return dialogBox.TypeDialog($"{playerUnit.BattleUnit.Base.Name} Fainted");
            yield return new WaitForSeconds(2f);
            
            audios[random].Stop();
            
            yield return dialogBox.TypeDialog("Game restart in 2s.");
            yield return new WaitForSeconds(2f);
            SceneManager.LoadScene("FantaTwon");

        }
        else
        {
            PlayerActionSelection();
        }
        
        
    }
    IEnumerator Result()
    {
        state = Battlestates.Result;
        StartCoroutine(dialogBox.TypeDialog($"You defeated the enemy! Gain {enemyUnit.BattleUnit.dfExp} exp"));
        playerUnit.BattleUnit.GainExp(playerUnit);
        yield return new WaitForSeconds(3f);
        if (playerUnit.exp >= 100)
        {
            StartCoroutine(LevelUP());
        }
        yield return new WaitForSeconds(3f);
        onBattleOver(true);
        audios[random].Stop();
    }
    IEnumerator LevelUP()
    {
        state = Battlestates.LvUp;
        playerUnit.BattleUnit.LevelUp(playerUnit);
        StartCoroutine(dialogBox.TypeDialog("Level up!"));
        yield return new WaitForSeconds(3f);
        onBattleOver(true);
        audios[random].Stop();
    }
    public void TryToEscape()
    {
        state = Battlestates.PerformingMove;
        StartCoroutine(dialogBox.TypeDialog("Ran away Safely!"));
        StartCoroutine(RunSuccessfully());
    }
    IEnumerator RunSuccessfully()
    {
        state = Battlestates.BattleOver;
        yield return new WaitForSeconds(2f);
        onBattleOver(true);
        audios[random].Stop();
    }
}
