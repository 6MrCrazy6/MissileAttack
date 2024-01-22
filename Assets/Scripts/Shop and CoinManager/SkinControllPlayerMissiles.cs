using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class SkinControllPlayerMissiles : MonoBehaviour
{
    public int skinNum;
    public Button buyButton;
    public int price;

    public Image[] skins;

    public TextMeshProUGUI buyTextButton;

    void Start()
    {
        if (PlayerPrefs.GetInt("PlayerMissileskin1" + "PlayerMissilebuy") == 0)
        {
            foreach (Image img in skins)
            {
                if ("PlayerMissileskin1" == img.name)
                {
                    PlayerPrefs.SetInt("PlayerMissileskin1" + "PlayerMissilebuy", 1);
                    PlayerPrefs.SetInt("PlayerMissileskin1" + "PlayerMissileequip", 1);
                }
                else
                {
                    PlayerPrefs.SetInt(GetComponent<Image>().name + "PlayerMissilebuy", 0);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt(GetComponent<Image>().name + "PlayerMissilebuy") == 0)
        {
            buyTextButton.text = price + " coins";
        }
        else if (PlayerPrefs.GetInt(GetComponent<Image>().name + "PlayerMissilebuy") == 1)
        {
            buyTextButton.text = "equip";
            if (PlayerPrefs.GetInt(GetComponent<Image>().name + "PlayerMissileequip") == 1)
            {
                buyTextButton.text = "equipped";
            }
            else if (PlayerPrefs.GetInt(GetComponent<Image>().name + "PlayerMissileequip") == 0)
            {
                buyTextButton.text = "equip";
            }
        }
    }
    public void Buy()
    {
        if (PlayerPrefs.GetInt(GetComponent<Image>().name + "PlayerMissilebuy") == 0)
        {
            if (Coins.countMoney >= price)
            {
                PlayerPrefs.SetInt("AllMoney", PlayerPrefs.GetInt("AllMoney") - price);
                PlayerPrefs.SetInt(GetComponent<Image>().name + "PlayerMissilebuy", 1);
                PlayerPrefs.SetInt("PlayerMissileskinNum", skinNum);

                foreach (Image img in skins)
                {
                    if (GetComponent<Image>().name == img.name)
                    {
                        PlayerPrefs.SetInt(GetComponent<Image>().name + "PlayerMissileequip", 1);
                    }
                    else
                    {
                        PlayerPrefs.SetInt(img.name + "equip", 0);
                    }
                }
            }
        }
        else if (PlayerPrefs.GetInt(GetComponent<Image>().name + "PlayerMissilebuy") == 1)
        {

            PlayerPrefs.SetInt(GetComponent<Image>().name + "PlayerMissileequip", 1);
            PlayerPrefs.SetInt("PlayerMissileskinNum", skinNum);

            foreach (Image img in skins)
            {
                if (GetComponent<Image>().name == img.name)
                {
                    PlayerPrefs.SetInt(GetComponent<Image>().name + "PlayerMissileequip", 1);
                }
                else
                {
                    PlayerPrefs.SetInt(img.name + "PlayerMissileequip", 0);
                }
            }
        }
    }
}
