using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerControl2 : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public float timerHeal;

    private float moveInputH;
    private Rigidbody2D rbPlayer;

    public bool facingRight = true;

    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask groundLayer;

    public GameObject Bullet;
    public Transform Muzzle;

    private bool allowJump;
    public int extraJump;
    [SerializeField]
    public int extraJumpValue;

    [SerializeField]
    private KeyCode Jump;
    [SerializeField]
    private KeyCode LightAttack;
    [SerializeField]
    private KeyCode HeavyAttack;
    [SerializeField]
    private KeyCode Shield;
    [SerializeField]
    private KeyCode ShuriKan;

    bool isTouchingFront;
    public Transform frontCheck;
    bool wallSliding;
    public float wallSlidingSpeed;

    bool wallJumping;
    public float xWallForce;
    public float yWallForce;
    public float wallJumpTime;

    public Animator anim;

    private float timeBtwAtk;
    public float startTimeAtk;

    public Transform attackPos;
    public float atkRange;
    public LayerMask Enemies;
    public int damage;
    public int hDamage;

    public int shurAmount;
    public Text Shuriken;
   
    
    public bool outOfBattle;

    public GameObject LV2GM;
    void Start()
    {
        rbPlayer = GetComponent<Rigidbody2D>();
        
        outOfBattle = false;
    }

    void FixedUpdate()
    {
       
        rbPlayer.freezeRotation = true;
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);

        isTouchingFront = Physics2D.OverlapCircle(frontCheck.position, checkRadius, groundLayer);

        moveInputH = Input.GetAxis("Horizontal");

        anim.SetFloat("Speed", Mathf.Abs(moveInputH));
        rbPlayer.velocity = new Vector2(moveInputH * speed, rbPlayer.velocity.y);
        if (facingRight == false && moveInputH > 0)
        {
            Flip();
        }
        else if (facingRight == true && moveInputH < 0)
        {
            Flip();
        }

        if (isTouchingFront == true && isGrounded == false && moveInputH != 0)
        {
            wallSliding = true;
        }
        else
        {
            wallSliding = false;
        }

        if (wallSliding)
        {
            WallSliding();
        }


    }

    void Update()
    {
        Shuriken.text = "x" + shurAmount;
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            anim.SetBool("LandingThrough", true);
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            anim.SetBool("LandingThrough", false);
        }
        if (timeBtwAtk <= 0)
        {

            if (Input.GetKeyDown(LightAttack))
            {
                anim.SetBool("IsLightAtk", true);
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, atkRange, Enemies);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<EnemyEntity>().TakeDamage(damage);
                }
            }
            else if (Input.GetKeyDown(HeavyAttack))
            {
                anim.SetBool("IsHeavyAtk", true);
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, atkRange, Enemies);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<EnemyEntity>().TakeDamage(hDamage);
                }
            }
            timeBtwAtk = startTimeAtk;
        }
        else
        {
            timeBtwAtk -= Time.deltaTime;

        }

        if (Input.GetKeyUp(LightAttack))
        {
            anim.SetBool("IsLightAtk", false);
        }
        if (Input.GetKeyUp(HeavyAttack))
        {
            anim.SetBool("IsHeavyAtk", false);
        }
        if (Input.GetKeyUp(ShuriKan))
        {
            anim.SetBool("IsShoot", false);
        }

        if (isGrounded)
        {
            extraJump = extraJumpValue;
            allowJump = true;
            anim.SetBool("IsGround", true);
        }
        if (Input.GetKeyUp(Jump) && !isGrounded)
        {
            anim.SetBool("IsJumping", false);
            anim.SetBool("IsGround", false);
        }
        if (Input.GetKeyDown(Jump) && extraJump > 0)
        {
            PlayerJump();
            extraJump--;
        }
        else if (Input.GetKeyDown(Jump) && extraJump == 0 && isGrounded)
        {
            PlayerJump();
        }
        if (Input.GetKeyDown(Jump) && wallSliding)
        {
            wallJumping = true;
            //anim.SetBool("IsJumping", true);
            Invoke("SetWallJumpingToFalse", wallJumpTime);
        }
        if (wallJumping)
        {
            WallJump();
            //anim.SetBool("IsJumping", true);
        }
        if (Input.GetKeyDown(ShuriKan) && shurAmount > 0)
        {
            Shoot(this.transform);
            anim.SetBool("IsShoot", true);
            shurAmount--;

        }
        if (outOfBattle)
        {
            //timerHeal += Time.deltaTime;
            if (this.GetComponent<PlayerEntity>().currenthealth < this.GetComponent<PlayerEntity>().maxHp)
            {
                this.GetComponent<PlayerEntity>().currenthealth += (Time.deltaTime);
                if (this.GetComponent<PlayerEntity>().currenthealth >= this.GetComponent<PlayerEntity>().maxHp)
                {
                    this.GetComponent<PlayerEntity>().currenthealth = this.GetComponent<PlayerEntity>().maxHp;
                    //timerHeal = 0;
                }
            }
        }
    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
        //or use function spriteRenderer.flipX = false; Remember assign spriteRender first
    }
    void PlayerJump()
    {
        rbPlayer.velocity = Vector2.up * jumpForce;
        anim.SetBool("IsJumping", true);
        //or use rbPlayer.AddForce(transform.up * jumpForce/Height, ForceMode2D.Impulse); 
    }
    void WallSliding()
    {
        rbPlayer.velocity = new Vector2(rbPlayer.velocity.x, Mathf.Clamp(rbPlayer.velocity.y, -wallSlidingSpeed, float.MaxValue));
    }
    void SetWallJumpingToFalse()
    {
        wallJumping = false;
    }
    void WallJump()
    {
        rbPlayer.velocity = new Vector2(xWallForce * -moveInputH, yWallForce);
    }
    void Shoot(Transform Muzzle)
    {
        GameObject bullet = Instantiate(Bullet, Muzzle.position, Muzzle.rotation);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, atkRange);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.name == "Exit")
        {     
        }
        else if (other.collider.tag == "Fangs")
        {
            this.GetComponent<PlayerEntity>().TakeDamage(5);
        }
        else if (other.collider.name == "DeathField" || other.collider.tag == "Laser")
        {
            //Destroy(this.gameObject);
            anim.SetBool("Isdead", true);
            this.GetComponent<PlayerEntity>().currenthealth = 0;
            this.GetComponent<PlayerController>().enabled = false;
        }
        else if (other.collider.name == "LV2EXIT")
        {
            SceneManager.LoadScene("Finished");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            this.GetComponent<PlayerEntity>().currenthealth = 0;
        }
        else if (other.tag == "Enemy")
        {
            outOfBattle = false;
            this.GetComponent<PlayerEntity>().TakeDamage(5);
        }
        else if (other.name == "HPBox")
        {
            if (this.GetComponent<PlayerEntity>().currenthealth + 10 >= this.GetComponent<PlayerEntity>().maxHp)
            {
                this.GetComponent<PlayerEntity>().currenthealth = this.GetComponent<PlayerEntity>().maxHp;
                Destroy(other.gameObject);
            }
            else if (this.GetComponent<PlayerEntity>().currenthealth + 10 < this.GetComponent<PlayerEntity>().maxHp)
            {
                this.GetComponent<PlayerEntity>().currenthealth += 10;
                Destroy(other.gameObject);
            }
        }
        else if (other.name == "KunaiBox")
        {
            shurAmount += 3;
            Destroy(other.gameObject);
        }
        else if (other.name == "Destoryer")
        {
            LV2GM.GetComponent<LV2GameMangent>().destoryerActive = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            outOfBattle = true;
        }
    }
}
