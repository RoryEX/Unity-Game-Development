using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Vector3 targtPosition;
    public float speed;
    public int damage;

    private void Start()
    {
        targtPosition = FindObjectOfType<PlayerController>().transform.position;
    }
    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, targtPosition, speed * Time.deltaTime);
        Destroy(this.gameObject, 3f);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag == "Player")
        {
            Debug.Log("Hit player");
            other.collider.GetComponent<PlayerEntity>().TakeDamage(damage);
            Destroy(this.gameObject);
        }
    }
}
