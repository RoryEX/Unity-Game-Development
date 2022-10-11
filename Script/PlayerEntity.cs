using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEntity : MonoBehaviour
{
    // Start is called before the first frame update
    public float maxHp;
    public float currenthealth;
    public Animator anim;
    public Slider playerHP;
    public GameObject panel;
    public AudioSource wounded;
    void Start()
    {
        currenthealth = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        playerHP.value = currenthealth;
        if (currenthealth <= 0)
        {
            //Destroy(this.gameObject);
            anim.SetBool("Isdead", true);
            this.GetComponent<PlayerController>().enabled = false;
            panel.SetActive(true);
        }

    }
    public void TakeDamage(int damage)
    {
        currenthealth -= damage;
        wounded.Play();
    }
}
