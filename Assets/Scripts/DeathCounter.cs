using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "DeathCount", menuName = "ScriptableObjects/DeathCounter")]
public class DeathCounter : ScriptableObject
{
    public float DeathCountSave; 



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Add1ToDeaths()
    {
        DeathCountSave++;
    }

    private void OnDisable()
    {
       DeathCountSave = 0;
    }
}
