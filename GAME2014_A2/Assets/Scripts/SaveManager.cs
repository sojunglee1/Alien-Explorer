using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*Source File Name: SaveManager
 * Author's Name: Sojung (Serena) Lee
 * Student #: 101245044
 * Date Last Modified: December 12, 2021
 * Program Description: Get score and print it UI
 * Revision History:
 * (December 12) Prints score to UI text in Game Over and Game Won screen
 * (December 12) Removed unnecessary variable (e.g. score)
 */

public class SaveManager : MonoBehaviour
{
    //gets text ui component
    [SerializeField] Text scoreText;
    void Start()
    {
        //if the player already has save file, then print to text ui
        if (PlayerPrefs.HasKey("Score"))
        {
            scoreText.text = "Score : " + PlayerPrefs.GetInt("Score").ToString();
        }
        //if not, then print out score not available
        else scoreText.text = "Score Not Available";
    }
}
