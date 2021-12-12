using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Source File Name: DeathPlaneController
 * Author's Name: Sojung (Serena) Lee
 * Student #: 101245044
 * Date Last Modified: December 12, 2021
 * Program Description: Sets up death plane and player's response to it
 * Revision History:
 * (December 11) Added basic script from personal GAME2014_Lab8 project
 * (December 11) Added respawn function (e.g. anytime player overlaps with death plane, respawn back to player's original position)
 * (December 11) Added delete function (e.g. if enemy or other game objects touches death plane, set other object inactive)
 * (December 12) Added "RemoveLife" function so player's loses life whenever overlaps with death plane
 */

public class DeathPlaneController : MonoBehaviour
{
    //set up player's respawn point
    public Transform playerSpawnPoint;

    //set up player's stat to have access to player's lives
    PlayerStats player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if player touches death plane then remove a life & respawn back to original point
        if (collision.gameObject.CompareTag("Player"))
        {
            player.RemoveLife();
            collision.transform.position = playerSpawnPoint.position + new Vector3(1, 0, 0);
        }
        else
        {
            //if other game objects touches death plane, then deactivate them
            collision.gameObject.SetActive(false);
        }

    }
}
