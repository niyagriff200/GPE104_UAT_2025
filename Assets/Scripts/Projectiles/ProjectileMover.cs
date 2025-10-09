using UnityEngine;

public class ProjectileMover : MonoBehaviour
{
    // Movement speed pulled from GameManager—designer-controlled
    private float moveSpeed;

    private void Start()
    {
        // Cache speed from GameManager to avoid repeated access
        moveSpeed = GameManager.instance.projectileSpeed;
    }

    private void Update()
    {
        // Move forward based on local up direction
        transform.position += transform.up * moveSpeed * Time.deltaTime;
    }
}
