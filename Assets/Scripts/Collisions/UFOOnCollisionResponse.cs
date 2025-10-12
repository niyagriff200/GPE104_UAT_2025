using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class UFOOnCollisionResponse : OnCollisionResponse
{
    protected override void Start()
    {
        // Pull damage value from GameManager for designer control
        damageAmount = GameManager.instance.ufoDamage;
    }

    protected override void HandleDamage(GameObject other)
    {
        base.HandleDamage(other); // Use base damage logic (instantKill or standard)
    }

    protected override void HandleEffects(GameObject other)
    {
        // Play metal impact sound if assigned
        if (GameManager.instance.damageMetalSound != null)
        {
            AudioSource.PlayClipAtPoint(GameManager.instance.damageMetalSound, transform.position, 1f);
        }

        // Trigger camera shake effect
        CameraShake shake = Camera.main.GetComponent<CameraShake>();
        if (shake != null)
        {
            shake.Shake();
        }

        // Apply knockback to damaged object if it supports it
        KnockbackOnDamage knockback = other.GetComponent<KnockbackOnDamage>();
        if (knockback != null)
        {
            Vector3 hitDirection = (other.transform.position - transform.position).normalized;
            knockback.ApplyKnockback(hitDirection);
        }
    }
}
