using GooglePlayGames;
using UnityEngine;
using UnityEngine.Events;

public class AchievementsManager : MonoBehaviour
{
    public GameControll myGameControll;
    public AchievementsManager achievementsManager;
    private UnityEvent OnAchievementUnlock;

    private bool welcomeAchievementUnlocked = false;
    private bool loseEverythingAchievementUnlocked = false;
    private bool allMineAchievementUnlocked = false;
    private bool myGoldAchievementUnlocked = false;
    private bool myGoldenVaultAchievementUnlocked = false;
    private bool theFirstBattleAchievementUnlocked = false;
    private bool humFromSpaceAchievementUnlocked = false;

    private int coins;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("AllMoney"))
            coins = 0;
        else
            coins = PlayerPrefs.GetInt("AllMoney");
    }

    private void Start()
    {
        achievementsManager = GetComponent<AchievementsManager>();

        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
        Social.localUser.Authenticate(success =>
        {
            if (success)
            {
                Debug.Log("User authenticate");
                if (!welcomeAchievementUnlocked)
                {
                    UnlockAchievement(GPGSIds.achievement_welcome, 5);
                    welcomeAchievementUnlocked = true;
                }
            }
            else
            {
                Debug.Log("User not authenticate");
            }
        });

        OnAchievementUnlock.AddListener(UpdateCoins);
    }

    private void Update()
    {
        if (!loseEverythingAchievementUnlocked && myGameControll.level == 1 && myGameControll.missileLeft == 30 && myGameControll.isGameOver == true)
        {
            UnlockAchievement(GPGSIds.achievement_lose_everything, 2);
            loseEverythingAchievementUnlocked = true;
        }

        if (!allMineAchievementUnlocked && myGameControll.spaceHouseCounter == 4 && myGameControll.level > 4)
        {
            UnlockAchievement(GPGSIds.achievement_all_mine, 5);
        }

        if (!myGoldAchievementUnlocked && coins >= 10)
        {
            UnlockAchievement(GPGSIds.achievement_my_gold, 3);
        }

        if (!myGoldenVaultAchievementUnlocked && coins >= 1000)
        {
            UnlockAchievement(GPGSIds.achievement_my_golden_vault, 20);
        }

        if (!theFirstBattleAchievementUnlocked && myGameControll.level == 6)
        {
            UnlockAchievement(GPGSIds.achievement_the_first_battle, 5);
        }

        if (!humFromSpaceAchievementUnlocked && myGameControll.level == 6 && myGameControll.spaceHouseCounter == 4)
        {
            UnlockAchievement(GPGSIds.achievement_hum_from_space, 10);
        }
    }

    private void UpdateCoins()
    {
        PlayerPrefs.SetInt("AllMoney", coins);
        PlayerPrefs.Save();
    }

    public void UnlockAchievement(string achievementId, int rewardCoins)
    {
        if (Social.localUser.authenticated)
        {
            bool achievementUnlocked = PlayerPrefs.GetInt(achievementId + "_Unlocked", 0) == 1;

            if (!achievementUnlocked)
            {
                Social.ReportProgress(achievementId, 100.0f, (bool success) =>
                {
                    if (success)
                    {
                        Debug.Log("Достижение разблокировано: " + achievementId);

                        bool coinsAdded = PlayerPrefs.GetInt(achievementId + "_CoinsAdded", 0) == 1;

                        if (!coinsAdded)
                        {
                            coins += rewardCoins;
                            UpdateCoins();
                            PlayerPrefs.SetInt(achievementId + "_CoinsAdded", 1);
                        }

                        PlayerPrefs.SetInt(achievementId + "_Unlocked", 1);
                        PlayerPrefs.Save();

                        OnAchievementUnlock.Invoke();
                    }
                    else
                    {
                        Debug.LogWarning("Не удалось разблокировать достижение: " + achievementId);
                    }
                });
            }
        }
        else
        {
            Debug.LogWarning("Пользователь не аутентифицирован. Невозможно разблокировать достижение: ");
        }
    }
}
