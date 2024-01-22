using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllSkin : MonoBehaviour
{
    public Sprite standart;
    public Sprite skin2;
    public Sprite skin3;
    public Sprite skin4;
    public Sprite skin5;
    public Sprite skin6;
    public Sprite skin7;
    public Sprite skin8;
    public Sprite skin9;
    public Sprite skin10;
    public Sprite skin11;
    public Sprite skin12;
    public GameObject Ball;
    void Start()
    {
        if (PlayerPrefs.GetInt("skinNum") == 1)
        {
            Ball.GetComponent<SpriteRenderer>().sprite = skin2;
        }
        else if (PlayerPrefs.GetInt("skinNum") == 2)
        {
            Ball.GetComponent<SpriteRenderer>().sprite = skin3;
        }
        else if (PlayerPrefs.GetInt("skinNum") == 3)
        {
            Ball.GetComponent<SpriteRenderer>().sprite = skin4;
        }
        else if (PlayerPrefs.GetInt("skinNum") == 4)
        {
            Ball.GetComponent<SpriteRenderer>().sprite = skin5;
        }
        else if (PlayerPrefs.GetInt("skinNum") == 5)
        {
            Ball.GetComponent<SpriteRenderer>().sprite = skin6;
        }
        else if (PlayerPrefs.GetInt("skinNum") == 6)
        {
            Ball.GetComponent<SpriteRenderer>().sprite = skin7;
        }
        else if (PlayerPrefs.GetInt("skinNum") == 7)
        {
            Ball.GetComponent<SpriteRenderer>().sprite = skin8;
        }
        else if (PlayerPrefs.GetInt("skinNum") == 8)
        {
            Ball.GetComponent<SpriteRenderer>().sprite = skin9;
        }
        else if (PlayerPrefs.GetInt("skinNum") == 9)
        {
            Ball.GetComponent<SpriteRenderer>().sprite = skin10;
        }
        else if (PlayerPrefs.GetInt("skinNum") == 10)
        {
            Ball.GetComponent<SpriteRenderer>().sprite = skin11;
        }
        else if (PlayerPrefs.GetInt("skinNum") == 11)
        {
            Ball.GetComponent<SpriteRenderer>().sprite = skin12;
        }
        else
        {
            Ball.GetComponent<SpriteRenderer>().sprite = standart;
        }
    }
}