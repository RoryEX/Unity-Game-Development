using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public float Speed;
    private float timer = 0;
    private float flyFlipTimer = 1.0f;
    private GameObject player = null;
    private float distanceToPlayer = 0;
    [SerializeField]
    public float diveRange = 3.0f;
    protected Rigidbody rig = null;
    protected Transform playerTransform;
    protected Vector3 moveDirection = Vector3.zero;
    protected Vector3 startPosition;
    public enum EnemyState { patrol, Diving, backFloating }
    public EnemyState currentState;
    private Collider col = null;
    [SerializeField] private float groundCheckDistance = .25f;

    void Start()
    {
        startPosition = this.transform.position;
        col = GetComponent<Collider>();
        player = GameObject.FindGameObjectWithTag("Player");
        
        rig = GetComponent<Rigidbody>();
        rig.freezeRotation = true;
        StartCoroutine(updateDistanceToPlayer());
        //transform.forward = startPosition;
        //moveDirection = transform.forward;
    }
    Coroutine EnemyStates;
    // Update is called once per frame
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
            case EnemyState.Diving:
                if (EnemyStates != null)
                {
                    StopCoroutine(EnemyStates);
                }
                EnemyStates = StartCoroutine(Diving());
                break;

            case EnemyState.backFloating:

                EnemyStates = StartCoroutine(backFloating());

                break;
        }
    }
    IEnumerator updateDistanceToPlayer()
    {
        while (true)
        {
            distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            yield return new WaitForSecondsRealtime(0.25f);
        }
    }
    IEnumerator EnemyPatrol()
    {

        moveDirection.x = rig.velocity.x;
        moveDirection.z = rig.velocity.z;
        rig.velocity = moveDirection * 0.1f * Speed;
        timer += Time.deltaTime;
        if (timer > flyFlipTimer)
        {
            timer = 0;
            moveDirection *= -1;
            //transform.forward = moveDirection;
        }
        if (distanceToPlayer < diveRange)
        {
            currentState = EnemyState.Diving;
        }
        yield return new WaitForSecondsRealtime(1);

    }
    IEnumerator Diving()
    {
        //playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        transform.LookAt(player.transform);
        //Vector3 playerPosition = playerTransform.position;
        //Vector3 playernewposition = player.transform.position;
        //Vector3 faceDirection = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z) - transform.position;
        //transform.forward = faceDirection;
        moveDirection = -transform.up * Speed * 3;
        rig.velocity = moveDirection;
        //transform.position = playernewposition;
        if (IsGrounded())
        { 
            timer = 0;
            currentState = EnemyState.backFloating;
            
        }
        return new WaitForSecondsRealtime(2);

    }
    IEnumerator backFloating()
    {
        
        timer += Time.deltaTime;
        if (timer > flyFlipTimer)
        {
            transform.position = startPosition;
            currentState = EnemyState.patrol;
        }
        yield return new WaitForSecondsRealtime(2);
    }
    protected bool IsGrounded()
    {
        float distanceToGround = col.bounds.extents.y + groundCheckDistance;

        bool isGrounded = Physics.Raycast(transform.position, -transform.up, distanceToGround);

        return isGrounded;
    }
}
