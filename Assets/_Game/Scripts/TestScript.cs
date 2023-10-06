using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    public Hurtbox player1Health; // Reference to the player 1 HealthManager script
    public Hurtbox player2Health; // Reference to the player 1 HealthManager script

    void Update()
    {
        // Check for a button press, for example, the "D" key
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // Decrease the player's health by a certain amount (e.g., 10)
            player1Health.TakeDamage(10);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            // Decrease the player's health by a certain amount (e.g., 10)
            player2Health.TakeDamage(10);
        }
    }
}
