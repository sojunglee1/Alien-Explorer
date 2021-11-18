using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonBehavior : MonoBehaviour
{
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
}
