using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float BulletSpeed = 5.0f;
    private Rigidbody rig = null;
    private int damge = 10;
    // Start is called before the first frame update

    void Start()
    {
        if (rig == null)
        {
            this.gameObject.AddComponent<Rigidbody>();
        }
        Destroy(this.gameObject, 1);
    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "Enemy")
        {
            Destroy(other.gameObject);
            GameManager.Gman.CoinsCount++;
        }
        Destroy(this.gameObject);
    }
   
}
