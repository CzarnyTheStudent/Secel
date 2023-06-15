using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    static public float TimePassed = 0f;
    static public bool IsTimerOn = false;

    public Text TimerTxt;

    public TimerScriptableObject timerSO;
   
    void Start()
    {
        IsTimerOn = true;
    }

    void Update()
    {
        if(IsTimerOn)
        {
            updateTimer(TimePassed);
        }
    }

    void updateTimer(float currentTime)
    {
        TimePassed += Time.deltaTime;
        
        string s = currentTime.ToString("0");
        
        TimerTxt.text = "Time: " + s +" seconds";
        timerSO.timer = Mathf.RoundToInt(TimePassed);
    }

}