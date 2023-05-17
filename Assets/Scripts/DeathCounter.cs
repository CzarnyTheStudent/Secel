using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "DeathCount", menuName = "ScriptableObjects/DeathCounter")]
public class DeathCounter : ScriptableObject
{
    public float DeathCountSave; 

    public void Add1ToDeaths()
    {
        DeathCountSave++;
    }

    private void OnDisable()
    {
       DeathCountSave = 0;
    }
}
