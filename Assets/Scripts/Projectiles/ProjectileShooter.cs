using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{
    // Local spawn point for the projectile—set in prefab
    [SerializeField] private Transform projectileSpawnPoint;

    // Called externally to fire a projectile
    public void Shoot()
    {
        GameObject projectilePrefab = GameManager.instance.projectile;

        if (projectilePrefab != null && projectileSpawnPoint != null)
        {
            Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
        }
    }
}
