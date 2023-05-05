using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

public class WaypointPath : MonoBehaviour
{
    public enum PathMode { Mode2D, Mode3D };    // tryby 2D i 3D
    public PathMode pathMode;                  // bie¿¹cy tryb œcie¿ki

    //public Color lineColor = Color.yellow;
    public Color waypointColor = Color.white;
    public Color lineBetweenWaypointsColor = Color.red;
    public float radius = 0.7f;
    public bool isClosed = false;
    public Transform[] waypoints;

    [Header("Move on Path")]
    public float moveSpeed = 5f;
    public bool loop = false;
    [SerializeField] private int currentWaypointIndex = -1;

    private void OnDrawGizmos()
    {
        if (waypoints == null || waypoints.Length < 2)
        {
            return;
        }

        int count = waypoints.Length;

        if (count > 1)
        {
            for (int i = 0; i < count; i++)
            {
                Vector3 current = waypoints[i].position;
                Vector3 previous = Vector3.zero;

                if (i > 0)
                {
                    previous = waypoints[i - 1].position;
                }
                else if (i == 0 && isClosed)
                {
                    previous = waypoints[count - 1].position;
                }

                if (previous != Vector3.zero)
                {
                    Gizmos.color = lineBetweenWaypointsColor;
                    Gizmos.DrawLine(previous, current);
                }

                Gizmos.color = waypointColor;
                Gizmos.DrawWireSphere(current, radius);
            }

            if (isClosed)
            {
                Vector3 current = waypoints[0].position;
                Vector3 last = waypoints[count - 1].position;

                Gizmos.color = lineBetweenWaypointsColor;
                Gizmos.DrawLine(last, current);
            }

            //Gizmos.color = lineColor;
        }
    }

    private void OnValidate()
    {
        for (int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i].name = "Waypoint " + (i + 1);
        }
    }

    public void AddWaypoint()
    {
        if (!EditorApplication.isPlaying)
        {
            Vector3 position = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition).origin;
            GameObject waypointObject = new GameObject("Waypoint " + (waypoints.Length));
            if (pathMode == PathMode.Mode2D) position.z = 0;   //ustawia pozycjê z na 0 dla trybu 2D
            waypointObject.transform.position = position;

            Transform[] newWaypoints = new Transform[waypoints.Length + 1];
            for (int i = 0; i < waypoints.Length; i++)
            {
                newWaypoints[i] = waypoints[i];
            }

            newWaypoints[waypoints.Length] = waypointObject.transform;

            waypoints = newWaypoints;
        }
    }

    public void RemoveWaypoint()
    {
        if (!EditorApplication.isPlaying)
        {
            if (waypoints.Length > 0)
            {
                Transform[] newWaypoints = new Transform[waypoints.Length - 1];
                for (int i = 0; i < newWaypoints.Length; i++)
                {
                    newWaypoints[i] = waypoints[i];
                }

                DestroyImmediate(waypoints[waypoints.Length - 1].gameObject);
                waypoints = newWaypoints;
            }
        }
    }

    
    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.white;

        for (int i = 0; i < waypoints.Length; i++)
        {
            Vector3 position = waypoints[i].position;
            EditorGUI.BeginChangeCheck();
            position = Handles.PositionHandle(position, Quaternion.identity);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(waypoints[i], "Move Waypoint");
                waypoints[i].position = position;
            }

        GUIStyle style = new GUIStyle();
        style.normal.textColor = Color.red;
        style.alignment = TextAnchor.MiddleCenter;
        Handles.Label(position, "Waypoint " + i, style);


        }

    }

     
    private void Start()
    {
      
    }

    private void Update()
    {
        if (Input.GetButtonDown("AddWaypoint"))
        {
            AddWaypoint();
        }
        else if (Input.GetButtonDown("RemoveWaypoint"))
        {
            RemoveWaypoint();
        }

        
        if (waypoints != null && waypoints.Length > 1)
        {
            Transform currentWaypoint = waypoints[currentWaypointIndex];
            Debug.Log(waypoints[currentWaypointIndex]);
            
         
            Transform nextWaypoint = waypoints[(currentWaypointIndex + 1) % waypoints.Length];
            float distanceToNextWaypoint = Vector3.Distance(transform.position, currentWaypoint.position);


            // If object is close enough to the next waypoint, move to the next waypoint
            if (distanceToNextWaypoint < radius)
            {
                
                currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
                currentWaypoint = waypoints[(currentWaypointIndex + 1) % waypoints.Length];  //waypoints[currentWaypointIndex];
                                                                           //nextWaypoint
                transform.position = Vector3.MoveTowards(transform.position, nextWaypoint.position, moveSpeed * Time.deltaTime);

                if (!isClosed && currentWaypointIndex == waypoints.Length - 1 && distanceToNextWaypoint < radius)
                {
                    // Reverse the array of waypoints
                    System.Array.Reverse(waypoints);

                    // Reset the current waypoint index
                    currentWaypointIndex = 0;

                    // Set the transform position to the second waypoint
                    transform.position = waypoints[currentWaypointIndex].position;
                }
            }

            // Move object towards the next waypoint
            transform.position = Vector3.MoveTowards(transform.position, nextWaypoint.position, moveSpeed * Time.deltaTime);

            // If loop is disabled and object has reached the last waypoint, stop moving
            if (!loop && currentWaypointIndex == waypoints.Length - 1 && distanceToNextWaypoint < radius)
            {
                enabled = false;
            }
        }
    }

   
}
//MADE BY CZARNY_ No thanks to me :0
