using UnityEngine;
using System.Collections;

public class Enemy : Actor
{
    public bool isNegative;
    public bool isTargetting = false;

    private float random;
    private bool jump = false;

    public float targetSpeed = 10.0f;

    private BoxCollider2D enemyCollider;

    private GameObject player;

    protected override void Start()
    {
        base.Start();

        isNegative = true;
        Move(-0.5f, false, false);
        enemyCollider = transform.GetComponent<BoxCollider2D>();
        player = GameObject.FindWithTag("CharacterPlayer");
    }

    protected override void Update()
    {
        base.Update();

        if (!isDead)
        {
            jump = false;
            RaycastHit2D hitLeft = Physics2D.Raycast(transform.position + Vector3.left * 0.5f, Vector2.left + Vector2.down * 0.5f, enemyCollider.size.x / 2 + 0.05f);
            Debug.DrawRay(transform.position + Vector3.left * 0.5f, Vector2.left + Vector2.down * 0.5f, Color.green);

            RaycastHit2D hitRight = Physics2D.Raycast(transform.position + Vector3.right * 0.5f, Vector2.right + Vector2.down * 0.5f, enemyCollider.size.x / 2 + 0.05f);
            Debug.DrawRay(transform.position + Vector3.right * 0.5f, Vector2.right + Vector2.down * 0.5f, Color.green);

            RaycastHit2D playerhitAbove = Physics2D.Raycast(transform.position + Vector3.up, Vector2.up, enemyCollider.size.y / 2 + 0.05f);
            random = Random.Range(0.0f, 2.0f);
            Caster(transform.position, 8);
            if (playerhitAbove.collider && playerhitAbove.collider.gameObject.layer == 14)
                KillEnemy();
            if (hitLeft.collider && hitLeft.collider.gameObject.layer == 14)
            {
                dpsTick += Time.deltaTime;
                if (dpsTick > 0)
                {
                    gamemanager.DamagePlayer(DPS);
                    dpsTick = -DPS;
                }
            }
            if (hitRight.collider && hitRight.collider.gameObject.layer == 14)
            {
                dpsTick += Time.deltaTime;
                if (dpsTick > 0)
                {
                    gamemanager.DamagePlayer(DPS);
                    dpsTick = -DPS;
                }
            }
            if (isNegative && hitLeft.collider)
            {
                if (random < 1.0f && !isTargetting)
                    isNegative = false;
                else
                    jump = true;
            }
            if (!isNegative && hitRight.collider)
            {
                if (random < 1.0f && !isTargetting)
                    isNegative = true;
                else
                    jump = true;
            }
            random = Random.Range(0.0f, 2.0f);
            if (random < 0.02f && !isTargetting)
                isNegative = !isNegative;
            targetSpeed = 6.0f;
            if (isTargetting)
            {
                walkSpeed = targetSpeed;
                if (player.transform.position.x != transform.position.x)
                {
                    if (player.transform.position.x < transform.position.x)
                        isNegative = true;
                    else if (player.transform.position.x > transform.position.x)
                        isNegative = false;
                }
            }
            if (player.transform.position.x != transform.position.x)
            {
                if (isNegative)
                    Move(-0.5f, jump);
                else
                    Move(0.5f, jump);
            }
        }
    }

    void Caster(Vector2 center, float radius)
    {
        isTargetting = false;
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(center, radius);
        int i = 0;
        while (i < hitColliders.Length)
        {
            if (hitColliders[i].gameObject.layer == 14)
            {
                isTargetting = true;
                break;
            }
            i++;
        }
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.layer == 13)
            KillEnemy(false);
    }

    public void KillEnemy(bool score = true)
    {
        isDead = true;
        if (score)
            gamemanager.interfacemanager.AddScore(200);
        gameObject.SetActive(false);
    }
}
