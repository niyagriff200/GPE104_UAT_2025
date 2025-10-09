using UnityEngine;

// Moves the object toward the player's position each frame
public class SeekerMover : MonoBehaviour
{
    private float moveSpeed; // Movement speed pulled from GameManager

    private void Start()
    {
        // Initialize speed from GameManager settings
        moveSpeed = GameManager.instance.ufoMoveSpeed;
    }

    private void Update()
    {
        // Continuously seek the player's position
        FindPlayerPosition();
    }

    public void FindPlayerPosition()
    {
        // Calculate vector from this object to the player's pawn
        Vector3 moveVector = GameManager.instance.players[0].pawn.transform.position - transform.position;

        // Normalize direction and apply movement based on speed and deltaTime
        moveVector = moveVector.normalized * moveSpeed * Time.deltaTime;

        // Move toward the player
        transform.position += moveVector;
    }
}
