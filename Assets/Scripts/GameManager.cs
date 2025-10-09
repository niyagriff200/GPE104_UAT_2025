using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
   public static GameManager instance;

    [Header("Players")]
    public List<PlayerController> players;

    [Header("Player Movement Settings")]
    public float moveSpeed;
    public float turboSpeed;
    public float rotateSpeed;
    public float teleportDistance;
    public float randomTeleportDistance;



    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }



}
