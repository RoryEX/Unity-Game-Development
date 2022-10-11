using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalPlatform : MonoBehaviour
{
    private PlatformEffector2D effoctor;
    public float waitTime;
    private void Start()
    {
        effoctor = GetComponent<PlatformEffector2D>();
    }
    private void Update()
    {
        if (waitTime <= 0)
        {
            effoctor.rotationalOffset = 0;
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            waitTime = 0;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
           
             effoctor.rotationalOffset = 180f;
             waitTime = 0.5f;
             waitTime -= Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            effoctor.rotationalOffset = 0;
            waitTime = 0;
        }
    }
}
