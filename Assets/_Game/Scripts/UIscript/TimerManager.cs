using FishNet.Managing.Timing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    public static TimerManager instance;

    // The timer duration selected in the settings.
    public float timerDuration = 60.0f;
    public Slider timerSlider;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update timer duration when the slider value changes
    public void UpdateTimerDuration()
    {
        float newTimerValue = timerSlider.value;
        PlayerPrefs.SetFloat("TimerDuration", newTimerValue);
        PlayerPrefs.Save();
    }
}
