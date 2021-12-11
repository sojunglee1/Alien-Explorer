using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Item
{
    STAR,
    GOLD_COIN,
    SILVER_COIN,
    BRONZE_COIN
}

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
            Destroy(this.gameObject);
        }
    }
}
