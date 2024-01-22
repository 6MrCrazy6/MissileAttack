using GooglePlayGames;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [HideInInspector]
    private const string leaderboard = "CgkIlNKumJAHEAIQAg";

    private void Start()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
        Social.localUser.Authenticate(success =>
        {
            if (success)
            {
                Debug.Log("User authenticate");
                AddScore();
            }
            else
            {
                Debug.Log("User not authenticate");
            }
        });
    }

    void AddScore()
    {
        int bestScore = PlayerPrefs.GetInt("BestScore");
        Debug.Log(bestScore);
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

    public void ShowLeaderBoard()
    {
        Social.ShowLeaderboardUI();
    }

    public void ShowAchivements()
    {
        Social.ShowAchievementsUI();
    }

    public void RoundsMode()
    {
        SceneManager.LoadScene("MissileGame");
    }

    public void TimeMode()
    {
        SceneManager.LoadScene("MissileGameWithTime");
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game Close");
    }


    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
