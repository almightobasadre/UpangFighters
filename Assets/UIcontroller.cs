using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIcontroller : MonoBehaviour
{
    public Slider _musicSlider, _sfxSlider;

    public void ToggleMusic()
    {
        AudioManager.Instance.ToggleMusic();
    }

    public void MusicVolume()
    {
        AudioManager.Instance.MusicVolume(_musicSlider.value);
    }
}
