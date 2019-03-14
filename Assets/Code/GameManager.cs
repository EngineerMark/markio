using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{

    [Header("Player")]
    public GameObject player;
    private Player playerController;
    public Vector3 playerSpawnPoint;

    [Header("Global")]
    public bool isPaused;
    public bool isGameWon;

    [Header("Statistics")]
    public float scoreAmount;
    public float timeLeft;
    public float livesLeft;
    public float coinsAmount;

    [Header("Enemies")]
    public GameObject[] enemies;
    private float enemiesLeft;

    [Header("Coins")]
    public GameObject[] coins;

    [Header("Clouds")]
    public GameObject[] clouds;
    public Vector2 cloudBorderPoints;

    [Header("Level Passing")]
    public Vector2 endPoleX;
    public Vector2 endPoleY;
    public bool isEndAnimation = false;

    [Header("Sound")]
    public AudioClip soundJump;
    public AudioClip soundDeath;
    public AudioClip soundPickupCoin;
    public AudioClip soundBackgroundMusic;

    [Header("Managers")]
    public InterfaceManager interfacemanager;
    public PauseManager pausemanager;

    [SerializeField]
    private GameObject eventSystem;

    public Manager[] managers;

    private void Awake()
    {
        interfacemanager = eventSystem.GetComponent<InterfaceManager>();
        pausemanager = eventSystem.GetComponent<PauseManager>();
        playerController = player.GetComponent<Player>();
        clouds = GameObject.FindGameObjectsWithTag("Cloud");
        foreach (GameObject cloud in clouds)
            cloud.AddComponent<Cloud>();

        managers = new Manager[]{
          new InputManager(),
          new ActorManager(),
          new SoundManager()
        };

        SoundManager.AddAudio(soundJump);
        SoundManager.AddAudio(soundPickupCoin);
        SoundManager.AddAudio(soundBackgroundMusic);
    }

    void Start()
    {
        UpdateObjectList();
    }

    public void UpdateObjectList()
    {
        enemies = GameObject.FindGameObjectsWithTag("CharacterEnemy");
        foreach (GameObject enemy in enemies)
        {
            foreach (GameObject enemyCollided in enemies)
            {
                if (enemy != enemyCollided)
                    Physics2D.IgnoreCollision(enemy.GetComponent<BoxCollider2D>(), enemyCollided.GetComponent<BoxCollider2D>());
            }
        }
        coins = GameObject.FindGameObjectsWithTag("Coin");
    }

    void Update()
    {
        scoreAmount = interfacemanager.GetScore();
        timeLeft = interfacemanager.GetTime();
        livesLeft = interfacemanager.GetLives();
        coinsAmount = interfacemanager.GetCoins();
        isPaused = pausemanager.GetPaused();
        enemiesLeft = 0.0f;
        foreach (GameObject enemy in enemies)
        {
            if (enemy.activeSelf)
                enemiesLeft++;
        }
        if (isGameWon)
        {
            if (Input.GetKey("space"))
            {
                isGameWon = false;
                ResetGame(true);
                interfacemanager.winscreen_ui.SetActive(false);
                pausemanager.SetSoftPaused(false);
                isEndAnimation = false;
            }
            if (Input.GetKey("escape"))
                SceneManager.LoadScene("mainmenu");
        }
        interfacemanager.SetEnemies(enemiesLeft);
    }

    public void DamagePlayer(float dmg)
    {
        player.GetComponent<Player>().healthPoints -= dmg;
        player.GetComponent<Player>().isDamaged = true;
        if (player.GetComponent<Player>().healthPoints < 1)
            KillPlayer();
    }

    public void KillPlayer()
    {
        interfacemanager.AddLife(-1);
        if (interfacemanager.GetLives() == 0)
            GameOver();
        ResetGame(false);
    }

    public void ResetGame(bool stats)
    {
        ActorManager.Reset();
        if (stats)
        {
            interfacemanager.SetLife(3);
            interfacemanager.SetScore(0);
            interfacemanager.SetCoin(0);
            interfacemanager.SetTime(300);
        }
        foreach (GameObject coin in coins)
        {
            if (!coin.activeSelf)
                coin.SetActive(true);
        }
    }

    public void EndGame()
    {
        isEndAnimation = true;
        playerController.velocity = new Vector2(0.0f, 0.0f);
        pausemanager.SetSoftPaused(true);
        isGameWon = true;
        interfacemanager.winscreen_ui.SetActive(true);
    }

    public void GameOver()
    {
        interfacemanager.SwitchGameover();
        pausemanager.SetPaused(true);
    }
}
