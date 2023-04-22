using UnityEngine;
using System.Collections.Generic;

public class PathCreator : MonoBehaviour
{
    // Define whether we're creating a 2D or 3D path
    public bool is2D = false;

    // Define the waypoints that will make up the path
    public List<Vector3> waypoints = new List<Vector3>();

    // Define the game object that will hold the path
    private GameObject pathGO;

    void Update()
    {
        // If the left mouse button is pressed and shift + ctrl are held down
        if (Input.GetMouseButton(0) && Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.LeftShift))
        {
            // Cast a ray from the mouse position in the scene
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                // Add the hit position as a waypoint to the path
                waypoints.Add(hit.point);
            }
        }

    }

    void OnValidate()
    {
        // Create a new game object for the path
        pathGO = new GameObject("Waypoint");
       
        for (int i = 0; i < waypoints.Count; i++)
        {
            Vector3 position = waypoints[i];
            if (is2D)
            {
                position.z = 0;
            }
        }
    }
}
