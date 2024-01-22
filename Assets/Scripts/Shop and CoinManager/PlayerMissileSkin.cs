using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMissileSkin : MonoBehaviour
{
    public Sprite standart;
    public Sprite skin2;
    public Sprite skin3;
    public Sprite skin4;

    public AudioSource audioSource;
    public AudioClip soundsLaser;

    public GameObject PlayerMisssile;
    void Start()
    {
        audioSource.volume = PlayerPrefs.GetFloat("SFXVolume");

        if (PlayerPrefs.GetInt("PlayerMissileskinNum") == 1)
        {
            PlayerMisssile.GetComponent<SpriteRenderer>().sprite = skin2;
            audioSource.PlayOneShot(soundsLaser);
        }
        else if (PlayerPrefs.GetInt("PlayerMissileskinNum") == 2)
        {
            PlayerMisssile.GetComponent<SpriteRenderer>().sprite = skin3;
            audioSource.PlayOneShot(soundsLaser);
        }
        else if (PlayerPrefs.GetInt("PlayerMissileskinNum") == 3)
        {
            PlayerMisssile.GetComponent<SpriteRenderer>().sprite = skin4;
            audioSource.PlayOneShot(soundsLaser);
        }
        else
        {
            PlayerMisssile.GetComponent<SpriteRenderer>().sprite = standart;
            audioSource.Stop();
        }
    }
}
