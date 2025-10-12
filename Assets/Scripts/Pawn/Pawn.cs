using UnityEngine;

// Base class for all controllable entities (player, enemies, etc.)
public abstract class Pawn : MonoBehaviour
{
    // Cached references to health and shooting components
    [HideInInspector] public Health health;
    [HideInInspector] public ProjectileShooter shooter;

    // Movement and ability methods to be defined by subclasses
    public abstract void Move(Vector3 moveVector);
    public abstract void Rotate(float angle);
    public abstract void Teleport(Vector3 direction);
    public abstract void MoveTurbo(Vector3 moveVector);
    public abstract void TeleportRandom();

    protected virtual void Start()
    {
        // Cache core components once at runtime
        health = GetComponent<Health>();
        shooter = GetComponent<ProjectileShooter>();
    }
}
