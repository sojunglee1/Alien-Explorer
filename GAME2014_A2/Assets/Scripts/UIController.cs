using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Source File Name: UIController
 * Author's Name: Sojung (Serena) Lee
 * Student #: 101245044
 * Date Last Modified: December 12, 2021
 * Program Description: sets up mobile UI controls - joysticks and on screen buttons
 * Revision History:
 * (December 12) Set up joysticks for player's movement & jump button from personal GAME2014_Lab8 project
 * (December 12) Set up shoot button
 */

public class UIController : MonoBehaviour
{
    //gets game object that contains the mobile UI controls
    [Header("On Screen Controls")]
    public GameObject onScreenControls;

    //gets the buttons for jump and shoot
    [Header("Button Control Events")]
    public static bool jumpButtonDown;
    public static bool shootButtonDown;

    // Start is called before the first frame update
    void Start()
    {
        //checks which build platform game is running
        CheckPlatform();
    }

    private void CheckPlatform()
    {
        //all platforms (e.g. apple, android, or windows) have same mobile screen UI layout/controls
        switch (Application.platform)
        {
            case RuntimePlatform.Android:           //if platform is running android, move down
            case RuntimePlatform.IPhonePlayer:      //if platform is running apple, move down
            case RuntimePlatform.WindowsEditor:     //if platform is running windows, then make sure screen controls are visible and active
                onScreenControls.SetActive(true);
                break;
            default:                                //if plaform is running on other OS, then turn off screen controls
                onScreenControls.SetActive(false);
                break;
        }
    }

    //if jump button is pressed, then set variable to true
    public void OnJumpButton_Down()
    {
        jumpButtonDown = true;
    }

    //if jump button is let go, then set variable to false
    public void OnJumpButton_Up()
    {
        jumpButtonDown = false;
    }

    //if shooting button is pressed, then set variable to true
    public void OnShootButton_Down()
    {
        shootButtonDown = true;
    }

    //if jump button is let go, then set variable to false
    public void OnShootButton_Up()
    {
        shootButtonDown = false;
    }
}
