using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{

    public int direction = 1;
    public Vector2 _direction = new Vector2(0,0);
    public bool collide = true;
    public float time = 3f;

    private float speed = 15;
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private float timer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        rb.velocity = new Vector2(speed * direction, 0.0f);
        if (_direction.x > 0 || _direction.x < 0 || _direction.y > 0 || _direction.y < 0)
            rb.velocity = _direction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collide)
        {
            if (collision.gameObject.layer != 14 && collision.gameObject.layer != 23)
            {
                if (collision.gameObject.layer == 16)
                    collision.gameObject.GetComponent<Enemy>().KillEnemy(true);
                Destroy(this.gameObject);
            }
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer > time)
            Destroy(this.gameObject);
    }
}
