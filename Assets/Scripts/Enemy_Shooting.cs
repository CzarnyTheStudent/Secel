using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Shooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;

    private float timer;

    public float bulletSpeed;
    
    [SerializeField] Transform player;
    [SerializeField] float aggroRange;

    void Update()
    {
        timer += Time.deltaTime;

        float distToPlayer = Vector2.Distance(transform.position, player.position);
        
        if(distToPlayer < aggroRange && timer > 3f)
        {
            if (player.position.x < transform.position.x)
            {
                timer = 0;
                shootLeft();
            }
            else
            {
                shootRight();
            }
        }
    }

    void shootRight()
    {
        var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        bullet.GetComponent<Rigidbody2D>().velocity = bulletSpawnPoint.right * bulletSpeed;
    }
    
    void shootLeft()
    {
        //Debug.Log("Strzelam w lewo");
        var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        bullet.GetComponent<Rigidbody2D>().velocity = bulletSpawnPoint.right * -bulletSpeed;
    }
}
