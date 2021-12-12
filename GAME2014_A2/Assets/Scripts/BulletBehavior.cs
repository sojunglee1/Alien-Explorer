using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    PlayerBehavior player;
    [SerializeField] private Vector2 startingPos;
    [SerializeField] private float distance;
    [SerializeField] private float speed;

    private float direction = 0;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehavior>();
        startingPos = player.transform.position;


        direction = player.transform.localScale.x;
        if (direction < 0) this.transform.position = startingPos - new Vector2(1, 0);
        else this.transform.position = startingPos + new Vector2(1, 0);
    }

    void FixedUpdate()
    {
        if (direction < 0) Move(-1);
        else Move(1);
    }

    private void Move(int direction)
    {
        this.transform.position += new Vector3(Time.deltaTime * speed * direction, 0.0f, 0.0f);
        if (direction > 0)
        {
            if (this.transform.position.x > startingPos.x + distance)
            {
                Destroy(this.gameObject);
            }
        }
        else
        {
            if (this.transform.position.x < startingPos.x - distance)
            {
                Destroy(this.gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Snail") || collision.gameObject.CompareTag("Slime"))
        {
            PlayerStats.AddPoints(collision.gameObject.GetComponent<EnemyController>().points);
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }
}
