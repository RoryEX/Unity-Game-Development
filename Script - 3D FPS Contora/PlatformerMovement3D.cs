using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlatformerMovement3D : MonoBehaviour
{
    private float vAxis = 2.0f;
    private float hAxis = 2.0f;

    private float mouseX = 1.0f;

    private bool jumped = false;

    [SerializeField] private float moveSpeed = 0.0f;
    [SerializeField] private float rotateSpeed = 0.0f;


    [SerializeField] private float jumpHeight = 5.0f;

    [SerializeField] private float groundCheckDistance = .25f;

    private Rigidbody rig = null;

    private Collider col = null;



    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<Rigidbody>()&& GetComponent<Collider>()){
            rig = GetComponent<Rigidbody>();

            col = GetComponent<Collider>();

            rig.freezeRotation = true;

        }
        else
        {
            Debug.LogWarning("There is no attached Rigidbody to" + this.gameObject.name);

            this.enabled = false;
        }

        Cursor.lockState = CursorLockMode.Locked;
        
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();

        Rotate();
    }


    private void FixedUpdate()
    {
        if (jumped)
        {
            Jump();

            jumped = false;
        }
        Move();
    }

    protected void Jump()
    {
        rig.AddForce(transform.up * jumpHeight, ForceMode.Impulse);
    }

    protected bool IsGrounded()
    {
        float distanceToGround = col.bounds.extents.y + groundCheckDistance;

        bool isGrounded = Physics.Raycast(transform.position, -transform.up, distanceToGround);

        return isGrounded;
    }

    private void GetInput()
    {
        vAxis = Input.GetAxis("Vertical");
        hAxis = Input.GetAxis("Horizontal");
        mouseX = Input.GetAxis("Mouse X");

        if(Input.GetKeyDown(KeyCode.Space)&& IsGrounded())
        {
            jumped = true; 
        }
    }

    private void Move()
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection =((transform.forward * vAxis)+ (transform.right * hAxis)) * moveSpeed;

        moveDirection.y = rig.velocity.y;

        rig.velocity = moveDirection;
    }

    private void Rotate()
    {
        Vector3 newRotation = transform.localEulerAngles;

        newRotation.y += mouseX * rotateSpeed;

        transform.localEulerAngles = newRotation;
    }
}
