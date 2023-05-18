using UnityEngine;

[CreateAssetMenu(fileName = "DeathCount", menuName = "ScriptableObjects/DeathCounter")]
public class DeathCounter : ScriptableObject
{
    public float deathCountSave; 

    public void Add1ToDeaths()
    {
        deathCountSave++;
    }

    private void OnDisable()
    {
       deathCountSave = 0;
    }
}
