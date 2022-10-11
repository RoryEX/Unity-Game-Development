using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolFloat : MonoBehaviour
{
    
    public float speed;
    private bool movingRight = true;
    //public Transform groundDetection;
    public float patorlTimer;

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        patorlTimer += Time.deltaTime;
        if (patorlTimer>=2&& movingRight)
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
            movingRight = false;
            patorlTimer = 0;
        }
        else if(patorlTimer >= 2 && !movingRight)
        {
             transform.eulerAngles = new Vector3(0, 0, 0);
             movingRight = true;
             patorlTimer = 0;
        }

        
    }
}
