using UnityEngine;

// Applies knockback force when damaged
public class KnockbackOnDamage : MonoBehaviour
{
    private float knockbackForce;

    private void Start()
    {
        knockbackForce = GameManager.instance.knockbackDamage;
    }

    public void ApplyKnockback(Vector3 hitDirection)
    {
        transform.position += hitDirection * knockbackForce;
    }
}
