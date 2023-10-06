using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsScene : MonoBehaviour
{
    public void Settings()
    {
        SceneManager.LoadScene("Settings");
    }
    public void SinglePlayer()
     {
        SceneManager.LoadScene("SinglePlayer");
    }
    public void Arcade()
    {
        SceneManager.LoadScene("ArcadeScene");
    }
    public void Practice()
    {
        SceneManager.LoadScene("PracticeScene");
    }
    public void BackInPractice()
    {
        SceneManager.LoadScene("MainMenu");
    }
     public void BackinMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
