using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
// SUPER SIMPLE game manager
public class GameManager : MonoBehaviour
{
    public static GameManager Gman;
    [SerializeField]
    private int lives = 3; // lives

    [SerializeField]
    private int coinCount = 0; // Coins
    

    [SerializeField]
    private int coinsForExtraLife = 10; // How many coins for extra life

    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI coinsText;

    void Start()
    {
        if (Gman == null)
        {
            Gman = this;
        }
        else
        {
            return;
        }

        livesText.text = "Lives: " + lives.ToString();
    }
    // Setters
    
    public int Lives
    {
        get
        {
            return lives;
        }
        set
        {
            lives = value;
            livesText.text = "Lives: " + lives.ToString();
            if (lives <= 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
    public int CoinsCount
    {
        get
        {
            return coinCount;
        }
        set
        {
            coinCount = value;
            coinsText.text = "Coins: " + coinCount.ToString();
            if (coinCount % 10 == 0)
            {
                Lives++;
            }
        }
    }
    // Updates lives
    public void UpdateLives(int amount)
    {
        // Add amount to lives
        lives += amount;
        // If lives is less than or equal to zero
        if (lives <= 0)
        {
            // Restart scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    // Update coins
    public void UpdateCoinCount(int amount)
    {
        // Add amoun to coins
        coinCount += amount;
        coinsText.text = "Coins: " + coinCount.ToString();
        // If we have enough for an extra life
        if (coinCount >= coinsForExtraLife)
        {
            // Subtract the cost of an extra life
            coinCount -= coinsForExtraLife;
            // Add a life
            lives++;
        }
    }
    // Getters 
    // Get Lives
    public int GetLives()
    {
        return lives;
    }
    // Get CoinCount
    public int GetCoinCount()
    {
        return coinCount;
    }

}
