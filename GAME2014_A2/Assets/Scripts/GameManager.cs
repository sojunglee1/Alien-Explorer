using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform player;
    public Transform playerSpawnPoint;

    [SerializeField] List<ItemsBehavior> items;

    void Start()
    {
        player.position = playerSpawnPoint.position + new Vector3(1, 0, 0);

        foreach(ItemsBehavior item in items)
        {
            Instantiate(item);
        }
    }
}
