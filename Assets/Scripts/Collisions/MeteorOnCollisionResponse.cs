using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MeteorOnCollisionResponse : OnCollisionResponse
{
    public enum MeteorSize { Big, Medium, Small }
    [SerializeField] private MeteorSize size; // Designer-assigned size for damage scaling

    protected override void Start()
    {
        // Scale damage based on meteor size using multipliers from GameManager
        float baseDamage = GameManager.instance.baseMeteorDamage;

        switch (size)
        {
            case MeteorSize.Big:
                damageAmount = baseDamage;
                break;
            case MeteorSize.Medium:
                damageAmount = baseDamage * GameManager.instance.mediumMeteorMultiplier;
                break;
            case MeteorSize.Small:
                damageAmount = baseDamage * GameManager.instance.smallMeteorMultiplier;
                break;
        }
    }

    protected override void HandleDamage(GameObject other)
    {
        base.HandleDamage(other); // Use base damage logic
    }

    protected override void HandleEffects(GameObject other)
    {
        // Trigger camera shake effect
        CameraShake shake = Camera.main.GetComponent<CameraShake>();
        if (shake != null)
        {
            shake.Shake();
        }

        // Apply knockback if target supports it
        KnockbackOnDamage knockback = other.GetComponent<KnockbackOnDamage>();
        if (knockback != null)
        {
            Vector3 hitDirection = (other.transform.position - transform.position).normalized;
            knockback.ApplyKnockback(hitDirection);
        }

        // Play rock impact sound if assigned
        if (GameManager.instance.damageRockSound != null)
        {
            AudioSource.PlayClipAtPoint(GameManager.instance.damageRockSound, transform.position, 1f);
        }
    }
}
