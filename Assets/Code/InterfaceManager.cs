using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class InterfaceManager : MonoBehaviour
{

    public float scoreAmount;
    public float timeLeft;
    public float livesLeft;
    public float coinsAmount;
    public float enemiesLeft;

    [Header("Stats")]
    public Text textScore;
    public Text textTime;
    public Text textLives;
    public Text textCoins;
    public Text textEnemies;

    [Header("Healthbar")]
    public RawImage healthbar;
    private float hpWidth;

    [Header("Pause Screen")]
    public GameObject pauseGroup;
    public Button pauseButtonContinue;
    public Button pauseButtonQuit;

    [Header("Game Over")]
    public GameObject gameover_ui;

    [Header("Win Screen")]
    public GameObject winscreen_ui;

    private GameManager gamemanager;

    public InterfaceManager()
    {
        scoreAmount = 0;
        timeLeft = 300;
        livesLeft = 3;
        coinsAmount = 0;
    }

    public void AddScore(float score)
    {
        scoreAmount += score;
        textScore.text = "Score: " + Mathf.RoundToInt(scoreAmount);
    }

    public void SetScore(float score)
    {
        scoreAmount = score;
        textScore.text = "Score: " + Mathf.RoundToInt(scoreAmount);
    }

    public float GetScore()
    {
        return scoreAmount;
    }

    public void AddTime(float time)
    {
        timeLeft += time;
        textTime.text = "Time left: " + Mathf.RoundToInt(timeLeft);
    }

    public void SetTime(float time)
    {
        timeLeft = time;
        textTime.text = "Time left: " + Mathf.RoundToInt(timeLeft);
    }

    public float GetTime()
    {
        return timeLeft;
    }

    public void AddLife(float life)
    {
        livesLeft += life;
        textLives.text = "Lives: " + livesLeft;
    }

    public void SetLife(float life)
    {
        livesLeft = life;
        textLives.text = "Lives: " + livesLeft;
    }

    public float GetLives()
    {
        return livesLeft;
    }

    public void AddCoin(float coin)
    {
        coinsAmount += coin;
        textCoins.text = "Coins: " + coinsAmount;
    }

    public void SetCoin(float coin)
    {
        coinsAmount = coin;
        textCoins.text = "Coins: " + coinsAmount;
    }

    public float GetCoins()
    {
        return coinsAmount;
    }

    public void SetEnemies(float enemies)
    {
        enemiesLeft = enemies;
        textEnemies.text = "Enemies left: " + enemiesLeft;
    }

    void Start()
    {
        gamemanager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        pauseButtonContinue.onClick.AddListener(ContinueGame);
        pauseButtonQuit.onClick.AddListener(ExitGame);
        AddCoin(0);
        AddScore(0);
        AddLife(0);

        hpWidth = healthbar.GetComponent<RectTransform>().sizeDelta.x;
    }

    void Update()
    {
        if (!gamemanager.isPaused)
        {
            AddTime(-Time.deltaTime);
            if (GetTime() <= 0)
            {
                gamemanager.GameOver();
            }
        }

        if (Input.GetKeyDown("escape"))
        {
            gamemanager.isPaused = true;
            pauseGroup.SetActive(true);
            gamemanager.pausemanager.SetPaused(true);
        }

        if (gamemanager.player)
        {
            healthbar.GetComponent<RectTransform>().sizeDelta = new Vector2((int)(hpWidth / gamemanager.player.GetComponent<Actor>().start_healthPoints * gamemanager.player.GetComponent<Actor>().healthPoints), 25);
        }
    }

    public void SwitchGameover()
    {
        gameover_ui.SetActive(!gameover_ui.activeSelf);
        textCoins.color = Color.white;
        textLives.color = Color.white;
        textTime.color = Color.white;
        textScore.color = Color.white;
    }

    private void ContinueGame()
    {
        gamemanager.pausemanager.SetPaused(false);
        pauseGroup.SetActive(false);
    }

    private void ExitGame()
    {
        SceneManager.LoadScene("mainmenu");
    }
}
