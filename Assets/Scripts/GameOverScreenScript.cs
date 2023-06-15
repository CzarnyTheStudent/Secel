using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverScreenScript : MonoBehaviour
{
    public TextMeshProUGUI timertxt;
    public TextMeshProUGUI deathtxt;
    public TextMeshProUGUI pointstxt;
    
    public TimerScriptableObject timerCounter;
    public DeathCounter deathCounter;
    public PointsScriptable pointsCounter;
    void Start()
    {
        timertxt.text = "Time: " + timerCounter.timer;
        deathtxt.text = "Deaths: " + deathCounter.deathCountSave.ToString();
        pointstxt.text = "Points: " + pointsCounter.pointsCountSave.ToString();
    }
}
