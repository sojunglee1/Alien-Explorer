using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Source File Name: LOS
 * Author's Name: Sojung (Serena) Lee
 * Student #: 101245044
 * Date Last Modified: December 11, 2021
 * Program Description: set up line of sight for enemy
 * Revision History:
 * (December 11) sets up LOS for enemy's detection using contact list from personal GAME2014_Lab8 project
 */

[RequireComponent(typeof(PolygonCollider2D))]
[System.Serializable]
public class LOS : MonoBehaviour
{
    //sets up components for environmental detection
    [Header("Detection Properties")]
    public Collider2D collidesWith; // debug
    public ContactFilter2D contactFilter;
    public List<Collider2D> colliderList;

    private PolygonCollider2D LOSCollider;

    void Start()
    {
        LOSCollider = GetComponent<PolygonCollider2D>();
    }
    void FixedUpdate()
    {
        //gets a list of contacts that the enemy touches/collides with
        Physics2D.GetContacts(LOSCollider, contactFilter, colliderList);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //if enemy is colliding with other game object, then show it to editor
        collidesWith = other;
    }
}
