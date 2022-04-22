using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatScroller : MonoBehaviour
{
    public float beatTempo;
    public bool hasStarted;
    void Start()
    {
        beatTempo /= 60f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasStarted)
        {
        }
        else
        {
            transform.position -= new Vector3(0, beatTempo * Time.deltaTime, 0);
        }
    }
}
