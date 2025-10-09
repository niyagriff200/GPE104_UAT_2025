using UnityEngine;

public class PlayerHealth : Health
{
    // Tracks remaining lives—used instead of destroying the player
    private int lives = 3;

    protected override void Start()
    {
        // Pull max health from GameManager for designer control
        maxHealth = GameManager.instance.playerMaxHealth;
        currentHealth = maxHealth;
    }

    protected override void Die()
    {
        lives--;

        // If player still has lives, reset health and recenter
        if (lives > 0)
        {
            DeathRecenter recenter = GetComponent<DeathRecenter>();
            if (recenter != null)
            {
                recenter.Die(); // Reset position instead of destroying
            }

            currentHealth = maxHealth; // Restore health for next life
        }
        else
        {
            // Final death—trigger spin effect and destroy
            DeathSpin spin = GetComponent<DeathSpin>();
            if (spin != null)
            {
                spin.Die();
            }
        }
    }
}
