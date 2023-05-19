using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Shooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;

    private float timer;

    public float bulletSpeed;

    void Update()
    {
        timer += Time.deltaTime;

        if(timer > 2.5f)
        {
            timer = 0;
            shootRight();

        }
    }

    void shootRight()
    {
        var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        bullet.GetComponent<Rigidbody2D>().velocity = bulletSpawnPoint.right * bulletSpeed;
    }
}
