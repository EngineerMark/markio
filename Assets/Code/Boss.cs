using UnityEngine;
using System.Collections;

public class Boss : MonoBehaviour {

    public bool hasSpawned = false;
    public string state = "despawned";
    public string mood = "mad";
    public Sprite spriteMad;
    public Sprite spriteNeutral;
    public Sprite spriteHappy;

    public float health = 100;
    private float startingHealth;

    private SpriteRenderer spriteRenderer;

    private Rigidbody2D rb;

    public float tick;

    void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0);

        float difficulty = 1;
        if (PlayerPrefs.HasKey("difficulty"))
            difficulty = PlayerPrefs.GetFloat("difficulty");

        health *= difficulty;

        startingHealth = health;

    }
	
	void Update () {
        if (hasSpawned)
        {
            if (state == "spawning")
            {
                tick += Time.deltaTime;
                tick = Mathf.Clamp(tick, 0, 1);
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, tick);
                if (spriteRenderer.color.a == 1)
                    state = "spawned";
            }

            if (mood == "mad")
                spriteRenderer.sprite = spriteMad;
            if (mood == "neutral")
                spriteRenderer.sprite = spriteNeutral;
            if (mood == "happy")
                spriteRenderer.sprite = spriteHappy;

            float hpProgress = 100 / startingHealth * health;
            if (hpProgress <= 100 / 3)
                mood = "happy";
            else if (hpProgress > 100 / 3 && hpProgress < 100 / 3 * 2)
                mood = "neutral";
            else
                mood = "mad";
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
	}

    public void SpawnBoss()
    {
        hasSpawned = true;
        state = "spawning";
    }
}
