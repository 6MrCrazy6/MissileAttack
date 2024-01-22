using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkinControllEnemyMissle : MonoBehaviour
{
    public int skinNum;
    public Button buyButton;
    public int price;

    public Image[] skins;

    public TextMeshProUGUI buyTextButton;

    void Start()
    {
        if (PlayerPrefs.GetInt("Enemyskin1" + "Enemybuy") == 0)
        {
            foreach (Image img in skins)
            {
                if ("Enemyskin1" == img.name)
                {
                    PlayerPrefs.SetInt("Enemyskin1" + "Enemybuy", 1);
                    PlayerPrefs.SetInt("Enemyskin1" + "Enemyequip", 1);
                }
                else
                {
                    PlayerPrefs.SetInt(GetComponent<Image>().name + "Enemybuy", 0);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt(GetComponent<Image>().name + "Enemybuy") == 0)
        {
            buyTextButton.text = price + " coins";
        }
        else if (PlayerPrefs.GetInt(GetComponent<Image>().name + "Enemybuy") == 1)
        {
            buyTextButton.text = "equip";
            if (PlayerPrefs.GetInt(GetComponent<Image>().name + "Enemyequip") == 1)
            {
                buyTextButton.text = "equipped";
            }
            else if (PlayerPrefs.GetInt(GetComponent<Image>().name + "Enemyequip") == 0)
            {
                buyTextButton.text = "equip";
            }
        }
    }
    public void Buy()
    {
        if (PlayerPrefs.GetInt(GetComponent<Image>().name + "Enemybuy") == 0)
        {
            if (Coins.countMoney >= price)
            {
                PlayerPrefs.SetInt("AllMoney", PlayerPrefs.GetInt("AllMoney") - price);
                PlayerPrefs.SetInt(GetComponent<Image>().name + "Enemybuy", 1);
                PlayerPrefs.SetInt("EnemyskinNum", skinNum);

                foreach (Image img in skins)
                {
                    if (GetComponent<Image>().name == img.name)
                    {
                        PlayerPrefs.SetInt(GetComponent<Image>().name + "Enemyequip", 1);
                    }
                    else
                    {
                        PlayerPrefs.SetInt(img.name + "Enemyequip", 0);
                    }
                }
            }
        }
        else if (PlayerPrefs.GetInt(GetComponent<Image>().name + "Enemybuy") == 1)
        {

            PlayerPrefs.SetInt(GetComponent<Image>().name + "Enemyequip", 1);
            PlayerPrefs.SetInt("EnemyskinNum", skinNum);

            foreach (Image img in skins)
            {
                if (GetComponent<Image>().name == img.name)
                {
                    PlayerPrefs.SetInt(GetComponent<Image>().name + "Enemyequip", 1);
                }
                else
                {
                    PlayerPrefs.SetInt(img.name + "Enemyequip", 0);
                }
            }
        }
    }
}
