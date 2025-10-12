using UnityEngine;

public class ProjectileOnCollisionResponse : OnCollisionResponse
{
    protected override void Start()
    {
        // Pull damage value from GameManager for designer control
        damageAmount = GameManager.instance.projectileDamage;
    }

    protected override void HandleDamage(GameObject other)
    {
        base.HandleDamage(other); // Use base damage logic
    }

    protected override void HandleEffects(GameObject other)
    {
        // Trigger damage flash if target supports it
        DamageFlash flash = other.GetComponent<DamageFlash>();
        if (flash != null)
        {
            flash.Flash();
        }

        // Trigger camera shake effect
        CameraShake shake = Camera.main.GetComponent<CameraShake>();
        if (shake != null)
        {
            shake.Shake();
        }
    }

    protected override void HandleCleanup()
    {
        base.HandleCleanup(); // Destroy projectile after impact
    }
}
