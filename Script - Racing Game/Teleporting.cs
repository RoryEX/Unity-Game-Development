using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporting : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform telPoint = null;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Player")
        {
            other.transform.position = telPoint.position;
        }
    }
}

