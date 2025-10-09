using UnityEngine;

public class MeteorHealth : Health
{
    // Used to determine split behavior—3 = big, 2 = medium, 1 = small
    private int size = 3;

    protected override void Start()
    {
        // Pull max health from GameManager—designer-controlled
        maxHealth = GameManager.instance.meteorMaxHealth;
        currentHealth = maxHealth;
    }

    protected override void Die()
    {
        // Split into smaller meteors if size > 1
        if (size > 1)
        {
            GameManager.instance.SplitMeteors(transform.position, size);
        }

        // Remove from activeEnemies and destroy—meteors use DeathTarget
        DeathTarget target = GetComponent<DeathTarget>();
        if (target != null)
        {
            target.Die(); // Handles removal and destruction
        }
    }
}
