using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool IsOpen;
    public int KnocksToOpen = 1;

    private Animator DoorsAnim;
    private Collider2D DoorColl;
    public int knocks;
    // Start is called before the first frame update
    void Start()
    {
        DoorsAnim = GetComponent<Animator>();
        DoorColl = GetComponent<Collider2D>();
    }

    public void Open()
    {
        knocks++;
        if(!IsOpen && knocks >= KnocksToOpen)
        {
            SetState(true);
        }
    }

    public void Close()
    {
        knocks--;
        if (IsOpen && knocks < KnocksToOpen)
        {
            SetState(false);
        }
    }

    public void Toogle()
    {
        if (IsOpen)
        {
            Close();
        } 
        else
        {
            Open();
        }
    }

    void SetState(bool open)
    {
        IsOpen = open;
        DoorsAnim.SetBool("Open", open);
        DoorColl.isTrigger = open;
    }


}
