using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class PlayerMovement : UnityEngine.MonoBehaviour
{
    // Variables
    // Movement speed, can be adjusted in the inspector via value slider
    public event Action onEncountered;

    public LayerMask enemyEnclayer;
    [Header("Player movement speed")]
    [SerializeField]
    [Range(0, 10)]
    private float movementSpeed = 5.0f;
    
    bool isMgKeyget = false;
    bool isICEkey = false;
    bool isFireKey = false;
    bool isExGet = false;
    bool ismoving = false;
    [SerializeField] inBattleUnit enemyUnit;

    [SerializeField]
    public List<BattleBaseUnit> enemylist;
   
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
    private float randomEncounter;
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
    public void HandleUpdate()
    {
        randomEncounter= UnityEngine.Random.Range(1, 2001) * 0.5f;
        // Call the move player function
        MovePlayer();
        //encouterEnemySetUp();
        if (ismoving)
        {
            checkForEncounter();
            randomEncounter += 10;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        
    }


    // Moves the player
    void MovePlayer()
    {
        // Move player towards the direction vector when a direction key is pressed
        ismoving = false;
        if (Input.GetKey(moveNorth))
        {
            transform.position += (north * movementSpeed) * Time.deltaTime;
            ismoving = true;
        }
        if (Input.GetKey(moveSouth))
        {
            transform.position += (south * movementSpeed) * Time.deltaTime;
            ismoving = true;
        }
        if (Input.GetKey(moveEast))
        {
            spriteRenderer.flipX = false;
            transform.position += (east * movementSpeed) * Time.deltaTime;
            ismoving = true;
        }
        if (Input.GetKey(moveWest))
        {
            spriteRenderer.flipX = true;
            transform.position += (west * movementSpeed) * Time.deltaTime;
            ismoving = true;
        }
        
    }
    public void checkForEncounter()
    {
        if (Physics2D.OverlapCircle(transform.position, 0.2f, enemyEnclayer) !=null)
        {
            if ((randomEncounter <= 10))
            {
                encouterEnemySetUp();
                onEncountered();
            }
        }
    }
    public void encouterEnemySetUp()
    {
        enemyUnit._base = enemylist[UnityEngine.Random.Range(0, enemylist.Count)];
        enemyUnit.level = UnityEngine.Random.Range(0, 101);
    }
    
}
