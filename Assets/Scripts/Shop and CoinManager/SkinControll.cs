using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkinControll : MonoBehaviour
{
    public int skinNum;
    public Button buyButton;
    public int price;

    public Image[] skins;

    public TextMeshProUGUI buyTextButton;

    void Start()
    {
        if (PlayerPrefs.GetInt("skin1" + "buy") == 0)
        {
            foreach (Image img in skins)
            {
                if ("skin1" == img.name)
                {
                    PlayerPrefs.SetInt("skin1" + "buy", 1);
                    PlayerPrefs.SetInt("skin1" + "equip", 1);
                }
                else
                {
                    PlayerPrefs.SetInt(GetComponent<Image>().name + "buy", 0);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt(GetComponent<Image>().name + "buy") == 0)
        {
            buyTextButton.text = price + " coins";
        }
        else if (PlayerPrefs.GetInt(GetComponent<Image>().name + "buy") == 1)
        {
            buyTextButton.text = "equip";
            if (PlayerPrefs.GetInt(GetComponent<Image>().name + "equip") == 1)
            {
                buyTextButton.text = "equipped";
            }
            else if (PlayerPrefs.GetInt(GetComponent<Image>().name + "equip") == 0)
            {
                buyTextButton.text = "equip";
            }
        }
    }
    public void Buy()
    {
        if (PlayerPrefs.GetInt(GetComponent<Image>().name + "buy") == 0)
        {
            if (Coins.countMoney >= price)
            {
                PlayerPrefs.SetInt("AllMoney", PlayerPrefs.GetInt("AllMoney") - price);
                PlayerPrefs.SetInt(GetComponent<Image>().name + "buy", 1);
                PlayerPrefs.SetInt("skinNum", skinNum);

                foreach (Image img in skins)
                {
                    if (GetComponent<Image>().name == img.name)
                    {
                        PlayerPrefs.SetInt(GetComponent<Image>().name + "equip", 1);
                    }
                    else
                    {
                        PlayerPrefs.SetInt(img.name + "equip", 0);
                    }
                }
            }
        }
        else if (PlayerPrefs.GetInt(GetComponent<Image>().name + "buy") == 1)
        {

            PlayerPrefs.SetInt(GetComponent<Image>().name + "equip", 1);
            PlayerPrefs.SetInt("skinNum", skinNum);

            foreach (Image img in skins)
            {
                if (GetComponent<Image>().name == img.name)
                {
                    PlayerPrefs.SetInt(GetComponent<Image>().name + "equip", 1);
                }
                else
                {
                    PlayerPrefs.SetInt(img.name + "equip", 0);
                }
            }
        }
    }
}
