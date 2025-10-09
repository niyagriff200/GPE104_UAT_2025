using UnityEngine;

// Handles movement and teleportation logic for the player's pawn
public class PlayerPawn : Pawn
{
    // Movement settings pulled from GameManager
    private float moveSpeed;
    private float rotateSpeed;
    private float turboSpeed;
    private float teleportDistance;
    private float randomTeleportDistance;

    // Get movement values from GameManager
    protected override void Start()
    {
        base.Start(); // Cache health and shooter from Pawn

        moveSpeed = GameManager.instance.playerMoveSpeed;
        rotateSpeed = GameManager.instance.playerRotateSpeed;
        turboSpeed = GameManager.instance.playerTurboSpeed;
        teleportDistance = GameManager.instance.playerTeleportDistance;
        randomTeleportDistance = GameManager.instance.playerRandomTeleportDistance;
    }

    public override void Move(Vector3 moveVector)
    {
        transform.position += moveVector * moveSpeed * Time.deltaTime;
    }

    public override void MoveTurbo(Vector3 moveVector)
    {
        transform.position += moveVector * turboSpeed * Time.deltaTime;
    }

    public override void Rotate(float angle)
    {
        transform.Rotate(new Vector3(0, 0, angle * rotateSpeed) * Time.deltaTime);
    }

    public override void Teleport(Vector3 direction)
    {
        transform.position += direction.normalized * teleportDistance;
    }

    public override void TeleportRandom()
    {
        float x = Random.Range(-randomTeleportDistance, randomTeleportDistance);
        float y = Random.Range(-randomTeleportDistance, randomTeleportDistance);
        transform.position = new Vector3(x, y, 0);
    }
}
