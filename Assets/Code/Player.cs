using UnityEngine;
using System.Collections;
using GameInput;

public class Player : Actor
{

    [Header("Player")]

    private AudioSource[] audioSources;
    private AudioSource music;
    public AudioSource sfx;

    private Gun gun;

    public string playerID;

    private ControlScheme keyControls;
    private string playStyle = "keyboard";

    protected override void Awake()
    {
        base.Awake();

        gun = GetComponent<Gun>();
        healthPoints -= (healthPoints / 3 * PlayerPrefs.GetFloat("difficulty")) / 2;

    }

    protected override void Start()
    {
        base.Start();

        audioSources = GetComponents<AudioSource>();
        music = audioSources[0];
        sfx = audioSources[1];
        music.loop = true;
        music.clip = SoundManager.GetAudio("cave_themeb4");
        music.Play();

        int i = 0;
        foreach (ControlScheme controlScheme in InputManager.GetControlSchemes())
        {
            i++;
            if (playerID == "" + i)
                keyControls = controlScheme;
        }
    }

    protected override void Update()
    {
        base.Update();

        GetInput();
    }

    void GetInput()
    {
        float hori = 0;
        bool jump = false;
        bool shoot = false;


        if (gamemanager.isEndAnimation != true)
        {
            if (playStyle == "keyboard")
            {
                KeyCode leftKey;
                KeyCode rightKey;
                KeyCode jumpKey;
                KeyCode shootKey;

                keyControls.keyInput.TryGetValue("Left", out leftKey);
                keyControls.keyInput.TryGetValue("Right", out rightKey);
                keyControls.keyInput.TryGetValue("Jump", out jumpKey);
                keyControls.keyInput.TryGetValue("Shoot", out shootKey);

                if (Input.GetKey(leftKey))
                    hori = -1;
                if (Input.GetKey(rightKey))
                    hori = 1;

                if (isGrounded == true)
                {
                    if (Input.GetKey(jumpKey))
                        jump = true;
                }

                if (Input.GetKey(shootKey))
                    shoot = true;
            }
        }

        if (shoot)
        {
            int dir;
            if (hori < 0)
                dir = -1;
            else
                dir = 1;
            gun.Shoot(dir);
        }

        if (jump && isGrounded == true)
        {
            sfx.PlayOneShot(SoundManager.GetAudio("jump"), 1.0F);
        }

        Move(hori, jump, false);
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.layer == 13)
        {
            gamemanager.KillPlayer();
        }
        if (c.gameObject.layer == 15)
        {
            c.gameObject.GetComponent<Coin>().GrabCoin();
            sfx.PlayOneShot(SoundManager.GetAudio("coin_pick"), 1.0F);
        }
        if (c.gameObject.layer == 17)
        {
            gamemanager.EndGame();
        }
        if (c.gameObject.tag == "BossEnterArea")
        {
            c.gameObject.SetActive(false);
            GameObject.FindGameObjectWithTag("Boss").GetComponent<Boss>().SpawnBoss();
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>().StartBossCam();
            foreach (GameObject coin in gamemanager.coins)
            {
                coin.SetActive(false);
            }
            foreach (GameObject enemy in gamemanager.enemies)
            {
                enemy.SetActive(false);
            }
        }
    }
}
