using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsSystem : MonoBehaviour
{
    public Text ScoreText;
    private int Score;
    //public Collider2D Target;
    
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
        }
        
    }

}
