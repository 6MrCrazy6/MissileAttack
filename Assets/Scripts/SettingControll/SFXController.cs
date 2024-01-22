using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SFXController : MonoBehaviour
{
    public static SFXController instance;

    public float sfxVolume;

    public Slider volumeSlider;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey("SFXVolume"))
        {
            PlayerPrefs.SetFloat("SFXVolume", 1f);
        }

        sfxVolume = PlayerPrefs.GetFloat("SFXVolume");
        volumeSlider.value = sfxVolume;
    }

    void Start()
    {
        
    }

    public void OnVolumeChanged()
    {
        sfxVolume = volumeSlider.value;
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
        PlayerPrefs.Save();
    }
}

