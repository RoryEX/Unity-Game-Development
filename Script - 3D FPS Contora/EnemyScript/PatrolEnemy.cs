using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolEnemy : MonoBehaviour
{
    [SerializeField]
    public float speedPatrol = 1.0F;
    [SerializeField]
    public float chaseSpeed = 3.0f;
    [SerializeField]
    public float patrolRange = 1.0f;
    [SerializeField]
    private float timer = 0;
    [SerializeField] 
    protected float chaseRange = 3.0f;
    private float distanceToPlayer = 0;
    [SerializeField] 
    protected float patrolSwitchTimer = 2.5f;
    private Vector3 startPosition, playerPosition;
    private GameObject player = null;
    protected Transform playerTransform;
    protected Rigidbody rig = null;
    public enum EnemyState { patrol, chase, idle }
    public EnemyState currentState;
    protected Vector3 moveDirection = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = gameObject.transform.position;
        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rig = GetComponent<Rigidbody>();
        rig.freezeRotation = true;
        StartCoroutine(updateDistanceToPlayer());
        transform.forward = Vector3.right;
        moveDirection = transform.forward;
    }

    // Update is called once per frame
    Coroutine EnemyStates;
    void Update()
    {
        switch (currentState)
        {
            case EnemyState.patrol:

                if (EnemyStates != null)
                {
                    StopCoroutine(EnemyStates);
                }
                EnemyStates = StartCoroutine(EnemyPatrol());
                break;
            case EnemyState.idle:
                if (EnemyStates != null)
                {
                    StopCoroutine(EnemyStates);
                }
                EnemyStates = StartCoroutine(EnemyIdel());
                break;
            case EnemyState.chase:
                if (EnemyStates != null)
                {
                    StopCoroutine(EnemyStates);
                }
                EnemyStates = StartCoroutine(EnemyChase());
                break;
        }
    }
    IEnumerator EnemyPatrol()
    {
        moveDirection.y = rig.velocity.y;
        rig.velocity = moveDirection;
        timer += Time.deltaTime;
        if (timer > patrolSwitchTimer)
        {
            timer = 0;
            moveDirection *= -1;
            transform.forward = moveDirection;
        }
        if (distanceToPlayer < chaseRange)
        {
            moveDirection = transform.forward * chaseSpeed;
            currentState = EnemyState.idle;
        }
        yield return new WaitForSecondsRealtime(3);

    }
    IEnumerator updateDistanceToPlayer()
    {
        while (true)
        {
            distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            yield return new WaitForSecondsRealtime(0.25f);
        }
    }
    IEnumerator EnemyChase()
    {
        transform.LookAt(player.transform);
        Vector3 faceDirection = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z) - transform.position;
        transform.forward = faceDirection;
        moveDirection = transform.forward * chaseSpeed;
        moveDirection.y = rig.velocity.y;
        rig.velocity = moveDirection;

        /*if (distanceToPlayer > chaseRange)
        {
            transform.forward = Vector3.right;
            moveDirection = transform.forward * speedPatrol;
            timer = 0;
            currentState = EnemyState.patrol;
        }*/
        
        yield return new WaitForSecondsRealtime(1);
    }
    IEnumerator EnemyIdel()
    {
        timer += Time.deltaTime;
        if (timer > 3 && distanceToPlayer < chaseRange)
        {
            timer = 0;
            currentState = EnemyState.chase;
        }
        else if (timer > 3 && distanceToPlayer > chaseRange)
        {
            timer = 0;
            currentState = EnemyState.patrol;

        }

        
        yield return new WaitForSecondsRealtime(1);
    }
    void OnCollisionEnter(Collision collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            currentState = EnemyState.idle;
            GameManager.Gman.Lives--;
        }
    }
}
