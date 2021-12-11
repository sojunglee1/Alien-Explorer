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

    void Start()
    {
        PlayerPoints = 0;
        lives = LivesPanel.GetComponentsInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        PointsPanel.text = PlayerPoints.ToString();
        CheckDeath();
    }

    public void RemoveLife()
    {
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
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        if (collision.gameObject.tag == "Obstacle")
        {
            RemoveLife();
        }
    }

    public static void Save()
    {
        PlayerPrefs.SetInt("Score", PlayerPoints);
    }
}
