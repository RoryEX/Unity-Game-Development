using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerMovement : UnityEngine.MonoBehaviour
{
    // Variables
    // Movement speed, can be adjusted in the inspector via value slider
    [Header("Player movement speed")]
    [SerializeField]
    [Range(0, 10)]
    private float movementSpeed = 5.0f;

    // Keycodes for moving the player. You can assign these in the inspector
    // You can also just use an axis for movement like "Vertical" or "Horizontal"
    // It's good to know you can assign keys in the inspector if you want to experiement with bindings
    [Header("Keys for player movement")]
    [SerializeField]
    private KeyCode moveNorth;
    [SerializeField]
    private KeyCode moveSouth;
    [SerializeField]
    private KeyCode moveEast;
    [SerializeField]
    private KeyCode moveWest;
    [SerializeField] 
    private float jumpHeight = 5.0f;
    [SerializeField] 
    private float groundCheckDistance;
    private Vector2 pos;
    // Direction vectors
    // You can always use Vector3.up/right/forward or transform.up/right/forward
    // This is another good to know, there no reason you can't create your own direction vectors
    // An example would be moving the player in diagonal directions North West for instance, would be (-1, 1, 0)
    private Vector2 north = new Vector2(0, 1);
    private Vector2 south = new Vector2(0, -1);
    private Vector2 east = new Vector2(1, 0);
    private Vector2 west = new Vector2(-1, 0);

    // Caching the spriteRenderer component so the sprite can be flipped based on movement direction
    private SpriteRenderer spriteRenderer;
    private Collider2D col = null;
    private Rigidbody2D rig = null;
    [SerializeField] private bool jumped = false;
    [SerializeField] private int jumpCount;
    [SerializeField] private int defaultJump = 2;
    [SerializeField] private Transform CubeTransform = null;
    [SerializeField] private GameObject Cube = null;
    [SerializeField] private GameObject Bullet = null;
    private int CubeCount=3;
    private GameObject ShieldRight;
    private GameObject ShieldLeft;
    private bool upShieldLock;
    private GameObject upShield;
    private bool isLeft;
    private bool allowShoot;
    private int coins=0;
    private bool Finished=false;
    
    


    // Start is called before the first frame update
    void Start()
    {
        ShieldRight = GameObject.Find("Player/ShieldRight").gameObject;
        ShieldRight.SetActive(false);
        ShieldLeft = GameObject.Find("Player/ShieldLeft").gameObject;
        ShieldLeft.SetActive(false);
        upShield = GameObject.Find("Player/UpShield").gameObject;
        upShield.SetActive(false);
        upShieldLock = false;
        allowShoot = false;
        

        if (GetComponent<Rigidbody2D>() && GetComponent<Collider2D>())
        {
            rig = GetComponent<Rigidbody2D>();

            col = GetComponent<Collider2D>();
            
            rig.freezeRotation = true;

        }
        else
        {
            Debug.LogWarning("There is no attached Rigidbody to" + this.gameObject.name);

            this.enabled = false;
        }
        // Get the sprite renderer component, if none exists log a warning
        if (GetComponent<SpriteRenderer>())
            spriteRenderer = GetComponent<SpriteRenderer>();
        else
            Debug.LogWarning("The player does not have a Sprite Renderer component! Please fix....");
    }

    // Update is called once per frame
    void Update()
    {
        pos = transform.position;
        
        // Call the move player function
        if (Input.GetKey(KeyCode.S)&&!isLeft)
        {
            ShieldRight.SetActive(true);
            this.gameObject.tag = "Shield";
        }
        else if(Input.GetKey(KeyCode.S) && isLeft)
        {
            ShieldLeft.SetActive(true);
            this.gameObject.tag = "Shield";
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            ShieldRight.SetActive(false);
            ShieldLeft.SetActive(false);
            this.gameObject.tag = "Player";
        }

        if (Input.GetKey(KeyCode.Z)&&upShieldLock)
        {
            upShield.SetActive(true);
            this.gameObject.tag = "Shield";
        }
        else if (Input.GetKeyUp(KeyCode.Z)) 
        {
            upShield.SetActive(false);
            this.gameObject.tag = "Player";
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (Input.GetKeyDown(KeyCode.X)&&CubeCount>0)
        {
            cubeSpawn();
            CubeCount--;
        }
        if (Input.GetKeyDown(KeyCode.C) && !isLeft && allowShoot)
        {
            Shoot(ShieldRight.transform);
            
        }
        else if(Input.GetKeyDown(KeyCode.C) && isLeft && allowShoot)
        {
            Shoot(ShieldLeft.transform);
        }
        GetInput();
    }
    private void FixedUpdate()
    {
        if (IsGrounded())
        {
            jumpCount = defaultJump;
        }
        if (jumped)
        {
            jumped = false;
            Jump();
        }
        MovePlayer();
    }
    private void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount > 0)
        {
            jumped = true;
            jumpCount--;
        }
        else if (jumpCount <= 0)
        {
            jumped = false;
        }
    }
    protected void Jump()
    {
        rig.AddForce(transform.up * jumpHeight, ForceMode2D.Impulse);
    }
    protected bool IsGrounded()
    {
        float distanceToGround = col.bounds.extents.y + groundCheckDistance;
        float Xoffset = 0.2f;
        pos.y -= (col.bounds.extents.y + groundCheckDistance);
        pos.x += Xoffset;
        Vector2 leftPos = pos;
        leftPos.x = -pos.x;
        Vector2 rightPos = pos;
        bool isGrounded=false;
        RaycastHit2D hitLeft = Physics2D.Raycast(leftPos, Vector2.down, distanceToGround);
        RaycastHit2D hitRight = Physics2D.Raycast(rightPos, Vector2.down, distanceToGround);
        //Debug.DrawRay(pos, Vector2.down*distanceToGround,Color.red);
        if (hitLeft.collider != null|| hitRight.collider!=null)
        {
            //Debug.Log(hitLeft.collider.gameObject);
            //Debug.Log(hitRight.collider.gameObject);
            /*if (hitRight.collider.gameObject.tag == "Ground") 
            { 
                isGrounded = true;
                Debug.Log("Hit ground");
            }
            else if(hitLeft.collider.gameObject.tag == "Ground")
            {
                isGrounded = true;
                Debug.Log("Hit ground");
            }
            else
            {
                isGrounded = false;
            }
            Debug.Log("Hit something");     */
            isGrounded = true;
            Debug.Log("Hit ground");
        }
        else
        {
            Debug.Log("Hit nothing");
        }
        Debug.Log(isGrounded);
        return isGrounded;
    }

    // Moves the player
    void MovePlayer()
    {
        // Move player towards the direction vector when a direction key is pressed
        if (Input.GetKey(moveNorth))
        {
            //transform.position += (north * movementSpeed) * Time.deltaTime;
        }
        if (Input.GetKey(moveSouth))
        {
            //transform.position += (south * movementSpeed) * Time.deltaTime;
        }
        if (Input.GetKey(moveEast))
        {
            spriteRenderer.flipX = false;
            rig.velocity += (east * movementSpeed) * Time.deltaTime;
            isLeft = false;
        }
        if (Input.GetKey(moveWest))
        {
            spriteRenderer.flipX = true;
            rig.velocity += (west * movementSpeed) * Time.deltaTime;
            isLeft = true;
        }
    }
    void cubeSpawn()
    {
        GameObject CubeB = Instantiate(Cube, CubeTransform.position, CubeTransform.rotation);
    }
    void Shoot(Transform Muzzle)
    {
        GameObject bullet = Instantiate(Bullet, Muzzle.position, Muzzle.rotation);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Enemy")
        {
            Destroy(this.gameObject);
            SceneManager.LoadScene("SampleScene");
        }
        if (other.transform.name == "Excarlibur")
        {
            allowShoot = true;
            Destroy(other.gameObject);
        }
        if (other.transform.name == "Shield")
        {
            upShieldLock = true;
            Destroy(other.gameObject);
        }
        if (other.transform.name== "CubeSupply")
        {
            CubeCount = 3;
        }
        if (other.transform.name == "Lava")
        {
            SceneManager.LoadScene("SampleScene");
        }
        if (other.transform.tag == "Coin")
        {
            coins++;
            Destroy(other.gameObject);
        }
        if (other.transform.name == "GameConsole" && coins>=10)
        {
            Finished = true;
        }
        
    }
    private void OnGUI()
    {
        GUIStyle labelFont = new GUIStyle();
        labelFont.fontSize = 80;
        labelFont.normal.textColor = Color.red;
        if (true)
        {
            GUI.Label(new Rect(50.0f, 30.0f, 200.0f, 200.0f), coins + "Coins", labelFont);
            GUI.Label(new Rect(50.0f, 100.0f, 200.0f, 200.0f), CubeCount + "Cubes can using", labelFont);
        }
        if (Finished)
        {
            GUI.Label(new Rect(50.0f, 160.0f, 200.0f, 200.0f), "Level Finished!", labelFont);
        }
        else if (!Finished)
        {
            GUI.Label(new Rect(50.0f, 160.0f, 200.0f, 200.0f), "Collect all coins!", labelFont);
        }
    }
}

