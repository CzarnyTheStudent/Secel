using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class DamageOnColPlayer : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!collision.gameObject.GetComponent<PlayerMovement>().isDashing)
        {
          if (collision.gameObject.CompareTag("Player"))
          {
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage();
          }
        }

        
        if (collision.gameObject.GetComponent<PlayerMovement>().isDashing)
        {
            Destroy(gameObject);
        } 
    }
}
