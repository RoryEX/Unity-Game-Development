using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceGround : MonoBehaviour
{
    int hits = 0;
    float timer = 0;
    public Transform telPoint = null;
    // Start is called before the first frame update
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer<=0)
        {
            hits = 0;
            this.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.transform.tag=="Player")
        {
            this.GetComponent<SpriteRenderer>().color = Color.red;
            hits++;
            timer = 20;
            if (hits >= 2)
            {
                other.transform.position = telPoint.position;
                hits = 0;
                this.GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
    }
}
