using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NPCInteraction : MonoBehaviour
{
    // Start is called before the first frame update
    public Text npc1Text;
    public GameObject dialogPanel;
    public bool dialogFin;
    public bool dialogFirstTime;
    public bool interactWithPlayer;
    public GameObject Boss;
    //public bool canSpeak;
    public enum dialogState { dialog1,dialog2,dialog3,dialogFin };
    public dialogState Dstate;
    void Start()
    {
        Dstate = dialogState.dialog1;
        dialogFirstTime = true;
        
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
                if (Dstate == dialogState.dialog1 )
                {
                    StartCoroutine(Dialog_1());
                   
                }
                else if (Dstate == dialogState.dialog2 )
                {
                    StartCoroutine(Dialog_2());
                    
                }
                else if(Dstate == dialogState.dialog3 )
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
        npc1Text.text = "Thanks god! You are finally here! I konw you want to destory this factory, but you need to save me first...";
        Dstate = dialogState.dialog2;
        yield return new WaitForSeconds(2f);
    }
    IEnumerator Dialog_2()
    {
        npc1Text.text = "Here is a hacker equip, and it will help you to de-power the ENDAI machine.";
        Dstate = dialogState.dialog3;
        yield return new WaitForSeconds(2f);
    }
    IEnumerator Dialog_3()
    {
        npc1Text.text = "Good Luck man! Hope you will be success!(Boss HP down!)";
        Dstate = dialogState.dialogFin;
        Boss.GetComponent<EnemyEntity>().health = 100;
        yield return new WaitForSeconds(2f);
    }
}
