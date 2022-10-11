using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootAI : MonoBehaviour
{
    public float speed;
    public Transform target;
    public float minDistance;
    [SerializeField]
    public float shootRange;
    [SerializeField]
    public float partolDistance;
    public float distanceToPlayer;

    public GameObject projectile;
    private float nextShotTime;
    [SerializeField]
    public float timeBetweenShots;
    private void Update()
    {
        distanceToPlayer = Vector2.Distance(transform.position, target.position);
        if(distanceToPlayer< shootRange)
        {
            if (Time.time > nextShotTime)
            {
                Instantiate(projectile, transform.position, Quaternion.identity);
                nextShotTime = Time.time + timeBetweenShots;
            }
        }
        if (distanceToPlayer < minDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, -speed * 2 * Time.deltaTime);
        }


    }

}
