using UnityEngine;

public class DamageOnCollision : MonoBehaviour
{
    [SerializeField] private bool instantKill;
    private float damage;

    private void Start()
    {
        damage = GameManager.instance.projectileDamage;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Health targetHealth = other.GetComponent<Health>();

        if (targetHealth != null)
        {
            if (instantKill)
            {
                targetHealth.InstaKill();
            }
            else
            {
                targetHealth.TakeDamage(damage);
            }
        }
    }
}
