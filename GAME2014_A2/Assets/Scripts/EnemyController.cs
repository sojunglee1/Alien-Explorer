using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*Source File Name: EnemyController
 * Author's Name: Sojung (Serena) Lee
 * Student #: 101245044
 * Date Last Modified: December 12, 2021
 * Program Description: Sets up the behaviors for enemy (e.g. movement, animation)
 * Revision History:
 * (December 11) Added basic functions (e.g. basic movement, LOS, animations) from personal GAME2014_Lab8 project
 * (December 12) Added how much each enemy is worth
 */

//Class for enemy movement behavior
public class EnemyController : MonoBehaviour
{
    //determines how much each enemy is worth when killed
    [Header("Points")]
    public int points;

    //helps enemy detects player
    [Header("Player Detection")]
    public LOS enemyLOS;

    //for basic movement (e.g. walking, ground detection)
    [Header("Movement")]
    public float runForce;
    public Transform lookAheadPoint;
    public Transform lookInFrontPoint;
    public LayerMask groundLayerMask;
    public LayerMask wallLayerMask;
    public bool isGroundAhead;
    private Rigidbody2D rb;

    //for enemy animations (e.g. idle & walking)
    [Header("Animation")]
    public Animator animatorController;

    void Start()
    {
        //gets component for physics and animations
        rb = GetComponent<Rigidbody2D>();
        enemyLOS = GetComponent<LOS>();
        animatorController = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        LookAhead();    //checks what's ahead of enemy (upcoming)
        LookInFront(); //checks what's in front of enemy (current)

        //if the enemy doesn't have a line of sight (LOS) of player, then continue with movement & enble a walking animation
        if (!HasLOS())
        {
            animatorController.enabled = true;
            animatorController.Play("Walk");
            MoveEnemy();
        }
        else //if the enemy does have a LOS of player, then...
        {   
            //if this enemy is a fast snail, then run to player faster
            if (this.gameObject.tag == "Snail") rb.AddForce(Vector2.left * runForce * transform.localScale.x);
            //if this enemy is a slime, then don't move
            if (this.gameObject.tag == "Slime") animatorController.enabled = false;
        }
    }

    //checks if enemy has line of sight of player
    private bool HasLOS()
    {
        if (enemyLOS.colliderList.Count > 0)
        {
            // Case 1 enemy polygonCollider2D collides with player and player is at the top of the list
            if ((enemyLOS.collidesWith.gameObject.CompareTag("Player")) &&
                (enemyLOS.colliderList[0].gameObject.CompareTag("Player")))
            {
                return true;
            }
            // Case 2 player is in the Collider List and we can draw ray to the player
            else
            {
                foreach (var collider in enemyLOS.colliderList)
                {
                    if (collider.gameObject.CompareTag("Player"))
                    {
                        var hit = Physics2D.Raycast(lookInFrontPoint.position, Vector3.Normalize(collider.transform.position - lookInFrontPoint.position), 5.0f, enemyLOS.contactFilter.layerMask);

                        if ((hit) && (hit.collider.gameObject.CompareTag("Player")))
                        {
                            return true;
                        }
                    }
                }
            }
        }

        return false;
    }

    //checks what's upcoming (ahead) using linecast
    private void LookAhead() 
    {
        var hit = Physics2D.Linecast(transform.position, lookAheadPoint.position, groundLayerMask);
        isGroundAhead = (hit) ? true : false;   //ground check
    }

    //checks what's in front using linecast
    private void LookInFront() 
    {
        var hit = Physics2D.Linecast(transform.position, lookInFrontPoint.position, wallLayerMask);
        if (hit)
        {
            Flip(); //if the enemy hits a wall, then flip the enemy to the other direction
        }
    }

    //creates movement behavior for enemy
    private void MoveEnemy()
    {
        //if there's more ground in front of enemy, then continue moving
        if (isGroundAhead)
        {
            rb.AddForce(Vector2.left * runForce * transform.localScale.x);
            rb.velocity *= 0.90f;
        }
        else //if there is no more ground, then flip to the other direction
        {
            Flip();
        }
    }

    //flips the sprite to face the opposite direction
    private void Flip() 
    {
        transform.localScale = new Vector3(transform.localScale.x * -1.0f, transform.localScale.y, transform.localScale.z);
    }

    //checks collision with enemy and other game objecs
    private void OnCollisionEnter2D(Collision2D other)
    {
        //if the enemy collides with the platform, then set its transform parent to the platform (easier for moving/floating platforms)
        if (other.gameObject.CompareTag("Platform"))
        {
            transform.SetParent(other.transform);
        }
    }

    //checks when other game objects stop colliding with enemy
    private void OnCollisionExit2D(Collision2D other)
    {
        //removes enemy's parent transform to itself after leaving platform
        if (other.gameObject.CompareTag("Platform"))
        {
            transform.SetParent(null);
        }
    }

    //draws gizmos to show user what is enemy 'seeing'
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, lookAheadPoint.position);
        Gizmos.DrawLine(transform.position, lookInFrontPoint.position);
    }
}
