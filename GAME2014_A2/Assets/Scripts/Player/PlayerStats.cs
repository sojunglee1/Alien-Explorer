using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    [HideInInspector] public static int PlayerPoints = 0;
    [SerializeField] public int Lives;
    [SerializeField] private Text livesText;

    [SerializeField] Text PointsPanel;

    Rigidbody2D rb;


    void Start()
    {
        PlayerPoints = 0;
        if (PlayerPrefs.HasKey("Score")) PlayerPoints = PlayerPrefs.GetInt("Score");
        rb = GetComponent<Rigidbody2D>();

        
    }

    private void Update()
    {
        PointsPanel.text = PlayerPoints.ToString();
        livesText.text = "x" + Lives.ToString();
        CheckDeath();
    }

    public void RemoveLife()
    {
        AudioManager.audioManager.DeathSound();
        Lives -= 1;
    }

    public void CheckDeath()
    {
        if (Lives == 0)
        {
            Save();
            SceneManager.LoadScene("Game Over");
        }
    }

    public static void AddPoints(int i)
    {
        PlayerPoints += i;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Finish")
        {
            Save();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        if (collision.gameObject.tag == "Obstacle")
        {
            RemoveLife();
        }        
        if (collision.gameObject.CompareTag("Snail") || collision.gameObject.CompareTag("Slime"))
        {
            AudioManager.audioManager.AttackedSound();

            RemoveLife();
        }
    }

    public static void Save()
    {
        PlayerPrefs.SetInt("Score", PlayerPoints);
    }
}
