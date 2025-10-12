using UnityEngine;

public class PlayerHealth : Health
{
    // Tracks remaining lives—used
    private int startingLives;
    private int currentLives;



    protected override void Start()
    {
        // Pull max health from GameManager for designer control
        maxHealth = GameManager.instance.playerMaxHealth;
        currentHealth = maxHealth;
        startingLives = GameManager.instance.startingLives;
        currentLives = startingLives;

    }


    public int GetCurrentLives()
    {
        return currentLives;
    }


    protected override void Die()
    {
        currentLives--;
        GameManager.instance.gameplayUI.UpdateLives(currentLives);

        // If player still has lives, reset health and recenter
        if (currentLives > 0)
        {
            
            HealToFull();
            DeathRecenter recenter = GetComponent<DeathRecenter>();
            if (recenter != null)
            {
                AudioSource.PlayClipAtPoint(GameManager.instance.deathSound, transform.position, 1f);
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
                AudioSource.PlayClipAtPoint(GameManager.instance.playerFinalDeathSound, transform.position, 1f);
                spin.Die();
            }
        }
    }
}
