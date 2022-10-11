using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LV2GameMangent : MonoBehaviour
{
    public float timer;
    public Text timerText;
    public GameObject PlayerObj;
    public bool destoryerActive;
    public GameObject chaseingEnemy;
    public Transform spwnPos;
    public Vector3 spOffset;
    public float spTimer;
    GameObject chaseEnemy;
    public AudioSource BGM;

    public GameObject Menu;
    public GameObject questPanle;
    // Start is called before the first frame update
    void Start()
    {
        destoryerActive = false;
        BGM.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Menu.SetActive(true);
        }
        spOffset = new Vector3(PlayerObj.transform.position.x-5, PlayerObj.transform.position.y+1, PlayerObj.transform.position.z);
        spwnPos.position = spOffset;
        if (destoryerActive)
        {
            timer -= Time.deltaTime;
            spTimer -= Time.deltaTime;
            timerText.text = "Time left:" + timer;
            if (spTimer <= 0)
            {
                chaseEnemy = Instantiate(chaseingEnemy, spwnPos.position, Quaternion.identity);
                chaseEnemy.GetComponent<FollowAI>().target = PlayerObj.transform;
                spTimer = 4;
            }
            
        }
        if (timer <= 0)
        {
            GameOver();
        }
   
    }
    void GameOver()
    {
        PlayerObj.GetComponent<PlayerController>().enabled = false;
        PlayerObj.GetComponent<PlayerEntity>().currenthealth = 0;

    }
    public void Onrestart()
    {
        SceneManager.LoadScene("Ninja 2");
    }
    public void OntoMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void JumpBackLastLevel()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void QuestPanel()
    {
        questPanle.SetActive(true);
    }
    public void BacktoGame()
    {
        questPanle.SetActive(false);
        Menu.SetActive(false);
    }
}
