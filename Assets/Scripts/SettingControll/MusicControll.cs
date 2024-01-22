using UnityEngine;
using UnityEngine.UI;

public class MusicControll : MonoBehaviour
{
    public AudioSource music;
    public Slider slider;
    private const string VolumePrefsKey = "Volume";

    private void Awake()
    {
        if (!PlayerPrefs.HasKey(VolumePrefsKey))
        {
            PlayerPrefs.SetFloat(VolumePrefsKey, 1f);
        }
        else
        {
            music.volume = PlayerPrefs.GetFloat(VolumePrefsKey);
        }
    }

    void Update()
    {
        music.volume = PlayerPrefs.GetFloat(VolumePrefsKey);
        PlayerPrefs.SetFloat(VolumePrefsKey, music.volume);
    }

    public void OnSliderValueChanged()
    {
        music.volume = slider.value;
        PlayerPrefs.SetFloat(VolumePrefsKey, music.volume);
        PlayerPrefs.Save();
    }
}
