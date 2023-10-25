using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject menu;
    public bool isPause;
    void Start()
    {
        menu.SetActive(false);
    }
    void Update() 
    { 
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPause)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }
    public void ResumeGame()
    {
        menu.SetActive(false);
        Time.timeScale = 1f;
        isPause = false;
    }
    public void PauseGame()
    {
        menu.SetActive(true);
        Time.timeScale = 0f;
        isPause = true;
    }

    public void RestartGame()
    {
        menu.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }
}
