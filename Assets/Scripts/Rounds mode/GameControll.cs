using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameControll : MonoBehaviour
{
    EnemyMissileSpawner myEnemyMissileSpawner;

    public int score = 0;
    public int level = 1;
    public int missileLeft = 30;
    public int spaceHouseCounter = 0;
    public float enemyMissileSpeed = 0.5f;
    public int enemyMissilesThisRound = 15;
    private int enemyMissilesLeftInRound = 0;

    // Score values
    private int missileDestroyPoint = 25;

    [SerializeField] private GameObject endOfRoundPanel;
    [SerializeField] private GameObject gameOverPanel;

    [SerializeField] private TextMeshProUGUI myScoreText;
    [SerializeField] private TextMeshProUGUI myLevelText;
    [SerializeField] private TextMeshProUGUI myMissileLeftText;
    [SerializeField] private TextMeshProUGUI countDownText;
    [SerializeField] private TextMeshProUGUI finishScoreText;
    [SerializeField] private TextMeshProUGUI bestScoreText;

    [SerializeField] private int missileEndOfRoundPoints = 10;
    [SerializeField] private int spaceHouseEndOfRoundPoints = 100;
    [SerializeField] private float enemyMissileSpeedMultiplier = 0.5f;

    [SerializeField] private TextMeshProUGUI leftOverMissileBonusText;
    [SerializeField] private TextMeshProUGUI leftOverSpaceHouseBonusText;
    [SerializeField] private TextMeshProUGUI totalBonusText;
    [SerializeField] private TextMeshProUGUI CoinsText;

    [HideInInspector]
    private const string leaderboard = "CgkIlNKumJAHEAIQAg";

    public bool isRoundOver = false;
    public bool isGameOver = false;

    private void Awake()
    {
        missileLeft = 30;
        enemyMissilesThisRound = 30;
    }

    void Start()
    {
        Time.timeScale = 1f;

        myEnemyMissileSpawner = GameObject.FindObjectOfType<EnemyMissileSpawner>();
        spaceHouseCounter = GameObject.FindGameObjectsWithTag("Defenders").Length;
        Debug.Log(spaceHouseCounter);

        UpdateScoreText();
        UpdateLevelText();
        UpdateMissileLeftText();

        StartRound();
    }

    void Update()
    {
        if (EnemyMissilesDectroyed() && !isRoundOver)
        {
            isRoundOver = true;
            StartCoroutine(EndOfRound());
        }

        if(spaceHouseCounter == 0 && !isGameOver)
        {
            isGameOver = true;
            StartCoroutine(GameOver());   
        }
    }

    public void UpdateMissileLeftText()
    {
        myMissileLeftText.text = " " + missileLeft;
    }

    public void UpdateScoreText()
    {
        myScoreText.text = "Score: " + score;
    }

    public void UpdateLevelText()
    {
        myLevelText.text = "Level " + level;
    }

    public void AddMissileDestroyScore()
    {
        score += missileDestroyPoint;
        EnemyMissilesDestroyed();
        UpdateScoreText();
        UpdateMissileLeftText();
    }

    public void EnemyMissilesDestroyed()
    {
        enemyMissilesLeftInRound--;
    }

    public void StartRound()
    {
        myEnemyMissileSpawner.missileToSpawnThisRound = enemyMissilesThisRound;
        enemyMissilesLeftInRound = enemyMissilesThisRound;
        myEnemyMissileSpawner.StartRound();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
        isGameOver = false;
        isRoundOver = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        isGameOver = false;
        isRoundOver = false;
    }

    private bool EnemyMissilesDectroyed()
    {
        GameObject[] enemyMissiles = GameObject.FindGameObjectsWithTag("EnemyMissile");
        foreach (GameObject missile in enemyMissiles)
        {
            if (missile.activeSelf)
            {
                return false;
            }
        }

        return true;
    }

    public IEnumerator EndOfRound()
    {
        yield return new WaitForSeconds(2f);
        endOfRoundPanel.SetActive(true);
        int leftOverMissileBonus = missileLeft * missileEndOfRoundPoints;

        GameObject[] spaceHouse = GameObject.FindGameObjectsWithTag("Defenders");
        int leftSpaceHouseBonus = spaceHouse.Length * spaceHouseEndOfRoundPoints;

        int totalBonus = leftOverMissileBonus + leftSpaceHouseBonus;

        leftOverMissileBonusText.text = "Left over Missile Bonus: " + leftOverMissileBonus;
        leftOverSpaceHouseBonusText.text = "Left over SpaceHouse Bonus: " + leftSpaceHouseBonus;
        totalBonusText.text = "Total Bonus: " + totalBonus;

        score += totalBonus;
        level++;

        countDownText.text = "3";
        yield return new WaitForSeconds(1f);
        countDownText.text = "2";
        yield return new WaitForSeconds(1f);
        countDownText.text = "1";
        yield return new WaitForSeconds(1f);
        countDownText.text = "0";
        yield return new WaitForSeconds(0.5f);

        endOfRoundPanel.SetActive(false);

        isRoundOver = false;

        // Updateting new setting round
        missileLeft = 30;
        enemyMissileSpeed += enemyMissileSpeedMultiplier;

        StartRound();
        UpdateLevelText();
        UpdateMissileLeftText();
        UpdateScoreText();
    }

    public IEnumerator GameOver()
    {
        yield return new WaitForSeconds(0f);
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        gameOverPanel.SetActive(true);
        isGameOver = true;
        int bestScore = PlayerPrefs.GetInt("BestScore");
        int coins = score / 100;
        PlayerPrefs.SetInt("Money", coins);
        if (score > bestScore) PlayerPrefs.SetInt("BestScore", score);
        bestScore = PlayerPrefs.GetInt("BestScore");
        finishScoreText.text = "Score: " + score;
        bestScoreText.text = "Best Score: " + bestScore;
        CoinsText.text = "Coins: " + coins;
        Social.ReportScore(bestScore, leaderboard, (bool success) =>
        {
            if (success)
            {
                Debug.Log("Score submitted to Leaderboard successfully");
            }
            else
            {
                Debug.Log("Score submission to Leaderboard failed");
            }
        });
    }
}
