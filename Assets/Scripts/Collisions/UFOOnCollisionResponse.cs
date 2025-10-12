using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class UFOOnCollisionResponse : OnCollisionResponse
{
    protected override void Start()
    {
        damageAmount = GameManager.instance.ufoDamage;
    }

    protected override void HandleDamage(GameObject other)
    {
        base.HandleDamage(other);
    }

    protected override void HandleEffects(GameObject other)
    {
        if (GameManager.instance.damageMetalSound != null)
        {
            AudioSource.PlayClipAtPoint(GameManager.instance.damageMetalSound, transform.position, 1f);

        }

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
    }
}
