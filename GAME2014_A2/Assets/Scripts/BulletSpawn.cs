using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*Source File Name: BulletSpawn
 * Author's Name: Sojung (Serena) Lee
 * Student #: 101245044
 * Date Last Modified: December 12, 2021
 * Program Description: Sets up the bullet manager / bullet spawn manager
 * Revision History:
 * (December 12) Added shooting function to instantiate a bullet
 */

//Class for bullet management
public class BulletSpawn : MonoBehaviour
{
    [SerializeField] Transform SpawnPoint;  //creates the spawning point (position) for each bullet
    [SerializeField] BulletBehavior Bullet; //creates the bullet game object

    //creates a mini limited-sized bullet pool
    [SerializeField] private int bulletPopulation;
    [SerializeField] private List<BulletBehavior> bulletPool;

    //creates bullet text UI
    [SerializeField] Text BulletText;

    private void Start()
    {
        SpawnPoint.position = this.transform.position;  //spawning point is the same as bullet spawn manager point (attached to player)
        bulletPool = new List<BulletBehavior>(bulletPopulation);    //creates list for bullets
    }

    private void Update()
    {
        //always update the bullet text UI with the number of bullets that are left in the bullet pool
        BulletText.text = "x" + (bulletPopulation - bulletPool.Count).ToString();

        //checks user input to see if they shot bullet
        if (Input.GetKeyDown(KeyCode.E) || UIController.shootButtonDown)
        {
            //if there are spots available in the bullet pool...
            if (bulletPool.Count < bulletPopulation)
            {
                //..play shooting sound and create bullet game object
                AudioManager.audioManager.ShootSound();
                Instantiate(Bullet);
                bulletPool.Add(Bullet);
            }
            //one press = one bullet
            UIController.shootButtonDown = false;
        }
    }
}
