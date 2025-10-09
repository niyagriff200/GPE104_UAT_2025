using UnityEngine;

public abstract class Pawn : MonoBehaviour
{
    // Core components used by all pawns
    [HideInInspector] public Health health;
    [HideInInspector] public ProjectileShooter shooter;

    // Movement and ability methods to be implemented by subclasses
    public abstract void Move(Vector3 moveVector);
    public abstract void Rotate(float angle);
    public abstract void Teleport(Vector3 direction);
    public abstract void MoveTurbo(Vector3 moveVector);
    public abstract void TeleportRandom();

    protected virtual void Start()
    {
        // Get needed components once at start
        health = GetComponent<Health>();
        shooter = GetComponent<ProjectileShooter>();
    }
}
