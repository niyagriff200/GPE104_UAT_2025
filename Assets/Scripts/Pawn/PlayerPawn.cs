using UnityEngine;

public class PlayerPawn : Pawn
{
    private float moveSpeed;
    private float rotateSpeed;
    private float turboSpeed;
    private float teleportDistance;
    private float randomTeleportDistance;


    public void Start()
    {
        moveSpeed = GameManager.instance.moveSpeed;
        rotateSpeed = GameManager.instance.rotateSpeed;
        turboSpeed = GameManager.instance.turboSpeed;
        teleportDistance = GameManager.instance.teleportDistance;
        randomTeleportDistance = GameManager.instance.randomTeleportDistance;
    }
    public override void Move(Vector3 moveVector)
    {
        //Move based on units per second
        transform.position += moveVector * moveSpeed * Time.deltaTime;
    }

    public override void MoveTurbo(Vector3 moveVector)
    {
        //Move based on units per second
        transform.position += moveVector * turboSpeed * Time.deltaTime;
    }

    public override void Rotate(float angle)
    {
        //Rotate based on units per second
        transform.Rotate(new Vector3(0, 0, angle * rotateSpeed) * Time.deltaTime);
    }

    public override void Teleport(Vector3 direction)
    {
        //Teleport a certain distance (normalized = 1)
        transform.position += direction.normalized * teleportDistance;
    }

    public override void TeleportRandom()
    {
        //Teleport a random range in any direction (square)
        float x = Random.Range(randomTeleportDistance, -randomTeleportDistance);
        float y = Random.Range(randomTeleportDistance, -randomTeleportDistance);
        transform.position = new Vector3(x, y, 0);
    }
}
