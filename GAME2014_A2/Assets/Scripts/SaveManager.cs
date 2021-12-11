using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveManager : MonoBehaviour
{
    [SerializeField] Text scoreText;
    void Start()
    {
        int score = 0;
        if (PlayerPrefs.HasKey("Score"))
        {
            score = PlayerPrefs.GetInt("Score");
            scoreText.text = "Score : " + score.ToString();
        }
        else scoreText.text = "Score Not Available";
    }
}
