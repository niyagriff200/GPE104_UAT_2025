using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MeteorOnCollisionResponse : OnCollisionResponse
{
    public enum MeteorSize { Big, Medium, Small }
    [SerializeField] private MeteorSize size;

    protected override void Start()
    {
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
        base.HandleDamage(other);
    }

    protected override void HandleEffects(GameObject other)
    {
        CameraShake shake = Camera.main.GetComponent<CameraShake>();
        if (shake != null)
        {
            shake.Shake();
        }

        KnockbackOnDamage knockback = other.GetComponent<KnockbackOnDamage>();
        if (knockback != null)
        {
            Vector3 hitDirection = (other.transform.position - transform.position).normalized;
            knockback.ApplyKnockback(hitDirection);
        }

        if (GameManager.instance.damageRockSound != null)
        {
            AudioSource.PlayClipAtPoint(GameManager.instance.damageRockSound, transform.position, 1f);

        }
    }
}
