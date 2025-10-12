using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{
    // Local spawn point for the projectile—set in prefab
    [SerializeField] private Transform projectileSpawnPoint;

    // Fires a projectile from the spawn point using prefab and sound from GameManager
    public void Shoot()
    {
        GameObject projectilePrefab = GameManager.instance.projectile;

        // Only fire if prefab and spawn point are valid
        if (projectilePrefab != null && projectileSpawnPoint != null)
        {
            // Play shooting sound effect
            GetComponent<AudioSource>().PlayOneShot(GameManager.instance.shootSound);

            // Spawn projectile at designated position and rotation
            Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
        }
    }
}
