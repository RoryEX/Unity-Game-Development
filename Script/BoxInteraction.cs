using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BoxInteraction : MonoBehaviour
{
    // Start is called before the first frame update
    public Text npc1Text;
    public GameObject dialogPanel;
    public bool dialogFin;
    public bool dialogFirstTime;
    public bool interactWithPlayer;
    public bool shutDown;
    public GameObject laser1,laser2,laser3,laser4;
    //public bool canSpeak;
    public enum dialogState { dialog1, dialog2, dialog3, dialogFin };
    public dialogState Dstate;
    void Start()
    {
        Dstate = dialogState.dialog1;
        dialogFirstTime = true;
        shutDown = false;

        //canSpeak = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(interactWithPlayer);
        if (interactWithPlayer == true)
        {

            if (Input.GetKeyDown(KeyCode.Q))
            {
                Debug.Log("Q down");
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
        npc1Text.text = "Looks like this is the controller of the laser.";
        Dstate = dialogState.dialog2;
        yield return new WaitForSeconds(2f);
    }
    IEnumerator Dialog_2()
    {
        npc1Text.text = "Now let me shut down it.";
        Dstate = dialogState.dialog3;
        yield return new WaitForSeconds(2f);
    }
    IEnumerator Dialog_3()
    {
        npc1Text.text = "Great! Lasers are shut down. Time to save the innocences at left top!";
        Dstate = dialogState.dialogFin;
        Destroy(laser1);
        Destroy(laser2);
        Destroy(laser3);
        Destroy(laser4);
        shutDown = true;
        yield return new WaitForSeconds(2f);
    }
}
