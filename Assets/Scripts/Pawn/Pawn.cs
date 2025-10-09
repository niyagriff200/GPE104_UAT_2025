using UnityEngine;

public abstract class Pawn : MonoBehaviour
{
    public abstract void Move(Vector3 moveVector);

    public abstract void MoveTurbo(Vector3 moveVector);

    public abstract void Rotate(float angle);

    public abstract void Teleport(Vector3 direction);

    public abstract void TeleportRandom();
    
    
}
