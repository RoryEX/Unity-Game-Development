using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LV1GM : MonoBehaviour
{
    public AudioSource BGMLV1;
    public GameObject Menu;
    public GameObject questPanle;
    // Start is called before the first frame update
    void Start()
    {
        BGMLV1.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Menu.SetActive(true);
        }
    }

    public void Onrestart()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void OntoMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void JumpToNextLevel()
    {
        SceneManager.LoadScene("Ninja 2");
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
