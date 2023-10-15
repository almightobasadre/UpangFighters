using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static CharController;

public class Hurtbox : MonoBehaviour
{
    public int maxHealth = 100; // Adjust the maximum health as needed
    public int currentHealth;

    public Image healthBar;

    private Animator animator;

    public enum CharacterType
    {
        Player,
        Computer
    }

    public CharacterType characterType;

    // Start is called before the first frame update
    private void Start()
    {
        currentHealth = maxHealth;
        // Get the Animator component from the parent GameObject
        animator = transform.parent.GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Ensure health doesn't go below 0 or above maxHealth

        // Update the health bar fill amount
        float fillAmount = (float)currentHealth / maxHealth;
        // Assuming you have a reference to the health bar Image component
        healthBar.fillAmount = fillAmount;

        animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            // Pause Game;
            Time.timeScale = 0;
            Debug.Log(characterType + " Knocked Out");
        }
    }

}
