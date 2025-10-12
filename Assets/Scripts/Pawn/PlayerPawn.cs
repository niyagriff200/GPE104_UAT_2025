using UnityEngine;

// Handles movement and teleportation logic for the player's pawn
public class PlayerPawn : Pawn
{
    // Movement settings pulled from GameManager for designer control
    private float moveSpeed;
    private float rotateSpeed;
    private float turboSpeed;
    private float teleportDistance;
    private float randomTeleportDistance;

    // Cache movement values from GameManager at runtime
    protected override void Start()
    {
        base.Start(); // Inherited setup: caches health and shooter

        moveSpeed = GameManager.instance.playerMoveSpeed;
        rotateSpeed = GameManager.instance.playerRotateSpeed;
        turboSpeed = GameManager.instance.playerTurboSpeed;
        teleportDistance = GameManager.instance.playerTeleportDistance;
        randomTeleportDistance = GameManager.instance.playerRandomTeleportDistance;
    }

    // Moves the player based on input vector and moveSpeed
    public override void Move(Vector3 moveVector)
    {
        transform.position += moveVector * moveSpeed * Time.deltaTime;
    }

    // Moves the player using turboSpeed (Shift key)
    public override void MoveTurbo(Vector3 moveVector)
    {
        transform.position += moveVector * turboSpeed * Time.deltaTime;
    }

    // Rotates the player around Z-axis using rotateSpeed
    public override void Rotate(float angle)
    {
        transform.Rotate(new Vector3(0, 0, angle * rotateSpeed) * Time.deltaTime);
    }

    // Teleports the player in a given direction by teleportDistance
    public override void Teleport(Vector3 direction)
    {
        transform.position += direction.normalized * teleportDistance;
    }

    // Teleports the player to a random location within a set range
    public override void TeleportRandom()
    {
        float x = Random.Range(-randomTeleportDistance, randomTeleportDistance);
        float y = Random.Range(-randomTeleportDistance, randomTeleportDistance);
        transform.position = new Vector3(x, y, 0);
    }
}
