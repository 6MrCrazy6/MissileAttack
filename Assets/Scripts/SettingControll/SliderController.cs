using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public Slider slider;
    public float volume;

    void Start()
    {
        volume = slider.value;
        if (!PlayerPrefs.HasKey("Volume")) slider.value = 1f;
        else slider.value = PlayerPrefs.GetFloat("Volume");
    }


    void Update()
    {
        if (volume != slider.value)
        {
            PlayerPrefs.SetFloat("Volume", slider.value);
            PlayerPrefs.Save();
            volume = slider.value;
        }
    }

}
