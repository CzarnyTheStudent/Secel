using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class WaypointPath : MonoBehaviour
{
    [SerializeField] private bool use3DTerrain = true;
    [SerializeField] private GameObject waypointPrefab;
    [SerializeField] private List<Vector3> waypointPositions = new List<Vector3>();

    private bool creatingWaypoints;
    private Camera sceneCamera;

    private void Start()
    {
        sceneCamera = SceneView.lastActiveSceneView.camera;
    }

    private void Update()
    {
        if (creatingWaypoints && Event.current.type == EventType.MouseDown && Event.current.button == 2)
        {
            var position = GetMousePositionInTerrain();
            CreateWaypoint(position);
        }

        if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.LeftShift)
        {
            creatingWaypoints = true;
        }
        else if (Event.current.type == EventType.KeyUp && Event.current.keyCode == KeyCode.LeftShift)
        {
            creatingWaypoints = false;
        }
    }

    private Vector3 GetMousePositionInTerrain()
    {
        Vector3 position = Vector3.zero;
        Ray ray = sceneCamera.ScreenPointToRay(Event.current.mousePosition);
        RaycastHit hit;
        if (use3DTerrain && Physics.Raycast(ray, out hit))
        {
            position = hit.point;
        }
        else if (!use3DTerrain && Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Terrain")))
        {
            position = hit.point;
        }
        return position;
    }

    private void CreateWaypoint(Vector3 position)
    {
        var waypoint = Instantiate(waypointPrefab, position, Quaternion.identity, transform);
        waypoint.name = "Waypoint " + (waypointPositions.Count + 1);
        waypointPositions.Add(position);
    }

    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.red;
        foreach (var position in waypointPositions)
        {
            Handles.DrawSolidDisc(position, Vector3.up, 0.5f);
        }
    }
}
