using UnityEngine;

public class PlayerController : Controller
{
    public Pawn pawn;

    private void Update()
    {
        if (pawn != null)
        {
            if (Input.GetKey(KeyCode.W))
            {
                //MoveTurbo if the player is holding left or right shift key
                if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                {
                    pawn.MoveTurbo(pawn.transform.up);
                }
                else
                {
                    //Otherwise use Move
                    pawn.Move(pawn.transform.up);
                }

            }

            if (Input.GetKey(KeyCode.S))
            {
                //MoveTurbo if the player is holding left or right shift key
                if (Input.GetKey(KeyCode.RightShift) ||  Input.GetKey(KeyCode.LeftShift))
                {
                    pawn.MoveTurbo(-pawn.transform.up);
                }
                else
                {
                    //Otherwise use Move
                    pawn.Move(-pawn.transform.up);
                }
            }

            if (Input.GetKey(KeyCode.D))
            {
                //Rotate to the right
                pawn.Rotate(-1.0f);
            }

            if (Input.GetKey(KeyCode.A))
            { 
                //Rotate to the left
                pawn.Rotate(1.0f); 
            }

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                //Teleport a certain distance up
                pawn.Teleport(Vector3.up);
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                //Teleport a certain distance down
                pawn.Teleport(Vector3.down);
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                //Teleport a certain distance to the left
                pawn.Teleport(Vector3.left);
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                //Teleport a certain distance to the right
                pawn.Teleport(Vector3.right);
            }

            if (Input.GetKeyDown(KeyCode.T))
            {
                //Randomly Teleport a certain distance in a square radius
                pawn.TeleportRandom();

            }
        }
    }
}
