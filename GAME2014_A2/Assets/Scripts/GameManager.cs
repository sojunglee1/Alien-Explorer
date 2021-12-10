using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform player;
    public Transform playerSpawnPoint;

    void Start()
    {
        player.position = playerSpawnPoint.position + new Vector3(1, 0, 0);
    }
}
