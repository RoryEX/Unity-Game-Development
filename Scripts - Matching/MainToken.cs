using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainToken : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    GameObject gameControl;

    public Sprite[] faces;
    public Sprite back;
    public int faceIndex;
    public bool matched = false;
    public bool inTokens = false;
    
    public void OnMouseDown()
    {
        if (matched == false)
        {
            
            if (spriteRenderer.sprite == back)
            {
                
                if (gameControl.GetComponent<GameControl>().TwoCardsUp() == false)
                {
                    spriteRenderer.sprite = faces[faceIndex];
                    gameControl.GetComponent<GameControl>().AddVisibleFace(faceIndex);
                    matched = gameControl.GetComponent<GameControl>().CheckMatch(faceIndex);
                }
            }
            else
            {
                    gameControl.GetComponent<GameControl>().RemoveVisibleFace(faceIndex);
                    spriteRenderer.sprite = back;
            }
           
        }
        
    }

        void Awake()
    {
        gameControl = GameObject.Find("GameControl");
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
}
