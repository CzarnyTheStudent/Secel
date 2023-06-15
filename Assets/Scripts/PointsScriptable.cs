using UnityEngine;

[CreateAssetMenu(fileName = "PointsCounter", menuName = "ScriptableObjects/PointsCounter")]
public class PointsScriptable : ScriptableObject
{
    public float pointsCountSave; 

    public void Add1ToPoints()
    {
        pointsCountSave++;
    }

    private void OnDisable()
    {
        pointsCountSave = 0;
    }
}