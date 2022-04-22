using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuContorller : MonoBehaviour
{
    public void TutorLv()
    {
        SceneManager.LoadScene("TutorialLevel");
    }

    public void instructions()
    {
        SceneManager.LoadScene("Instruction");
    }
    public void LoadLv1()
    {
        SceneManager.LoadScene("Level1");
    }
    public void exitGame()
    {
        Application.Quit();
    }
    public void BackMain()
    {
        SceneManager.LoadScene("Menu");
    }
}