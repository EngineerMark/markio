using UnityEngine;
using System.Collections;

public class Actor : MonoBehaviour
{

    [Header("Character Controller")]

    public float walkSpeed = 12;
    public float jumpSpeed = 13;
    public Vector3 spawnPoint;
    public Vector2 velocity;
    public float difficulty;
    public bool isJumping;
    public bool isGrounded;
    public bool isMoving;
    public bool isCrouch;
    public bool isDead = false;

    public bool isVisible;

    public float healthPoints = 100;
    public float DPS = 3;
    [HideInInspector]
    public float dpsTick = 0f;


    public float start_healthPoints;
    [HideInInspector]
    public float start_dps;

    private float mulDuringJump = 0.5f;


    protected Rigidbody2D rb;
    protected SpriteRenderer usedSprite;
    protected Animator anim;
    protected BoxCollider2D coll;
    private float jumpForce;
    public Vector2 rbVelocity;

    public GameObject objectGamemanager;
    public GameManager gamemanager;

    public string type = "normal";

    //Damage flicker
    public bool isDamaged = false;
    private float dmgTick = 0f;
    private float maxDmgTick = 1.0f;

    //public GameObject camera;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        usedSprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
        objectGamemanager = GameObject.FindWithTag("GameManager");
        gamemanager = objectGamemanager.GetComponent<GameManager>();

        start_dps = DPS;
    }

    protected virtual void Start()
    {
        start_healthPoints = healthPoints;
        spawnPoint = transform.position;
        ActorManager.RegisterActor(this);
    }

    void OnCollisionEnter2D(Collision2D c)
    {
        isGrounded = true;
        if (c.gameObject.name == "ColliderAntiEscape")
        {
            jumpForce = 0.0f;
        }
    }

    void OnCollisionExit2D(Collision2D c)
    {
        if (c.gameObject.name == "OverworldCollider" || c.gameObject.name == "ColliderCoinRoom")
        {
            isGrounded = false;
        }
        if (c.gameObject.name == "SkyCollision")
        {
            jumpForce = -2.0f;
        }
    }

    public void Move(float x, bool jump, bool groundSmash = false)
    {
        mulDuringJump = 1f;
        if (isJumping != jump)
        {
            jumpForce = jumpSpeed;
            isGrounded = false;
        }
        if (isGrounded == false)
        {
            mulDuringJump = 0.8f;
        }
        if (x < 0)
        {
            usedSprite.flipX = true;
        }
        else
        {
            usedSprite.flipX = false;
        }
        isJumping = jump;
        if (!isJumping)
        {
            velocity.x = (x * walkSpeed) * mulDuringJump;
        }
        else
        {
            velocity.x = 0;
        }

        if (groundSmash)
        {
            jumpForce = -jumpSpeed * 1.2f;

        }
    }


    protected virtual void Update()
    {
    }

    void FixedUpdate()
    {
        if (isDamaged)
        {
            dmgTick += Time.fixedDeltaTime;
            usedSprite.enabled = !usedSprite.enabled;
            if (dmgTick > maxDmgTick)
            {
                isDamaged = false;
                usedSprite.enabled = true;
                dmgTick = 0f;
            }
        }
        if (rb)
        {
            if (jumpForce >= 0)
                jumpForce = Mathf.Max(0f, jumpForce - 0.2f);
            velocity.y = jumpForce;
            if (isGrounded && velocity.y != 0)
            {
                jumpForce = 0;
                velocity.y = 0;
            }
            RaycastHit2D hit = Physics2D.Raycast(transform.position + Vector3.up, Vector2.up, (coll.size.y / 4));
            if (hit.collider)
            {
                if (!hit.collider.isTrigger)
                {
                    jumpForce = 0;
                }
            }
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
            if (velocity.x != 0)
            {
                if (!isMoving)
                {
                    anim.Play("run");
                }
                isMoving = true;
            }
            else
            {
                if (!isMoving)
                {
                    anim.Play("idle");
                }
                isMoving = false;
            }
        }
    }
}
