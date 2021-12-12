using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Source File Name: BulletBehavior
 * Author's Name: Sojung (Serena) Lee
 * Student #: 101245044
 * Date Last Modified: December 12, 2021
 * Program Description: Sets up the behaviors for an individual bullet
 * Revision History:
 * (December 12) Added basic behavior functions for bullet (e.g. enemy collision)
 * (December 12) Added basic physics functions for bullet 
 */

//Class for bullet movement behaviors
public class BulletBehavior : MonoBehaviour
{
    PlayerBehavior player;  //finds player - needed to player's forward direction & spawning point
    private float direction = 0;

    //basic physic components (e.g. starting position of the bullet, how far each bullet can go, and how fast each bullet can go)
    [SerializeField] private Vector2 startingPos;
    [SerializeField] private float distance;
    [SerializeField] private float speed;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehavior>();
        startingPos = player.transform.position;    //bullet starts at player's position

        direction = player.transform.localScale.x;  //gets direction of player's face

        //sets the bullet's starting position based on player's direction
        if (direction < 0) this.transform.position = startingPos - new Vector2(1, 0);   
        else this.transform.position = startingPos + new Vector2(1, 0);
    }

    void FixedUpdate()
    {
        //sets the bullet's shooting moving direction based on player's direction
        if (direction < 0) Move(-1);
        else Move(1);
    }

    //sets up the bullet movement when spawned
    private void Move(int direction)
    {
        //moves the bullet's horizontal position based on direction, speed, and time
        this.transform.position += new Vector3(Time.deltaTime * speed * direction, 0.0f, 0.0f);

        //after bullet shoots in a specific direction, checks if it reaches its max distance and then destroys bullet
        if (direction > 0)  // right forward direction
        {
            if (this.transform.position.x > startingPos.x + distance)
            {
                Destroy(this.gameObject);
            }
        }
        else //left forward direction
        {
            if (this.transform.position.x < startingPos.x - distance)
            {
                Destroy(this.gameObject);
            }
        }
    }

    //checks when player starts colliding with other game objects
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if the bullet hits an enemy -> adds points, destroys enemy, and then destroys itself
        if (collision.gameObject.CompareTag("Snail") || collision.gameObject.CompareTag("Slime"))
        {
            PlayerStats.AddPoints(collision.gameObject.GetComponent<EnemyController>().points);
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }
}
