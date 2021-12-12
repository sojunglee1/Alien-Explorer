using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*Source File Name: PlayerStats
 * Author's Name: Sojung (Serena) Lee
 * Student #: 101245044
 * Date Last Modified: December 12, 2021
 * Program Description: Sets up the stats for the player (e.g. lives, scores)
 * Revision History:
 * (December 11) Added player total points, added lives panel (for 3 life icons), points panel
 * (December 11) Added simple saving points function (Player's Prefs), checks death & adding points functions
 * (December 11) Added flinching whenever player touches obstacles or enemies
 * (December 12) Removed lives panel (for 3 life icones) and added lives text and simpler integer value for number of lives
 * (December 12) Moved flinching to Player Behavior script (for better organization)
 */

//Class for player stats
public class PlayerStats : MonoBehaviour
{
    [HideInInspector] public static int PlayerPoints = 0;   //total number of accumulated points
    [SerializeField] private int Lives;                     //total number of lives
    [SerializeField] private Text livesText;                //lives text component for UI
    [SerializeField] private Text PointsPanel;              //points text component for UI

    void Start()
    {
        PlayerPoints = 0;

        //if player's save file already exists, then set value into local variable
        if (PlayerPrefs.HasKey("Score")) PlayerPoints = PlayerPrefs.GetInt("Score");
    }

    private void Update()
    {
        //updates UI components with variable
        PointsPanel.text = PlayerPoints.ToString(); 
        livesText.text = "x" + Lives.ToString();
        CheckDeath();   //checks player death every time
    }

    //updates audio and number of lives everytime player loses life
    public void RemoveLife()   
    {
        AudioManager.audioManager.DeathSound();
        Lives -= 1;
    }

    //checks if the number of lives reaches to zero (save player's points & load game over screen)
    public void CheckDeath()
    {
        if (Lives == 0)
        {
            Save();
            SceneManager.LoadScene("Game Over");
        }
    }

    //increases player's points
    public static void AddPoints(int i)
    {
        PlayerPoints += i;
    }

    //checks for when player starts colliding with other game objects
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if the player touches the red finish flag, then save player's points for next level & load next level
        if (collision.gameObject.tag == "Finish")
        {
            Save();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        // if the player touches an obstacle (gray spikes), lose a life
        if (collision.gameObject.tag == "Obstacle")
        {
            RemoveLife();
        }        
        //if the player touches an enemy, played sfx & lose a life
        if (collision.gameObject.CompareTag("Snail") || collision.gameObject.CompareTag("Slime"))
        {
            AudioManager.audioManager.AttackedSound();
            RemoveLife();
        }
    }

    //saves player's total number of points for next level & game win/over scene using Player's Prefs
    public static void Save()
    {
        PlayerPrefs.SetInt("Score", PlayerPoints);
    }
}
