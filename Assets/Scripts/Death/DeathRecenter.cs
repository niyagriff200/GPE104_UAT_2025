using UnityEngine;

// Recenter the player
public class DeathRecenter : Death
{
    public override void Die()
    {
        transform.position = Vector3.zero; // Reset position
    }
}
