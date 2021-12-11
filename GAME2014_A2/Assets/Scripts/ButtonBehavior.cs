using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*Source File Name: ButtonBehavior
 * Author's Name: Sojung (Serena) Lee
 * Student #: 101245044
 * Date Last Modified: November 18, 2021
 * Program Description: Sets up the button behaviors to change the current scene
 * Revision History:
 * (November 18) Added different button functions for main menu and game over
 * (November 19) Added load scene function for user to switch scenes more easily
 */

public class ButtonBehavior : MonoBehaviour
{
    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level 1");
    }
    
    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }
    
    public void Instructions()
    {
        SceneManager.LoadScene("Instructions");
    }
    
    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

}
