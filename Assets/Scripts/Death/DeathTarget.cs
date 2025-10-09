using UnityEngine;

public class DeathTarget : DeathDestroy
{
    private bool hasDied = false; // Prevents multiple death triggers from overlapping damage or collisions

    public override void Die()
    {
        if (!hasDied)
        {
            hasDied = true;

            // Remove from activeEnemies list—important for spawn pacing and enemy count checks
            GameManager.instance.RemoveEnemy(gameObject);

            // Call base death logic (Destroy) to clean up the object
            base.Die();
        }
    }
}
