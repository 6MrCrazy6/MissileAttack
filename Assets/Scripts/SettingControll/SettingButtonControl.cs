using UnityEngine;

public class SettingButtonControl : MonoBehaviour
{
    public void ResetData()
    {
        PlayerPrefs.DeleteAll();
    }
}
