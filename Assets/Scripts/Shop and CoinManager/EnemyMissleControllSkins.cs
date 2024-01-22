using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMissleControllSkins : MonoBehaviour
{
    public Sprite standart;
    public Sprite skin2;
    public Sprite skin3;
    public Sprite skin4;
    public Sprite skin5;
    public Sprite skin6;
    public Sprite skin7;
    public GameObject EnemyMissle;
    void Start()
    {
        if (PlayerPrefs.GetInt("EnemyskinNum") == 1)
        {
            EnemyMissle.GetComponent<SpriteRenderer>().sprite = skin2;
        }
        else if (PlayerPrefs.GetInt("EnemyskinNum") == 2)
        {
            EnemyMissle.GetComponent<SpriteRenderer>().sprite = skin3;
        }
        else if (PlayerPrefs.GetInt("EnemyskinNum") == 3)
        {
            EnemyMissle.GetComponent<SpriteRenderer>().sprite = skin4;
        }
        else if (PlayerPrefs.GetInt("EnemyskinNum") == 4)
        {
            EnemyMissle.GetComponent<SpriteRenderer>().sprite = skin5;
        }
        else if (PlayerPrefs.GetInt("EnemyskinNum") == 5)
        {
            EnemyMissle.GetComponent<SpriteRenderer>().sprite = skin6;
        }
        else if (PlayerPrefs.GetInt("EnemyskinNum") == 6)
        {
            EnemyMissle.GetComponent<SpriteRenderer>().sprite = skin7;
        }
        else
        {
            EnemyMissle.GetComponent<SpriteRenderer>().sprite = standart;
        }
    }
}
