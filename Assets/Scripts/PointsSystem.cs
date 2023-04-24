using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsSystem : MonoBehaviour
{
    public Text ScoreText;
    private int Score;
    public Collider2D Target;
    
    void Start()
    {
        Score = 0;
        ScoreText.text = "Score: " + Score;
    }

    private void OnTriggerEnter(Collider2D Target)
    {
        //if (Target.tag == "Point")
        if(true)
        {
            Score++;
            Destroy(Target.gameObject);
            ScoreText.text = "Score: " + Score;
        }
        
    }

}
