using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Gamestate { FreeRoam,Battle };
public class GameController : MonoBehaviour
{
    [SerializeField] PlayerMovement playerController;
    [SerializeField] BattleSystem battleSystem;
    [SerializeField] Camera worldCamera;
    [SerializeField] AudioSource audio;
    [SerializeField] MapArea maze1, maze2;
    [SerializeField] GameObject panel;
    [SerializeField] Text dialogText;
    int random;
    Gamestate state;
    private void Start()
    {
        audio.Play();
        panel.SetActive(true);
        StartCoroutine(TypeDialog("Welcome to EverNight town! This is a part of your dream.Don't be nervous.Enjoy and find way out!(Press X to close the dialog panel)"));
        playerController.onEncountered += StartBattle;
        battleSystem.onBattleOver += EndBattle;
    }
    void StartBattle()
    {
        state = Gamestate.Battle;
        battleSystem.gameObject.SetActive(true);
        worldCamera.gameObject.SetActive(false);

        battleSystem.StartBattle();
    }
    void EndBattle(bool won)
    {
        state = Gamestate.FreeRoam;
        battleSystem.gameObject.SetActive(false);
        worldCamera.gameObject.SetActive(true);
        audio.Play();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (state == Gamestate.FreeRoam)
        {
            playerController.HandleUpdate();
        }
        else if(state== Gamestate.Battle)
        {
            battleSystem.HandleUpdate();
            audio.Stop();
        }
    }
    public IEnumerator TypeDialog(string dialog)
    {
        dialogText.text = "";

        foreach (var letter in dialog.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(1f / 30);
        }
    }
}
