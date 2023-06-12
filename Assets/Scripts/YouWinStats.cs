using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YouWinStats : MonoBehaviour
{
    public Text EndTimerTxt;
    public Text EndPointsTxt;

    // Start is called before the first frame update
    void Start()
    {
        string s = TimerScript.TimePassed.ToString("0");

        EndTimerTxt.text = "Time: " + s + " seconds";
        EndPointsTxt.text = "Score: " + PointsSystem.Score;
    }

}
