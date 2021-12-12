using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*Source File Name: ButtonBehavior
 * Author's Name: Sojung (Serena) Lee
 * Student #: 101245044
 * Date Last Modified: December 12, 2021
 * Program Description: Sets up the button behaviors to change the current scene
 * Revision History:
 * (November 18) Added different button functions for main menu and game over
 * (November 19) Added load scene function for user to switch scenes more easily
 * (December 12) Removed unnecessary function - OnTriggerEnter
 * (December 12) Delete any saved files to reset points anytime game restarts
 */

//Class for button On Click behavior
public class ButtonBehavior : MonoBehaviour
{
    //loads scene with whatever level name (used only for Unity editor)
    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    //whenever the game starts at level 1, delete any saved files
    public void StartGame()
    {
        PlayerPrefs.DeleteKey("Score");
        SceneManager.LoadScene("Level 1");
    }
    
    public void Credits()   //changes to credits scene
    {
        SceneManager.LoadScene("Credits");
    }
    
    public void Instructions()  //changes to instructions scene
    {
        SceneManager.LoadScene("Instructions");
    }
    
    public void MainMenu()  //changes to main menu scene
    {
        SceneManager.LoadScene("Menu");
    }
}
