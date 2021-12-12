using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*Source File Name: ItemsBehavior
 * Author's Name: Sojung (Serena) Lee
 * Student #: 101245044
 * Date Last Modified: December 12, 2021
 * Program Description: Sets up the behavior for each item
 * Revision History:
 * (December 11) Added points variable, set up collect function, and destroyed if player collides with it
 * (December 12) Differentiated between coin and star items for different sounds & set up randomized locations in level 3 (last level)
 * (December 12) Removed unnecessary "SetStartingPoints" function
 */

public class ItemsBehavior : MonoBehaviour
{
    [SerializeField] private int points; // serialized points so each item is worth different number of points


    private void Start()
    {
        RandomizeLocation(); // randomize each item's location in last level
    }

    public void Collect()
    {
        PlayerStats.AddPoints(points); //whenever item is 'collected', add item's worth to player's total points
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if item overlaps with player (using trigger collision), collect points, play specific sound (depending on item type), and destroy item
        if (collision.gameObject.tag == "Player")
        {
            Collect();

            if (this.gameObject.tag == "Coin") AudioManager.audioManager.CoinSound();
            if (this.gameObject.tag == "Star") AudioManager.audioManager.StarSound();

            Destroy(this.gameObject);
        }
    }

    //randomizes location based on level 3 map
    public void RandomizeLocation()
    {
        int PosX = Random.Range(50, 100);   // randomizes X axis
        int PosY = Random.Range(-5, 15);    //randomizes Y axis
        if (SceneManager.GetActiveScene().name == "Level 3")
        {
            this.transform.position = new Vector2(PosX, PosY);  //insert new random axis into item's position
        }
    }
}
