using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ProjectileOnCollisionResponse : OnCollisionResponse
{
    protected override void Start()
    {
        damageAmount = GameManager.instance.projectileDamage;
    }

    protected override void HandleDamage(GameObject other)
    {
        base.HandleDamage(other);
    }

    protected override void HandleEffects(GameObject other)
    {
        DamageFlash flash = other.GetComponent<DamageFlash>();
        if (flash != null)
        {
            flash.Flash();
        }

        CameraShake shake = Camera.main.GetComponent<CameraShake>();
        if (shake != null)
        {
            shake.Shake();
        }
    }

    protected override void HandleCleanup()
    {
        base.HandleCleanup();
    }
}
