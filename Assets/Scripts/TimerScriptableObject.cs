using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//[CreateAssetMenu(fileName = "DeathCount", menuName = "ScriptableObjects/DeathCounter")]
public class TimerScriptableObject : ScriptableObject
{
    public float TimerSaved;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        TimerSaved += Time.deltaTime;
    }

    public void Add1ToTimer()
    {
        TimerSaved++;
    }

    private void OnDisable()
    {
        TimerSaved = 0;
    }
}
