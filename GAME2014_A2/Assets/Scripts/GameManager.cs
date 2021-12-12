using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Source File Name: GameManager
 * Author's Name: Sojung (Serena) Lee
 * Student #: 101245044
 * Date Last Modified: December 12, 2021
 * Program Description: set up game manager for player spawn point for new level & instantiates items
 * Revision History:
 * (December 11) Added player spawn point - when player enters new level, player respawns back to original point (green flag game object) from personal GAME2014_Lab8 project
 * (December 12) Added random items - for each item in level 3, randomize its location
 */

public class GameManager : MonoBehaviour
{
    //sets up player's starting point
    public Transform player;
    public Transform playerSpawnPoint;

    //sets up list of items
    [SerializeField] List<ItemsBehavior> items;

    void Start()
    {
        //player's position will be next to the player's spawn point (green flag game object)
        player.position = playerSpawnPoint.position + new Vector3(1, 0, 0);

        //create a new item with random locations (only for level 3)
        foreach(ItemsBehavior item in items)
        {
            Instantiate(item);
        }
    }
}
