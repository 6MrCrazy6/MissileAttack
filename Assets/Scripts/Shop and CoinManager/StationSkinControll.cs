using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationSkinControll : MonoBehaviour
{
    public Sprite standart;
    public Sprite skin2;
    public Sprite skin3;
    public Sprite skin4;
    public Sprite skin5;
    public Sprite skin6;
    public Sprite skin7;
    public GameObject Station;
    void Start()
    {
        if (PlayerPrefs.GetInt("StationskinNum") == 1)
        {
            Station.GetComponent<SpriteRenderer>().sprite = skin2;
        }
        else if (PlayerPrefs.GetInt("StationskinNum") == 2)
        {
            Station.GetComponent<SpriteRenderer>().sprite = skin3;
        }
        else if (PlayerPrefs.GetInt("StationskinNum") == 3)
        {
            Station.GetComponent<SpriteRenderer>().sprite = skin4;
        }
        else if (PlayerPrefs.GetInt("StationskinNum") == 4)
        {
            Station.GetComponent<SpriteRenderer>().sprite = skin5;
        }
        else if (PlayerPrefs.GetInt("StationskinNum") == 5)
        {
            Station.GetComponent<SpriteRenderer>().sprite = skin6;
        }
        else if (PlayerPrefs.GetInt("StationskinNum") == 6)
        {
            Station.GetComponent<SpriteRenderer>().sprite = skin7;
        }
        else
        {
            Station.GetComponent<SpriteRenderer>().sprite = standart;
        }
    }
}
