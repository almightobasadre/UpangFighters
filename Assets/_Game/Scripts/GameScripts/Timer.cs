using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float totalTime = 60f; // Total time in seconds
    private float currentTime;
    public Text timerText;
    public Hurtbox player1Health; // Reference to the first fighter's HealthManager
    public Hurtbox player2Health; // Reference to the second fighter's HealthManager

    private bool timerRunning = true;

    public Text winResult;
    public GameObject winner;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = totalTime;
        winner.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (timerRunning)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 1)
            {
                currentTime = 0; // Set the timer to 0 when it reaches or goes below 0
                timerRunning = false;
                DetermineWinner();
            }
            UpdateTimerUI();
        }
    }

    void UpdateTimerUI()
    {
        int seconds = Mathf.FloorToInt(currentTime % 60);

        // Use a string format to display double digits for single-digit numbers
        timerText.text = seconds.ToString("00");
    }

    void DetermineWinner()
    {
        if (player1Health.currentHealth > player2Health.currentHealth || player2Health.currentHealth == 0)
        {
            // Handle win conditions for Player 1
            winResult.text = "Player 1 Wins!";
            winner.SetActive(true);

            // Pause Game;
            Time.timeScale = 0;
        }
        else if (player2Health.currentHealth > player1Health.currentHealth || player1Health.currentHealth == 0)
        {
            // Handle win conditions for Player 2
            winResult.text = "Computer Wins!";
            winner.SetActive(true);

            // Pause Game;
            Time.timeScale = 0;
        }
        else
        {
            // Handle draw conditions
            winner.SetActive(true);

            // Pause Game;
            Time.timeScale = 0;
        }
    }
}