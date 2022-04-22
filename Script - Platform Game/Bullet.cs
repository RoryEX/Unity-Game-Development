using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float BulletSpeed = 5.0f;
    private Rigidbody2D rig = null;
    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<Rigidbody2D>())
        {
            rig = GetComponent<Rigidbody2D>();

            rig.velocity = transform.right * BulletSpeed;
        }
        Destroy(this.gameObject, 2.0f);
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Enemy")
        {
            Destroy(other.gameObject);
        }
        if(other.transform.tag == "Block")
        {
            Destroy(other.gameObject);
        }
        if (other.transform.tag == "Shield")
        {
            Destroy(this.gameObject);
        }

    }
}
