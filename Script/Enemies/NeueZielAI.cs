using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NeueZielAI : MonoBehaviour
{
    public int currentHealth;
    public int damage;
    private float timer;
    //public Animator bossAnim;
    //public Animator camAnim;
    public bool isDead;
    public float shootTimer;

    public float speed;
    public Transform target;
    public float distanceToPlayer;
    public float distanceToFollow;
    public float atkRange;
    public float shootRange;

    public Slider hpBar;

    private bool movingRight = true;
    public float distanceCheckGround;

    public int stage2HP;
    public GameObject projectile;
    public float patorlTimer;
    
    public enum BossState { patrol, chase,Atk,Shoot,Idle}
    public BossState currentState;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = this.GetComponent<EnemyEntity>().health;
        
        isDead = false;
    }
    Coroutine BossStates;
    // Update is called once per frame
    void Update()
    {
        currentHealth = this.GetComponent<EnemyEntity>().health;
        distanceToPlayer = Vector2.Distance(transform.position, target.position);
        hpBar.value = currentHealth;
        if (currentHealth <= 0)
        {
            isDead = true;
            
        }
        switch (currentState)
        {
            case BossState.patrol:

                if (BossStates != null)
                {
                    StopCoroutine(BossStates);
                }
                BossStates = StartCoroutine(BossPatrol());
                break;
            case BossState.Idle:
                if (BossStates != null)
                {
                    StopCoroutine(BossStates);
                }
                BossStates = StartCoroutine(BossIdle());
                break;
            case BossState.chase:
                if (BossStates != null)
                {
                    StopCoroutine(BossStates);
                }
                BossStates = StartCoroutine(BossChase());
                break;
            case BossState.Atk:
                {
                    if (BossStates != null)
                    {
                        StopCoroutine(BossStates);
                    }
                    BossStates = StartCoroutine(BossAtk());
                }
                break;
            case BossState.Shoot:
                {
                    if (BossStates != null)
                    {
                        StopCoroutine(BossStates);
                    }
                    BossStates = StartCoroutine(BossShoot());
                }
                break;
        }
    }
    IEnumerator BossPatrol()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        
        patorlTimer += Time.deltaTime;
        if (patorlTimer >= 2 && movingRight)
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
            movingRight = false;
            patorlTimer = 0;
        }
        else if (patorlTimer >= 2 && !movingRight)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            movingRight = true;
            patorlTimer = 0;
        }
        if (distanceToPlayer< distanceToFollow)
        {
            currentState = BossState.chase;
        }
        else if (distanceToPlayer < shootRange && currentHealth < stage2HP)
        {
            currentState = BossState.Shoot;
        }
        yield return new WaitForSeconds(3);
    }

    IEnumerator BossChase()
    {
       
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        if (distanceToPlayer < atkRange && currentHealth > stage2HP)
        {
            currentState = BossState.Atk;
        }
        else if (distanceToPlayer<shootRange && currentHealth < stage2HP)
        {
            currentState = BossState.Shoot;
        }
        else if (distanceToPlayer > distanceToFollow)
        {
            currentState=BossState.Idle;
        }
        yield return new WaitForSeconds(3);
    }
    IEnumerator BossAtk()
    {
        timer += Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * 3 * Time.deltaTime);
        if (timer > 2)
        {
            currentState = BossState.Idle;
            timer = 0;
        }
        
        
        yield return new WaitForSeconds(3);
    }
    IEnumerator BossShoot()
    {
        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0)
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
            Instantiate(projectile, transform.position, Quaternion.identity);
            shootTimer = 2;
        }
        
        if (distanceToPlayer > shootRange)
        {
            currentState = BossState.Idle;
        }
        yield return new WaitForSeconds(2);
    }
    IEnumerator BossIdle()
    {
        timer += Time.deltaTime;
        if(timer>3&&distanceToPlayer> distanceToFollow)
        {
            timer = 0;
            currentState = BossState.patrol;
        }
        else if (timer > 3 && distanceToPlayer < distanceToFollow)
        {
            timer = 0;
            currentState = BossState.chase;
        }
        else if (timer>2&&distanceToPlayer < atkRange)
        {
            timer = 0;
            currentState = BossState.Atk;
        }
        yield return new WaitForSeconds(1);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
         if (other.CompareTag("Player") && isDead == false)
         {
            //camAnim.SetTrigger("shake");
            other.GetComponent<PlayerEntity>().TakeDamage(damage);
         }
        
    }
}
