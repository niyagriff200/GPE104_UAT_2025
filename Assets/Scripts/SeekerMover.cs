using UnityEngine;

// Moves the object toward the player's position each frame
public class SeekerMover : MonoBehaviour
{
    private float moveSpeed; // Movement speed pulled from GameManager

    private void Start()
    {
        // Cache speed from GameManager for designer control
        moveSpeed = GameManager.instance.ufoMoveSpeed;
    }

    private void Update()
    {
        // Continuously seek the player's position
        FindPlayerPosition();
    }

    public void FindPlayerPosition()
    {
        // Get direction vector toward the first player's pawn
        Vector3 moveVector = GameManager.instance.players[0].pawn.transform.position - transform.position;

        // Normalize and scale movement by speed and deltaTime
        moveVector = moveVector.normalized * moveSpeed * Time.deltaTime;

        // Apply movement toward the player
        transform.position += moveVector;
    }
}
