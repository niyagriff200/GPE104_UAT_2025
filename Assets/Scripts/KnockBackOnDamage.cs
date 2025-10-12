using UnityEngine;

public class KnockbackOnDamage : MonoBehaviour
{
    private float knockbackForce;

    private void Start()
    {
        knockbackForce = GameManager.instance.knockbackDamage;
    }

    public void ApplyKnockback(Vector3 hitDirection)
    {
        Vector3 direction = hitDirection;
        transform.position += direction * knockbackForce;
    }
}