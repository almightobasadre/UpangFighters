using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Hurtbox;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    public Image healthBar;

    // Start is called before the first frame update
    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Ensure health doesn't go below 0 or above maxHealth

        // Update the health bar fill amount
        float fillAmount = (float)currentHealth / maxHealth;
        // Assuming you have a reference to the health bar Image component
        healthBar.fillAmount = fillAmount;

        if (currentHealth <= 0)
        {
            // Handle death or any other logic when health reaches zero.
            // You can also destroy the GameObject if needed.
            Debug.Log("Knocked Out");

            // Pause Game;
            Time.timeScale = 0;
        }
    }

    public void ApplyHitStun(float duration)
    {
        // Implement hit stun logic here, such as disabling controls or animations.
    }

    public void ApplyKnockback(Vector2 force)
    {
        // Apply knockback force to move the target.
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero; // Reset velocity to prevent accumulation.
            rb.AddForce(force, ForceMode2D.Impulse);
        }
    }
}
