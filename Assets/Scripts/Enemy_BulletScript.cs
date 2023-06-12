using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_BulletScript : MonoBehaviour
{
    public float life = 3f;
    private void Awake()
    {
        Destroy(gameObject, life);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.GetComponent<PlayerHealth>().TakeDamage();
        Destroy(gameObject);
    }
}
