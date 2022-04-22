using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{
    [SerializeField] GameObject token;
    [SerializeField] Transform spawnPosition;
    List<int> faceIndexes = new List<int> {0, 1, 2, 3, 0, 1, 2, 3};

    public static System.Random rnd = new System.Random();
    public int shuffleNum = 0;
    int[] visibleFaces = {-1, -2};
    public List<GameObject> Tokens;
    private float timer = 5;
    private bool allMatched = false;
    void Start()
    {
        int originalCnt = faceIndexes.Count;
        Vector3 spPosition = spawnPosition.position;
       

        for (int i = 0; i <= 7; i++)
        {
            shuffleNum = rnd.Next(0, (faceIndexes.Count));
            var temp = Instantiate(token, spPosition, Quaternion.identity);
            temp.GetComponent<MainToken>().faceIndex = faceIndexes[shuffleNum];
            faceIndexes.Remove(faceIndexes[shuffleNum]);
            spPosition.x +=4;
            if(i == (originalCnt/2-1 ))
            {
                spPosition.y += -4f;
                spPosition.x = spawnPosition.position.x;
            }
            Tokens.Add(temp);
        }
        
    }
    public void checkInTokens()
    {
        for (int i = 0; i < Tokens.Count; i++)
        {
            for (int j = i+1; j < Tokens.Count; j++)
            {
                MainToken mainTokenI = Tokens[i].GetComponent<MainToken>();
                MainToken mainTokenJ = Tokens[j].GetComponent<MainToken>();
                if(mainTokenI.faceIndex== mainTokenJ.faceIndex)
                {
                    if(mainTokenI.matched || mainTokenJ.matched)
                    {
                        mainTokenI.matched = true;
                        mainTokenJ.matched = true;
                    }
                }
            }
        }

    }
    public void CheckAllClear()
    {
        for(int i = 0; i < Tokens.Count; i++)
        {
            MainToken mainTokenI = Tokens[i].GetComponent<MainToken>();
            if (mainTokenI.matched == false)
            {
                allMatched = false;
            }
            else
            {
                allMatched = true;
            }
        }
    }
    public bool TwoCardsUp()
    {
        bool cardsUp = false;
        if (visibleFaces[0] >= 0 && visibleFaces[1] >= 0)
        {
            cardsUp = true;
        }
        return cardsUp;
    }

    public void AddVisibleFace(int index)
    {
        if(visibleFaces[0] == -1)
        {
            
            visibleFaces[0] = index;
        }
        else if (visibleFaces[1] == -2)
        {
            visibleFaces[1] = index;
        }
        
    }

    public void RemoveVisibleFace(int index)
    {
        if (visibleFaces[0] == index)
        {
            visibleFaces[0] = -1;
        }
        else if(visibleFaces[1] == index)
        {
            visibleFaces[1] = -2;
        }
    }

    public bool CheckMatch(int index)
    {
        bool success = false;
        if (visibleFaces[0] == visibleFaces[1])
        {
            visibleFaces[0] = -1;
            visibleFaces[1] = -2;
            success = true;
            timer += 3;
        }
        return success;
    }
    private void OnGUI()
    {
        GUIStyle labelFont = new GUIStyle();
        labelFont.fontSize = 30;
        labelFont.normal.textColor = Color.red;
        GUI.Label(new Rect(50.0f, 30.0f, 200.0f, 200.0f), "Timer: " + timer, labelFont);
        if (timer < 0)
        {
            GUI.Label(new Rect(500.0f, 30.0f, 200.0f, 200.0f), "You lose! Restart in 2s. " , labelFont);
        }
        if (allMatched)
        {
            GUI.Label(new Rect(500.0f, 30.0f, 200.0f, 200.0f), "Congratuation! ", labelFont);
            
        }
    }
    private void Update()
    {
        checkInTokens();
        CheckAllClear();
        
        timer -= Time.deltaTime;
        
        if (allMatched)
        {
            timer = 0;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (timer <= -2)
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
}
