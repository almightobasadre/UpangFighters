using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class settingsScene : MonoBehaviour
{
    public void settings()
    {
        SceneManager.LoadScene("Settings");
    }
    public void singlePlayer()
     {
        SceneManager.LoadScene("GamePlay");
    }
    public void Practice()
    {
        SceneManager.LoadScene("PracticeScene");
    }
    public void BackInPractice()
    {
        SceneManager.LoadScene("Scene01");
    }
     public void BackinMenu()
    {
        SceneManager.LoadScene("Scene01");
    }
}
