using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseBossAI : MonoBehaviour
{
    // Start is called before the first frame update
    public int health;
    public int damage;
    private float timeBtwDamage = 1.5f;
    public Animator bossAnim;
    public Animator camAnim;
    public bool isDead;

    public Slider hpBar;
    void Start()
    {
        bossAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeBtwDamage > 0)
        {
            timeBtwDamage -= Time.deltaTime;
        }
        hpBar.value = health;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player") && isDead == false)
        {
            if (timeBtwDamage <= 0)
            {
                camAnim.SetTrigger("shake");
                other.collider.GetComponent<PlayerEntity>().TakeDamage(damage);
            }
        }
    }

    private void TakeDamage(int damage)
    {
        health -= damage;
    }
}
