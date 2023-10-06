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
<<<<<<< HEAD
=======

    public Text winner;
>>>>>>> main

    private bool timerRunning = true;

    public Text winResult;
    public GameObject winner;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = totalTime;
<<<<<<< HEAD
        winner.SetActive(false);
=======
<<<<<<< HEAD
        winner.enabled = false;
=======
        winner.SetActive(false);
>>>>>>> 34c3cbc19273977544a5e31845cc306936e49a9d
>>>>>>> main
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
<<<<<<< HEAD
=======
<<<<<<< HEAD
            winner.text = "Player 1 Wins!";
            winner.enabled = true;
=======
>>>>>>> main
            winResult.text = "Player 1 Wins!";
            winner.SetActive(true);

            // Pause Game;
            Time.timeScale = 0;
<<<<<<< HEAD
=======
>>>>>>> 34c3cbc19273977544a5e31845cc306936e49a9d
>>>>>>> main
        }
        else if (player2Health.currentHealth > player1Health.currentHealth || player1Health.currentHealth == 0)
        {
            // Handle win conditions for Player 2
<<<<<<< HEAD
=======
<<<<<<< HEAD
            winner.text = "Computer 0 Wins!";
            winner.enabled = true;
=======
>>>>>>> main
            winResult.text = "Computer Wins!";
            winner.SetActive(true);

            // Pause Game;
            Time.timeScale = 0;
<<<<<<< HEAD
=======
>>>>>>> 34c3cbc19273977544a5e31845cc306936e49a9d
>>>>>>> main
        }
        else
        {
            // Handle draw conditions
<<<<<<< HEAD
=======
<<<<<<< HEAD
            winner.enabled = true;
=======
>>>>>>> main
            winner.SetActive(true);

            // Pause Game;
            Time.timeScale = 0;
<<<<<<< HEAD
=======
>>>>>>> 34c3cbc19273977544a5e31845cc306936e49a9d
>>>>>>> main
        }
    }
}