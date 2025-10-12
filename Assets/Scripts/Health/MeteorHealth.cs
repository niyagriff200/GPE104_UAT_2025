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
        // Award score based on size
        switch (size)
        {
            case 3:
                GameManager.instance.AddScore(GameManager.instance.bigMeteorScore);
                break;
            case 2:
                GameManager.instance.AddScore(GameManager.instance.mediumMeteorScore);
                break;
            case 1:
                GameManager.instance.AddScore(GameManager.instance.smallMeteorScore);
                break;
        }

        // Split into smaller meteors if size > 1
        if (size > 1)
        {
            GameManager.instance.SplitMeteors(transform.position, size);
        }

        // Remove from activeEnemies and destroy—meteors use DeathTarget
        DeathTarget target = GetComponent<DeathTarget>();
        if (target != null)
        {
            AudioSource.PlayClipAtPoint(GameManager.instance.deathSound, transform.position, 1f);
            target.Die(); // Handles removal and destruction
        }
    }

    // Called externally to define meteor size before spawning
    public void SetSize(int newSize)
    {
        size = newSize;
    }
}
