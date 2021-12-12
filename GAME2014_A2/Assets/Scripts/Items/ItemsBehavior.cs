using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemsBehavior : MonoBehaviour
{
    [SerializeField] private int points;


    private void Start()
    {
        RandomizeLocation();
    }

    public void SetStartingPoints(int i)
    {
        points = i;
    }

    public void Collect()
    {
        PlayerStats.AddPoints(points);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Collect();

            if (this.gameObject.tag == "Coin") AudioManager.audioManager.CoinSound();
            if (this.gameObject.tag == "Star") AudioManager.audioManager.StarSound();

            Destroy(this.gameObject);
        }
    }

    public void RandomizeLocation()
    {
        int PosX = Random.Range(40, 110);
        int PosY = Random.Range(-4, 14);
        if (SceneManager.GetActiveScene().name == "Level 3")
        {
            this.transform.position = new Vector2(PosX, PosY);
        }
    }
}
