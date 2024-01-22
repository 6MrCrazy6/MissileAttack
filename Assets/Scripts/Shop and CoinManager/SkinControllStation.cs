using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkinControllStation : MonoBehaviour
{
    public int skinNum;
    public Button buyButton;
    public int price;

    public Image[] skins;

    public TextMeshProUGUI buyTextButton;

    void Start()
    {
        if (PlayerPrefs.GetInt("Stationskin1" + "Stationbuy") == 0)
        {
            foreach (Image img in skins)
            {
                if ("Stationskin1" == img.name)
                {
                    PlayerPrefs.SetInt("Stationskin1" + "Stationbuy", 1);
                    PlayerPrefs.SetInt("Stationskin1" + "Stationequip", 1);
                }
                else
                {
                    PlayerPrefs.SetInt(GetComponent<Image>().name + "Stationbuy", 0);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt(GetComponent<Image>().name + "Stationbuy") == 0)
        {
            buyTextButton.text = price + " coins";
        }
        else if (PlayerPrefs.GetInt(GetComponent<Image>().name + "Stationbuy") == 1)
        {
            buyTextButton.text = "equip";
            if (PlayerPrefs.GetInt(GetComponent<Image>().name + "Stationequip") == 1)
            {
                buyTextButton.text = "equipped";
            }
            else if (PlayerPrefs.GetInt(GetComponent<Image>().name + "Stationequip") == 0)
            {
                buyTextButton.text = "equip";
            }
        }
    }
    public void Buy()
    {
        if (PlayerPrefs.GetInt(GetComponent<Image>().name + "Stationbuy") == 0)
        {
            if (Coins.countMoney >= price)
            {
                PlayerPrefs.SetInt("AllMoney", PlayerPrefs.GetInt("AllMoney") - price);
                PlayerPrefs.SetInt(GetComponent<Image>().name + "Stationbuy", 1);
                PlayerPrefs.SetInt("StationskinNum", skinNum);

                foreach (Image img in skins)
                {
                    if (GetComponent<Image>().name == img.name)
                    {
                        PlayerPrefs.SetInt(GetComponent<Image>().name + "Stationequip", 1);
                    }
                    else
                    {
                        PlayerPrefs.SetInt(img.name + "Stationequip", 0);
                    }
                }
            }
        }
        else if (PlayerPrefs.GetInt(GetComponent<Image>().name + "Stationbuy") == 1)
        {

            PlayerPrefs.SetInt(GetComponent<Image>().name + "Stationequip", 1);
            PlayerPrefs.SetInt("StationskinNum", skinNum);

            foreach (Image img in skins)
            {
                if (GetComponent<Image>().name == img.name)
                {
                    PlayerPrefs.SetInt(GetComponent<Image>().name + "Stationequip", 1);
                }
                else
                {
                    PlayerPrefs.SetInt(img.name + "Stationequip", 0);
                }
            }
        }
    }

}
