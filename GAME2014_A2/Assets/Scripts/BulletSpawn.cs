using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletSpawn : MonoBehaviour
{
    [SerializeField] Transform SpawnPoint;

    [SerializeField] BulletBehavior Bullet;

    [SerializeField] private int bulletPopulation;
    [SerializeField] private List<BulletBehavior> bulletPool;

    [SerializeField] Text BulletText;

    private void Start()
    {
        SpawnPoint.position = this.transform.position;
        bulletPool = new List<BulletBehavior>(bulletPopulation);
    }

    private void Update()
    {
        BulletText.text = "x" + (bulletPopulation - bulletPool.Count).ToString();

        if (Input.GetKeyDown(KeyCode.E) || UIController.shootButtonDown)
        {
            if (bulletPool.Count < bulletPopulation)
            {
                AudioManager.audioManager.ShootSound();
                Instantiate(Bullet);
                bulletPool.Add(Bullet);
            }
            UIController.shootButtonDown = false;
        }
    }
}
