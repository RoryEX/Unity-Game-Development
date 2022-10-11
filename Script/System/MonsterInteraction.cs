using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterInteraction : MonoBehaviour
{
    // Start is called before the first frame update
    public Text npc1Text;
    public GameObject dialogPanel;
    public bool dialogFin;
    public bool dialogFirstTime;
    public bool interactWithPlayer;
    public bool fished;
    public string[] dialogs;
    
    public enum dialogState { dialog1, dialog2, dialog3, dialogFin };
    public dialogState Dstate;
    void Start()
    {
        Dstate = dialogState.dialog1;
        dialogFirstTime = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(interactWithPlayer);
        if (interactWithPlayer == true)
        {

            if (Input.GetKeyDown(KeyCode.Z))
            {
                Debug.Log("Z down");
                dialogPanel.SetActive(true);
                Debug.Log(dialogPanel.activeSelf);
                if (Dstate == dialogState.dialog1)
                {
                    StartCoroutine(Dialog_1());

                }
                else if (Dstate == dialogState.dialog2)
                {
                    StartCoroutine(Dialog_2());

                }
                else if (Dstate == dialogState.dialog3)
                {
                    StartCoroutine(Dialog_3());

                }
                else if (Dstate == dialogState.dialogFin)
                {
                    dialogFin = true;
                    dialogFirstTime = false;
                }
            }
            if (dialogFin)
            {
                dialogPanel.SetActive(false);
            }
            if (!dialogFirstTime)
            {
                Dstate = dialogState.dialog3;
            }

        }


    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag == "Player")
        {
            Debug.Log("Hit");
            interactWithPlayer = true;

        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.transform.tag == "Player")
        {
            interactWithPlayer = false;
            dialogPanel.SetActive(false);
            dialogFin = false;
        }
    }
    IEnumerator Dialog_1()
    {
        StartCoroutine(TypeDialog(dialogs[0]));
        Dstate = dialogState.dialog2;
        yield return new WaitForSeconds(2f);
    }
    IEnumerator Dialog_2()
    {
        StartCoroutine(TypeDialog(dialogs[1]));
        Dstate = dialogState.dialog3;
        yield return new WaitForSeconds(2f);
    }
    IEnumerator Dialog_3()
    {
        if (fished)
        {
            StartCoroutine(TypeDialog(dialogs[3]));
        }
        else
        {
            StartCoroutine(TypeDialog(dialogs[2]));
        }
        this.transform.position = new Vector3(11.75f, 9.5f, 0);
        Dstate = dialogState.dialogFin;
        yield return new WaitForSeconds(2f);
    }
    public IEnumerator TypeDialog(string dialog)
    {
        npc1Text.text = "";
        foreach (var letter in dialog.ToCharArray())
        {
            npc1Text.text += letter;
            yield return new WaitForSeconds(1f / 30);
        }
    }
}
