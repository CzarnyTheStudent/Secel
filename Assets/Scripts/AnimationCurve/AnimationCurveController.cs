using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCurveController : MonoBehaviour
{
    private int speed;// [] <-Attribute
    public float Speed;
    public float CurrentTime;
    public Vector2 StartPoint;
    public Vector2 EndPoint;
    public AnimationCurve Curve;


    void Start()
    {
        StartPoint = transform.position;
        EndPoint = Vector2.Scale(StartPoint, new Vector2(-1,1));
       
    }

    void Update()
    {
        CurrentTime += Time.deltaTime * Speed;
        transform.position = Vector2.Lerp(StartPoint, EndPoint, Curve.Evaluate(CurrentTime));
        

    }
}
