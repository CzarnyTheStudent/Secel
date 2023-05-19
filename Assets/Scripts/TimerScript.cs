using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class TimerScript : MonoBehaviour
{
    public TimerScriptableObject timerSO;
    public TextMeshProUGUI timerSOText;
    public float timerSOfloat;

    public static Action MinuteChanged; // sekundy
    public static Action HourChanged;   // minuty

    public static int Minute { get; private set; }
    public static int Hour { get; private set; }

    public float minuteRealTime = 1f;

    void Start()
    {
        timerSO.timer = minuteRealTime;

    }


    void Update()
    {

        timerSO.timer -= Time.deltaTime;

        if (timerSO.timer <= 0)
        {
            Minute++;
            MinuteChanged?.Invoke();


            if (Minute >= 60)
            {
                Hour++;
                Minute = 0;
                HourChanged?.Invoke();

            }

            timerSO.timer = minuteRealTime;
        }

    }

    private void OnEnable()
    {

        MinuteChanged += UpdateTime;
        HourChanged += UpdateTime;

    }

    private void OnDisable()
    {
        MinuteChanged -= UpdateTime;
        HourChanged -= UpdateTime;
    }

    void UpdateTime()
    {
        timerSOText.text = $"{Hour:00}:{Minute:00} AM";
    }



}
