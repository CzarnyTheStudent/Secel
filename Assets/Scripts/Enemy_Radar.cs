using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Radar : MonoBehaviour
{
    [SerializeField] Transform player;

    [SerializeField] float aggroRange;

    Rigidbody2D rb2d;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //dystans do gracza
        float distToPlayer = Vector2.Distance(transform.position, player.position);
        
        if(distToPlayer < aggroRange)
        {
            //kod strzelania do gracza
        }
        else
        {
            //koniec strzelania do gracza 
        }
    }
}
