using UnityEngine;

// Destroys projectile after a set lifetime to prevent clutter
public class Projectile : MonoBehaviour
{
    private void Start()
    {
        if (GameManager.instance != null)
        {
            Destroy(gameObject, GameManager.instance.projectileLifetime);
        }
        else
        {
            Destroy(gameObject, 5f); // fallback duration
        }
    }
}
