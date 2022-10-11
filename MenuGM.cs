using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuGM : MonoBehaviour
{
    public AudioSource BGMMenu;
    public GameObject TPanle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void SecendLevel()
    {
        SceneManager.LoadScene("Ninja 2");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void TutorialPanel()
    {
        TPanle.SetActive(true);
    }
    public void ClosePanel()
    {
        TPanle.SetActive(false);
    }
}
