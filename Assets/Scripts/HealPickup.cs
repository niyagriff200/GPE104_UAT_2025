using UnityEngine;

// Heals player on pickup and plays sound
public class HealPickup : MonoBehaviour
{
    private float healAmount;

    private void Start()
    {
        healAmount = GameManager.instance.healAmount;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerPawn pawn = other.GetComponent<PlayerPawn>();
        if (pawn != null && pawn.health != null)
        {
            AudioSource.PlayClipAtPoint(GameManager.instance.healthPickupSound, transform.position, 1f);
            pawn.health.Heal(healAmount);
            Destroy(gameObject);
        }
    }
}
