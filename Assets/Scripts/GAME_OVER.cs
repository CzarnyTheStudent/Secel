using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class GAME_OVER : MonoBehaviour
{
    private Collider2D door;
    public Door _door;
    public GameObject GameOver;

    private void Start()
    {
        door = GetComponent<Collider2D>();  
        _door = GetComponent<Door>();
        door.GetComponent<Collider2D>().enabled = false;
        GameOver.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (_door.IsOpened == true)
        {
            door.GetComponent<Collider2D>().enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
            GameOver.SetActive(true);
          //  TimerScript.IsTimerOn = false;
    }
}
