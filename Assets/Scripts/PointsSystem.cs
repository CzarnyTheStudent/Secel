using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsSystem : MonoBehaviour
{
    public Text ScoreText;
    static public int Score;
    //public Collider2D Target;

    public PointsScriptable pointsSO;
    
    void Start()
    {
        Score = 0;
        ScoreText.text = "Score: " + Score;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Point")
        {
            Score++;
            Destroy(other.gameObject);
            ScoreText.text = "Score: " + Score;
            pointsSO.Add1ToPoints();
        }
        
    }

}
