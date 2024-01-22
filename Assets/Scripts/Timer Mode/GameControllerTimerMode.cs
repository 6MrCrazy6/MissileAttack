using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameControllerTimerMode : MonoBehaviour
{
    EnemyMissileSpawnTimerMode myEnemyMissileSpawner;

    private int score = 0;
    public int spaceHouseCounter = 0;
    public float enemyMissileSpeed = 0.5f;
    public int destroyedEnemyMissile = 0;
    private int totalScore = 0;
    private int bestScore = 0;

    private float startTime;
    private float remainingTime;
    private float timerDuration = 60f;

    private float timeSinceLastSpeedIncrease = 0f; // Потрібна для відслідковування часу 
    private float speedIncreaseInterval = 20f;
    private float speedIncreaseAmount = 0.5f;

    private int missileDestroyPoint = 25;

    [SerializeField] private GameObject endTimePanel;
    [SerializeField] private GameObject gameOverPanel;

    [SerializeField] private TextMeshProUGUI myScoreText;
    [SerializeField] private TextMeshProUGUI myTimeText;

    //EndTimePanel
    [SerializeField] private TextMeshProUGUI endTimeScoreText;
    [SerializeField] private TextMeshProUGUI endTimeBestScore;
    [SerializeField] private TextMeshProUGUI bonusOfDestroyedEnemyMissileText;
    [SerializeField] private TextMeshProUGUI totalScoreText;
    [SerializeField] private TextMeshProUGUI coinsText;

    //GameOverPannel
    [SerializeField] private TextMeshProUGUI gameOverScoreText;
    [SerializeField] private TextMeshProUGUI gameOverBestScoreText;

    [SerializeField] private float enemyMissileSpeedMultiplier = 0.5f;

    public bool isTimerRunning = true;
    public bool isGameOver = false;
    private bool isPaused;

    void Start()
    {
        Time.timeScale = 1f;
        startTime = Time.time;

        myEnemyMissileSpawner = GameObject.FindObjectOfType<EnemyMissileSpawnTimerMode>();
        spaceHouseCounter = GameObject.FindGameObjectsWithTag("Defenders").Length;
        Debug.Log(spaceHouseCounter);

        UpdateScoreText();

        StartRound();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPaused)
        {
            remainingTime = Mathf.Max(timerDuration - (Time.time - startTime), 0f);

            if (remainingTime <= 0f)
            {
                remainingTime = 0f;
                Time.timeScale = 0f;
                isTimerRunning = false;
                StartCoroutine(EndTime());
            }

            int minutesInt = Mathf.FloorToInt(remainingTime / 60);
            int secondsInt = Mathf.FloorToInt(remainingTime % 60);

            myTimeText.text = "Time: " + minutesInt.ToString("D2") + ":" + secondsInt.ToString("D2");

            timeSinceLastSpeedIncrease += Time.unscaledDeltaTime;
            if (timeSinceLastSpeedIncrease >= speedIncreaseInterval)
            {
                enemyMissileSpeed += speedIncreaseAmount;
                timeSinceLastSpeedIncrease = 0f;
            }

            if (spaceHouseCounter == 0)
            {
                Time.timeScale = 0f;
                isTimerRunning = false;
                StartCoroutine(GameOver());
            }
        }
    }

    public void UpdateScoreText()
    {
        myScoreText.text = "Score: " + score;
    }

    public void AddMissileDestroyScore()
    {
        score += missileDestroyPoint;
        destroyedEnemyMissile += 1;
        UpdateScoreText();
    }

    public void StartRound()
    {
        myEnemyMissileSpawner.StartRound();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
        isGameOver = false;
        isTimerRunning = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        isGameOver = false;
        isTimerRunning = true;
    }

    public void ResumeTimer()
    {
        if (!isPaused)
            return;

        isPaused = false;
        startTime = Time.time - (timerDuration - remainingTime);
        Time.timeScale = 1f;
    }

    public void PauseTimer()
    {
        if (isPaused)
            return;

        isPaused = true;
        Time.timeScale = 0f;
    }

    public IEnumerator EndTime()
    {
        yield return new WaitForSeconds(0f);
        endTimePanel.SetActive(true);
        isTimerRunning = false;

        bestScore = PlayerPrefs.GetInt("BestScoreTimerMode");
        if (score > bestScore) PlayerPrefs.SetInt("BestScoreTimerMode", score);
        bestScore = PlayerPrefs.GetInt("BestScoreTimerMode");

        int coins = totalScore / 100;
        PlayerPrefs.SetInt("Money", coins);

        int bonusDestroyedEnemyMissile = destroyedEnemyMissile * 10;
        totalScore = bonusDestroyedEnemyMissile + score;

        bonusOfDestroyedEnemyMissileText.text = "Bonus of enemy missiles destroyed: " + bonusDestroyedEnemyMissile;
        endTimeScoreText.text = "Score: " + score;
        endTimeBestScore.text = "Best Score: " + bestScore;
        totalScoreText.text = "Total Score: " + totalScore;
        coinsText.text = "Coins: " + coins;
    }

    public IEnumerator GameOver()
    {
        yield return new WaitForSeconds(0f);
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        gameOverPanel.SetActive(true);
        isGameOver = true;
        isTimerRunning = false;

        bestScore = PlayerPrefs.GetInt("BestScoreTimerMode");
        if (score > bestScore) PlayerPrefs.SetInt("BestScoreTimerMode", score);
        bestScore = PlayerPrefs.GetInt("BestScoreTimerMode");

        gameOverScoreText.text = "Score: " + score;
        gameOverBestScoreText.text = "Best Score: " + bestScore;
    }
}
