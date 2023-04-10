using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public enum ResetType { Never, Timed, Immediately}

    public ResetType _ResetType = ResetType.Never;
    public GameObject Object;
    public bool On;
    public float TimeReset;

    private Animator LeverAnim;

    // Start is called before the first frame update
    void Start()
    {
        LeverAnim = GetComponent<Animator>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TurnOn()
    {
        if (!On)
        {
            SetState(true);
        }
    }

    public void TurnOff()
    {
        if (!On && _ResetType != ResetType.Never && _ResetType != ResetType.Timed)
        {
            SetState(false);
        }
    }

    public void Toogle()
    {
        if (On)
        {
            TurnOff();
        }
        else
        {
            TurnOn();
        }
    }

    void TimedReset()
    {
        SetState(false);
    }

    void SetState(bool on)
    {
        On = on;
        LeverAnim.SetBool("On", on);
        if (on)
        {
           if (_ResetType == ResetType.Immediately)
            {
                TurnOff();
            }
           else if (_ResetType == ResetType.Timed)
            {
                Invoke("TimedReset", TimeReset);
            }
        }
    }


}
