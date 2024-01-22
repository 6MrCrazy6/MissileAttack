using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float destroyTime = 0.5f;

    public AudioClip explosionSound;
    public AudioSource _audio;
    void Start()
    {
        _audio.volume = PlayerPrefs.GetFloat("SFXVolume");
        _audio.PlayOneShot(explosionSound);
        Destroy(gameObject, destroyTime);
        Debug.Log(_audio.volume);
    }


}
