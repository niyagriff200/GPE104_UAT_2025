using UnityEngine;

// Handles player input and passes movement commands to the pawn
public class PlayerController : Controller
{
    public Pawn pawn; // Reference to the controlled pawn

    private void Update()
    {
        // Only process input if a pawn is assigned
        if (pawn != null)
        {
            // Forward movement (W key)
            if (Input.GetKey(KeyCode.W))
            {
                // Use turbo if shift is held
                if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                {
                    pawn.MoveTurbo(pawn.transform.up);
                }
                else
                {
                    pawn.Move(pawn.transform.up);
                }
            }

            // Backward movement (S key)
            if (Input.GetKey(KeyCode.S))
            {
                // Use turbo if shift is held
                if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                {
                    pawn.MoveTurbo(-pawn.transform.up);
                }
                else
                {
                    pawn.Move(-pawn.transform.up);
                }
            }

            // Rotate right (D key)
            if (Input.GetKey(KeyCode.D))
            {
                pawn.Rotate(-1.0f);
            }

            // Rotate left (A key)
            if (Input.GetKey(KeyCode.A))
            {
                pawn.Rotate(1.0f);
            }

            // Directional teleport using arrow keys
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                pawn.Teleport(Vector3.up);
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                pawn.Teleport(Vector3.down);
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                pawn.Teleport(Vector3.left);
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                pawn.Teleport(Vector3.right);
            }

            // Random teleport (T key)
            if (Input.GetKeyDown(KeyCode.T))
            {
                pawn.TeleportRandom();
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                pawn.shooter.Shoot();
            }
        }
    }
}
