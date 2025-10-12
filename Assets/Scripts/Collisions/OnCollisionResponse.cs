using UnityEngine;

public abstract class OnCollisionResponse : MonoBehaviour
{
    // Core values used by all collision responders
    protected float damageAmount = 25f;
    protected bool instantKill = false;

    protected virtual void Start()
    {

    }

    // Entry point for collision logic
    protected virtual void OnCollisionEnter2D(Collision2D other)
    {

        HandleDamage(other.gameObject);
        HandleEffects(other.gameObject);
        HandleCleanup();
    }

    // Apply damage logic—can be overridden
    protected virtual void HandleDamage(GameObject other)
    {

        Health health = other.GetComponentInChildren<Health>();
        if (health != null)
        {
            if (instantKill)
            {
                health.InstaKill();
            }
            else
            {
                health.TakeDamage(damageAmount);
            }
        }
    }

    // Trigger visual/audio effects
    protected virtual void HandleEffects(GameObject other)
    {
        // TODO: camera shake, damage flash, knockback, etc.
    }

    
    protected virtual void HandleCleanup()
    {
        // Destroy self if it's a projectile
        if (GetComponent<Projectile>() != null)
        {
            Destroy(gameObject);
        }
    }
}
