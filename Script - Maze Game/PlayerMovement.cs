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
    
    bool isMgKeyget = false;
    bool isICEkey = false;
    bool isFireKey = false;
    bool isExGet = false;

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

    // Direction vectors
    // You can always use Vector3.up/right/forward or transform.up/right/forward
    // This is another good to know, there no reason you can't create your own direction vectors
    // An example would be moving the player in diagonal directions North West for instance, would be (-1, 1, 0)
    private Vector3 north = new Vector3(0, 1, 0);
    private Vector3 south = new Vector3(0, -1, 0);
    private Vector3 east = new Vector3(1, 0, 0);
    private Vector3 west = new Vector3(-1, 0, 0);

    // Caching the spriteRenderer component so the sprite can be flipped based on movement direction
    private SpriteRenderer spriteRenderer;
    

    // Start is called before the first frame update
    void Start()
    {
       
        // Get the sprite renderer component, if none exists log a warning
        if (GetComponent<SpriteRenderer>())
            spriteRenderer = GetComponent<SpriteRenderer>();
        else
            Debug.LogWarning("The player does not have a Sprite Renderer component! Please fix....");

        
    }

    // Update is called once per frame
    void Update()
    {
        // Call the move player function
        MovePlayer();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        
    }


    // Moves the player
    void MovePlayer()
    {
        // Move player towards the direction vector when a direction key is pressed
        if(Input.GetKey(moveNorth))
        {
            transform.position += (north * movementSpeed) * Time.deltaTime; 
        }
        if (Input.GetKey(moveSouth))
        {
            transform.position += (south * movementSpeed) * Time.deltaTime;
        }
        if (Input.GetKey(moveEast))
        {
            spriteRenderer.flipX = false;
            transform.position += (east * movementSpeed) * Time.deltaTime;
        }
        if (Input.GetKey(moveWest))
        {
            spriteRenderer.flipX = true;
            transform.position += (west * movementSpeed) * Time.deltaTime;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.transform.name== "MagicKey")
        {
            isMgKeyget = true;
            Destroy(other.gameObject);
            Debug.Log("Key get! GO open the gate!");
        }
        if(other.transform.name == "FakeTeleP"|| other.transform.name == "FakeTeleP (4)"||other.transform.name == "FakeTeleP (3)" || other.transform.name == "FakeTeleP (2)")
        {
            Debug.Log("It's a fake teleport point, haha!");
        }
        if(other.transform.name== "Key1")
        {
            isICEkey = true;
            Destroy(other.gameObject);
            Debug.Log("Key get! GO open the gate!");
        }
        if (other.transform.name == "Key2")
        {
            isFireKey = true;
            Destroy(other.gameObject);
            Debug.Log("Key get! GO open the gate!");
        }
        if(other.transform.name== "Excurlibur")
        {
            Destroy(other.gameObject);
            Debug.Log("Level Finish! Congratuation!");
            isExGet = true;
            
        }
    }
    void OnGUI()
    {
        GUIStyle labelFont = new GUIStyle();
        labelFont.fontSize = 80;
        labelFont.normal.textColor = Color.red;
        if (isExGet)
        {
            GUI.Label(new Rect(50.0f, 30.0f, 200.0f, 200.0f), "Congratuation! Maze Finished! ", labelFont);
            GUI.Label(new Rect(50.0f, 100.0f, 200.0f, 200.0f), "You find the Excurlibur! ", labelFont);
        }
        
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.name == "Gate")
        {
            if(isMgKeyget)
            {
                Destroy(other.gameObject);
                Debug.Log("Gate open!");
            }
            else
            {
                Debug.Log("Find the key first!");
            }
        }
        if(other.transform.name == "IceGate")
        {
            if(isICEkey)
            {
                Destroy(other.gameObject);
                Debug.Log("Gate open!");
            }
            else
            {
                Debug.Log("Find the key first!");
            }
        }
        if (other.transform.name == "FireGate")
        {
            if (isFireKey)
            {
                Destroy(other.gameObject);
                Debug.Log("Gate open!");
            }
            else
            {
                Debug.Log("Find the key first!");
            }
        }
    }
}
