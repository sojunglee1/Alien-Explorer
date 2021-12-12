using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsBehavior : MonoBehaviour
{
    [SerializeField] private int points;

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
}
