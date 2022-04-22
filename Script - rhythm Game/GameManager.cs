using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public AudioSource theMusic;

    public bool startPlaying;

    public BeatScroller theBS;

    public static GameManager instance;

    public int currentScore;
    public int scoreGoodNote = 100;
    public int scorePerfectNote = 150;
    public int scoreBadNote = 50;

    public Text scoreText;
    public Text combosText;

    public int maxCombo;
    public int currentCombo;
    public int everyMultCombo;
    public int Mulitplier;
    public int missNotes;
    public int life;

    public int Bads,Goods,Perfects;

    public GameObject resultScreen;
    public GameObject menuScreen;
    public GameObject fullComboEffect;
    public Text percenteageText, badsText, goodsText, perfectsText, missesText, rankText, finalScoreText;
    void Start()
    {
        instance = this;
        scoreText.text = "Score: 0";
        Mulitplier = 1;

        maxCombo = FindObjectsOfType<NoteObject>().Length;
        
    }

    
    void Update()
    {
        if (startPlaying)
        { 
            if (!theMusic.isPlaying && !resultScreen.activeSelf)
            {

                resultScreen.SetActive(true);

                badsText.text = "" + Bads;
                goodsText.text = "" + Goods;
                perfectsText.text = "" + Perfects;
                missesText.text = "" + missNotes;

                
                /*float percentageHit = ((maxCombo-missNotes) / maxCombo)*100;

                percenteageText.text = percentageHit.ToString("F1") + "%";

                string rankVal = "F";
                if (percentageHit <=100)
                {
                    rankVal = "SSS" + " You are a real Genius! And Full Combo with you!";
                    
                }
                else if(percentageHit >= 95)
                {
                    rankVal = "S";
                }
                else if (percentageHit >= 90)
                {
                    rankVal = "A";
                }
                else if (percentageHit >= 80)
                {
                    rankVal = "B";
                }
                else if (percentageHit >= 70)
                {
                    rankVal = "C";
                }
                else if (percentageHit >= 50)
                {
                    rankVal = "D";
                }
                else
                {
                    rankVal = "F";
                }*/
                if (missNotes == 0)
                {
                    Instantiate(fullComboEffect, resultScreen.transform.position, fullComboEffect.transform.rotation);
                }
                //rankText.text = rankVal;

                finalScoreText.text = currentScore.ToString();
            }
        }
    }

    public void NoteHit()
    {
        currentCombo++;
        everyMultCombo++;
        if (everyMultCombo>=5)
        {
            Mulitplier++;
            everyMultCombo = 0;
            if (Mulitplier >= 3)
            {
                Mulitplier = 3;
            }
            
        }
        
        scoreText.text = "Score:" + currentScore;
        combosText.text = currentCombo + "  Combos!";
    }
    public void GoodHit()
    {
        currentScore += scoreGoodNote * Mulitplier;
        NoteHit();
        Goods++;
    }
    public void PerfectHit()
    {
        currentScore += scorePerfectNote * Mulitplier;
        NoteHit();
        Perfects++;
    }
    public void BadHit()
    {
        currentScore += scoreBadNote * Mulitplier;
        NoteHit();
        Bads++;
    }
    public void NoteMiss()
    {
        Debug.Log("Miss!");
        missNotes++;
        Mulitplier = 0;
        currentCombo = 0;
    }
    public void CallMenu()
    {
        if (!menuScreen.activeSelf)
        {
            menuScreen.SetActive(true);
            theMusic.Pause();
        }
    }
    public void StartPlaySong()
    {
        if (!startPlaying)
        {
            startPlaying = true;
            theMusic.Play();
            theBS.hasStarted = true;
            menuScreen.SetActive(false);
        }
        
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void CloseMenu()
    {
        if (!theMusic.isPlaying&& startPlaying)
        {
            theMusic.UnPause();
        }
        menuScreen.SetActive(false);
    }
    public void ScrollSpeedx10()
    {
        theBS.beatTempo *= 1.1f;
    }
    public void ScrollSpeedx9()
    {
        theBS.beatTempo *= 1.09f;
    }
    public void ScrollSpeedx8()
    {
        theBS.beatTempo *= 1.08f;
    }
    public void ScrollSpeedx5()
    {
        theBS.beatTempo *= 1.05f;
    }
    public void ScrollSpeedx3()
    {
        theBS.beatTempo *= 1.03f;
    }
    public void ScrollSpeedx1()
    {
        theBS.beatTempo *= 1;
    }
}
