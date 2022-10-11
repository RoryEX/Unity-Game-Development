using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndMAAI : MonoBehaviour
{
    public int currentHealth;
    public int damage;
    private float timer;
    //public Animator bossAnim;
    public Animator camAnim;
    public bool isDead;

    public float speed;
    public Transform target;
    public float distanceToPlayer;
    public float minDistance;
    public float atkRange;
    public float shootRange;
    public Transform groundDetection;
    public float distanceCheckGround;

    public Slider hpBar;

    private bool movingRight = true;
    

    public int stage2HP;
    public GameObject projectile;
    public float patorlTimer;

    public enum BossState { patrol, Shoot, Idle }
    public BossState currentState;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = this.GetComponent<EnemyEntity>().health;
        stage2HP = currentHealth / 2;
    }
    Coroutine BossStates;
    // Update is called once per frame
    void Update()
    {
        currentHealth = this.GetComponent<EnemyEntity>().health;
        distanceToPlayer = Vector2.Distance(transform.position, target.position);
        hpBar.value = currentHealth;
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
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, distanceCheckGround);

        if (!groundInfo.collider)
        {
            if (movingRight)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }

        }
        if (distanceToPlayer < shootRange )
        {
            currentState = BossState.Shoot;
        }
        yield return new WaitForSeconds(3);
    }
    IEnumerator BossShoot()
    {
        Instantiate(projectile, transform.position, Quaternion.identity);
        if (distanceToPlayer > shootRange)
        {
            currentState = BossState.Idle;
        }
        else if(distanceToPlayer < minDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, -speed * 3 * Time.deltaTime);
            currentState = BossState.Idle;
        }
        yield return new WaitForSeconds(2);
    }
    IEnumerator BossIdle()
    {
        timer += Time.deltaTime;
        if (timer > 3 && distanceToPlayer > shootRange)
        {
            timer = 0;
            currentState = BossState.patrol;
        }
        else if (timer > 3 && distanceToPlayer < shootRange)
        {
            timer = 0;
            currentState = BossState.Shoot;
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
