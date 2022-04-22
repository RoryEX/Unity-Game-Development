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
    [Range(-15, 15)]
    private float movementSpeed = 5.0f;

    [Header("Player rotate speed")]
    [SerializeField]
    [Range(-90, 90)]
    private float roatespeed = 1.0f;
    // Keycodes for moving the player. You can assign these in the inspector
    // You can also just use an axis for movement like "Vertical" or "Horizontal"
    // It's good to know you can assign keys in the inspector if you want to experiement with bindings
    [Header("Keys for player movement")]
    [SerializeField]
    private KeyCode SpeedUp;
    [SerializeField]
    private KeyCode SpeedDown;
    [SerializeField]
    private KeyCode TurnLeft;
    [SerializeField]
    private KeyCode TurnRight;
    [SerializeField]
    private KeyCode Reset;
    // Direction vectors
    // You can always use Vector3.up/right/forward or transform.up/right/forward
    // This is another good to know, there no reason you can't create your own direction vectors
    // An example would be moving the player in diagonal directions North West for instance, would be (-1, 1, 0)
    private Vector3 north = new Vector3(0, 1, 0);
    private Vector3 south = new Vector3(0, -1, 0);
    private Vector3 east = new Vector3(1, 0, 0);
    private Vector3 west = new Vector3(-1, 0, 0);
    private Rigidbody2D rig;
    //private float rotInput;
    private float accel = 0.05f;
    private float decel = 0.05f;
    private float angle = 0.5f;
    // Caching the spriteRenderer component so the sprite can be flipped based on movement direction
    private SpriteRenderer spriteRenderer;
    private float timer=65;
    private float offsetY = 0;
    private bool isFinished = false;
    private bool accessCheck = false;
    private bool accessCheck2 = false;
    private bool accessCheck3 = false;
    private int rounds = 2;
    private int checkPoints = 3;
    private float checkTime;
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        
        // Get the sprite renderer component, if none exists log a warning
        if (GetComponent<SpriteRenderer>())
            spriteRenderer = GetComponent<SpriteRenderer>();
        else
            Debug.LogWarning("The player does not have a Sprite Renderer component! Please fix....");


    }
    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        Mathf.Clamp(movementSpeed, -15, 15);
        MovePlayer();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        Rotate(roatespeed);
        resetPosition();

    }
    // Moves the player
    void MovePlayer()
    {
        // Move player towards the direction vector when a direction key is pressed
        if (Input.GetKey(SpeedUp))
        {
            movementSpeed += accel;
            
            rig.velocity = (-transform.right * movementSpeed );
        }
        if (Input.GetKey(SpeedDown))
        {
            movementSpeed -= decel;

            rig.velocity = (-transform.right * movementSpeed/4 );
        }
    }
    protected void Rotate(float rotSpeed)
    {
        if (Input.GetKey(TurnLeft))
        {
            rotSpeed++;
            Quaternion newRotation =  Quaternion.Euler(0, 0, angle*rotSpeed);
            transform.rotation = transform.rotation * newRotation;
            
        }
        if (Input.GetKey(TurnRight))
        {
            rotSpeed++;
            Quaternion newRotation = Quaternion.Euler(0, 0, angle * -rotSpeed);
            transform.rotation = transform.rotation * newRotation;
        }


    }
    private void resetPosition()
    {
        if (Input.GetKeyDown(Reset))
        {
            Quaternion newRotation = Quaternion.Euler(0, 0, 0);
            transform.rotation = newRotation;
            rig.velocity = new Vector2(0,0);
            movementSpeed = 0;
            roatespeed = 0;
            rig.freezeRotation = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "SandPause")
        {
            Quaternion newRotation = Quaternion.Euler(0, 0, 0);
            transform.rotation = newRotation;
            rig.velocity = new Vector2(0, 0);
            //movementSpeed = 0;
            //roatespeed = 0;
            rig.freezeRotation = true;
        }
        if (other.transform.name == "CheckPointR1" && accessCheck==false)
        {
            checkPoints--;
            timer += 10;
            accessCheck = true;
        }
        if (other.transform.name == "CheckPointR2" && accessCheck2 == false)
        {
            checkPoints--;
            timer += 10;
            accessCheck2 = true;
        }
        if (other.transform.name == "CheckPointR3" && accessCheck3 == false)
        {
            checkPoints--;
            timer += 10;
            accessCheck3 = true;
        }
        if(other.transform.tag=="TimeBouns")
        {
            timer += 30;
            Destroy(other.gameObject);
        }
        if (other.transform.tag == "TerminalPoint" && checkPoints==0)
        {
            rounds--;
            checkPoints = 3;
            accessCheck = false;
            accessCheck2 = false;
            accessCheck3 = false;
            
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag == "Block")
        {
            movementSpeed =0;
        }
        if (other.transform.tag == "Blocker")
        {
            movementSpeed = 0;
            Destroy(other.gameObject);
        }
    }
    void OnGUI()
    {
        GUIStyle labelFont = new GUIStyle();
        labelFont.fontSize = 80;
        labelFont.normal.textColor = Color.red;
        if (true)
        {
            GUI.Label(new Rect(50.0f, 90.0f, 200.0f, 200.0f), "You still have " + rounds + "rounds", labelFont);
            GUI.Label(new Rect(50.0f, 30.0f, 200.0f, 200.0f), "Timer: "+timer, labelFont);
            
        }
        if (rounds<=0)
        {
            GUI.Label(new Rect(200.0f, 300.0f, 200.0f, 200.0f), "Win! ", labelFont);
        }
        if (timer <= 0)
        {
            GUI.Label(new Rect(200.0f, 300.0f, 200.0f, 200.0f), "Not Finished! You Lose!", labelFont);
        }

    }
    
}
