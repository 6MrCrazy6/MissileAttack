using TMPro;
using UnityEngine;

public class Coins : MonoBehaviour
{
    public static int countMoney;
    public TextMeshProUGUI moneyText;

    private void Awake()
    {
        Debug.Log(PlayerPrefs.GetInt("AllMoney"));
    }

    void Update()
    {
        int money = PlayerPrefs.GetInt("Money");
        int allMoney = PlayerPrefs.GetInt("AllMoney");
        countMoney = allMoney + money;
        PlayerPrefs.SetInt("AllMoney", countMoney);
        PlayerPrefs.Save();
        PlayerPrefs.DeleteKey("Money");
        moneyText.text = PlayerPrefs.GetInt("AllMoney").ToString();
    }
}
