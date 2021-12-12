using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    [HideInInspector] public static int PlayerPoints = 0;
    [SerializeField] private GameObject LivesPanel;
    private SpriteRenderer[] lives;

    [SerializeField] Text PointsPanel;

    Rigidbody2D rb;

    void Start()
    {
        PlayerPoints = 0;

        if (PlayerPrefs.HasKey("Score")) PlayerPoints = PlayerPrefs.GetInt("Score");

        lives = LivesPanel.GetComponentsInChildren<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        PointsPanel.text = PlayerPoints.ToString();
        CheckDeath();
    }

    public void RemoveLife()
    {
        AudioManager.audioManager.DeathSound();
        for (int i = 0; i < lives.Length; i++)
        {
            if (lives[i] != null)
            {
                Destroy(lives[i]);
                break;
            }
        }
    }

    public void CheckDeath()
    {
        if (lives[lives.Length-1] == null)
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
            rb.AddForce(new Vector2(150, 250)); // flinch
            RemoveLife();
        }        
        if (collision.gameObject.CompareTag("Snail") || collision.gameObject.CompareTag("Slime"))
        {
            AudioManager.audioManager.AttackedSound();
            rb.AddForce(new Vector2(150, 250)); // flinch
            RemoveLife();
        }
    }

    public static void Save()
    {
        PlayerPrefs.SetInt("Score", PlayerPoints);
    }
}
