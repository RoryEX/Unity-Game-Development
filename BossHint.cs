using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHint : MonoBehaviour
{
    public GameObject Panel;
    public GameObject Boss;
    public bool isdead;
    // Start is called before the first frame update
    void Start()
    {
        isdead = false;
    }

    // Update is called once per frame
    void Update()
    {
        isdead = Boss.GetComponent<NeueZielAI>().isDead;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Panel.SetActive(true);
        }
    }
    
}
