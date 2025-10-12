using UnityEngine;

public class PlayerHealth : Health
{
    // Tracks remaining lives for the player
    private int startingLives;
    private int currentLives;

    protected override void Start()
    {
        // Pull health and lives values from GameManager for designer control
        maxHealth = GameManager.instance.playerMaxHealth;
        currentHealth = maxHealth;
        startingLives = GameManager.instance.startingLives;
        currentLives = startingLives;
    }

    // Returns current number of lives (used by UI and win/lose logic)
    public int GetCurrentLives()
    {
        return currentLives;
    }

    protected override void Die()
    {
        currentLives--;
        GameManager.instance.gameplayUI.UpdateLives(currentLives); // Update UI display

        // If player still has lives, reset health and reposition
        if (currentLives > 0)
        {
            HealToFull(); // Restore health
            DeathRecenter recenter = GetComponent<DeathRecenter>();
            if (recenter != null)
            {
                AudioSource.PlayClipAtPoint(GameManager.instance.deathSound, transform.position, 1f);
                recenter.Die(); // Reset position instead of destroying
            }

            currentHealth = maxHealth; // Ensure health is restored
        }
        else
        {
            // Final death—trigger visual effect and destroy
            DeathSpin spin = GetComponent<DeathSpin>();
            if (spin != null)
            {
                AudioSource.PlayClipAtPoint(GameManager.instance.playerFinalDeathSound, transform.position, 1f);
                spin.Die();
            }
        }
    }
}
