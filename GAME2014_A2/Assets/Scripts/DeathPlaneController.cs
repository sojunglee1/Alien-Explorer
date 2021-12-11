using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlaneController : MonoBehaviour
{
    public Transform playerSpawnPoint;
    PlayerStats player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player.RemoveLife();
            collision.transform.position = playerSpawnPoint.position + new Vector3(1, 0, 0);
        }
        else
        {
            collision.gameObject.SetActive(false);
        }

    }
}
