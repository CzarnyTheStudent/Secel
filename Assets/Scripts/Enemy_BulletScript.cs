using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_BulletScript : MonoBehaviour
{
    public float life = 4f;
    private void Awake()
    {
        Destroy(gameObject, life);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage();
        }
        Destroy(gameObject);
    }
}
