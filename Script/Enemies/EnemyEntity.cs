using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEntity : MonoBehaviour
{
    public int health;
    public AudioSource explosion;
    public float timer=4;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        { 
            explosion.Play();
            this.gameObject.SetActive(false);
            
            
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}
