using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField]
    private float BulletSpeed = 5.0f;
    private Rigidbody2D rig = null;
    public GameObject player;
    //EnemyEntity Enemy;
    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        damage = 5;
        player = GameObject.Find("Ninja");
        bool isfacingRight = player.GetComponent<PlayerController>().facingRight;
        if (GetComponent<Rigidbody2D>())
        {
            rig = GetComponent<Rigidbody2D>();

            if (isfacingRight)
            {
                rig.velocity = transform.right * BulletSpeed;
            }
            else if(!isfacingRight)
            {
                rig.velocity = -transform.right * BulletSpeed;
            }
            
            
        }
        Destroy(this.gameObject, 2.0f);
    }
    

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Enemy")
        {
            other.GetComponent<EnemyEntity>().TakeDamage(damage); 
            Destroy(this.gameObject);
        }
    }
}
