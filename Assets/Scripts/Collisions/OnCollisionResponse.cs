using UnityEngine;

public abstract class OnCollisionResponse : MonoBehaviour
{
    // Core values used by all collision responders
    protected float damageAmount = 25f;
    protected bool instantKill = false;

    protected virtual void Start()
    {
        // Optional setup for subclasses
    }

    // Entry point for collision logic
    protected virtual void OnCollisionEnter2D(Collision2D other)
    {
        HandleDamage(other.gameObject);   // Apply damage to collided object
        HandleEffects(other.gameObject);  // Trigger visual/audio feedback
        HandleCleanup();                  // Optional self-destruction
    }

    // Apply damage logic—can be overridden
    protected virtual void HandleDamage(GameObject other)
    {
        Health health = other.GetComponentInChildren<Health>();
        if (health != null)
        {
            if (instantKill)
            {
                health.InstaKill(); // Bypass health and kill instantly
            }
            else
            {
                health.TakeDamage(damageAmount); // Apply standard damage
            }
        }
    }

    // Trigger visual/audio effects
    protected virtual void HandleEffects(GameObject other)
    {
        // TODO: camera shake, damage flash, knockback, etc.
    }

    // Optional cleanup logic—used by projectiles
    protected virtual void HandleCleanup()
    {
        if (GetComponent<Projectile>() != null)
        {
            Destroy(gameObject); // Destroy projectile after impact
        }
    }
}
